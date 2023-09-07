using Elsa.Workflows.Core;
using Elsa.Workflows.Core.Attributes;
using Elsa.Workflows.Core.Models;
using System.Dynamic;

namespace Gip.Elsa.Workflow.AddresWorkflow.Activities;

[Activity("Address", "ReclamationExport", Description = "Mark Reclamation to export", Kind = ActivityKind.Task)]
public class ReclamationExport : CodeActivity<string>
{
    public Input<ExpandoObject> Data { get; set; } = default!;

    protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        //var reclamation = Data.Get(context);
        //string RecJson = JsonConvert.SerializeObject(reclamation);
        //var message = $"Hello, {RecJson}!";

        //context.SetResult(message);


        await Task.CompletedTask;

    }
}
