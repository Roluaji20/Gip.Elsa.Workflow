using Elsa.Extensions;
using Gip.Elsa.Workflow.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddElsaServices(builder.Configuration);
    // Configure CORS to allow designer app hosted on a different origin to invoke the APIs.
    builder.Services.AddCors(cors => cors.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    app.UseHttpsRedirection();
    app.UseCors();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseWorkflowsApi();
    app.UseWorkflows();
    app.Run();
}