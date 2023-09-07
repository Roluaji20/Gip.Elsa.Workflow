using Elsa.EntityFrameworkCore.Extensions;
using Elsa.EntityFrameworkCore.Modules.Management;
using Elsa.EntityFrameworkCore.Modules.Runtime;
using Elsa.Extensions;

namespace Gip.Elsa.Workflow.Extensions;

public static class DependencyInjection
{
    public static void AddElsaServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var sqliteConnectionString = configuration.GetConnectionString("Sqlite")!;

        // Add services to the container.
        services.AddElsa(elsa =>
       {
           // Expose API endpoints.
           elsa.UseWorkflowsApi();

           // Add services for HTTP activities and workflow middleware.
           elsa.UseHttp();

           // Configure identity so that we can create a default admin user.
           elsa.UseIdentity(identity =>
           {
               // var config = configuration.getse;
               var identitySection = configuration.GetSection("Identity");
               var identityTokenSection = identitySection.GetSection("Tokens");

               identity.IdentityOptions = options => identitySection.Bind(options);
               identity.TokenOptions = options => identityTokenSection.Bind(options);
               identity.UseConfigurationBasedUserProvider(options => identitySection.Bind(options));
               identity.UseConfigurationBasedApplicationProvider(options => identitySection.Bind(options));
               identity.UseConfigurationBasedRoleProvider(options => identitySection.Bind(options));
           });

           elsa.UseWorkflowManagement(management =>
           {
               management.UseEntityFrameworkCore(ef => ef.UseSqlite(sqliteConnectionString));
               management.UseWorkflowInstances(m => m.UseEntityFrameworkCore(ef => ef.UseSqlite(sqliteConnectionString)));

           });

           elsa.UseWorkflowRuntime(runtime =>
           {
               // Use EF core for triggers and bookmarks.
               runtime.UseEntityFrameworkCore(ef => ef.UseSqlite(sqliteConnectionString));
               // Use EF core for execution log records.
               //  runtime.UseExecutionLogRecords(log => log.UseEntityFrameworkCore(ef => ef.UseSqlite(sqliteConnectionString)));
               // Use the default workflow runtime with EF core.
               // runtime.UseDefaultRuntime(defaultRuntime => defaultRuntime.UseEntityFrameworkCore(ef => ef.UseSqlite(sqliteConnectionString)));
               // Install a workflow state exporter to capture workflow states and store them in IWorkflowInstanceStore.
               // runtime.UseAsyncWorkflowStateExporter();
           });

           // Use default authentication (JWT + API Key).
           elsa.UseDefaultAuthentication(auth => auth.UseAdminApiKey());

           elsa.UseWorkflows(workflows =>
           {
               // Configure workflow execution pipeline to handle workflow contexts.
               //workflows.WithWorkflowExecutionPipeline(pipeline => pipeline
               //.Reset()
               //.UsePersistentVariables()
               //.UseBookmarkPersistence()
               //.UseWorkflowExecutionLogPersistence()
               //.UseWorkflowContexts()
               //  .UseDefaultActivityScheduler()
               //  );

               // Configure activity execution pipeline to handle workflow contexts.
               //   workflows.WithActivityExecutionPipeline(pipeline => pipeline
               //    .Reset()
               //    .UseWorkflowContexts()
               //   .UseBackgroundActivityInvoker()
               // );

           });

           elsa.UseWorkflowsApi();
           elsa.UseJavaScript();
           elsa.UseLiquid();
           elsa.UseHttp();


           // Add activities.
           elsa.AddActivitiesFrom<Program>();

       });
    }
}
