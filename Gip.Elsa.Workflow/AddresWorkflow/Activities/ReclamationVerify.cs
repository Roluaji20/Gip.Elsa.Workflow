using Elsa.Extensions;
using Elsa.Workflows.Core;
using Elsa.Workflows.Core.Attributes;
using Elsa.Workflows.Core.Models;
using Gip.Elsa.Workflow.AddresWorkflow.Models;
using Newtonsoft.Json;

namespace Gip.Elsa.Workflow.AddresWorkflow.Activities;

[Activity("Address", "ReclamationVerify", Description = "A activity to verify reclamation", Kind = ActivityKind.Task)]
public class ReclamationVerify : CodeActivity<bool>
{

    public Input<string> Data { get; set; } = default!;

    protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
    {

        var reclamationJson = Data.Get(context);
        var reclamation = JsonConvert.DeserializeObject<Reclamation>(reclamationJson);

        if (reclamation is null)
        {
            throw new Exception($"Reclamacion vacia: {reclamation}");
        }

        var result = reclamation.IsValid ? true : false;

        context.SetResult(result);

        //await context.CompleteActivityAsync(result);

        //await Task.CompletedTask;
        //await CompleteAsync(context);

    }
}
