using Elsa.Extensions;
using Elsa.Workflows.Core;
using Elsa.Workflows.Core.Attributes;
using Elsa.Workflows.Core.Models;
using Newtonsoft.Json;
using System.Dynamic;

namespace Gip.Elsa.Workflow.AddresWorkflow.Activities;

[Activity("Address", "Assign Mark forIntegration", Description = "A activity for assing mark to reclamation", Kind = ActivityKind.Task)]
public class AssignMarkforIntegration : CodeActivity<string>
{
    public Input<ExpandoObject> Data { get; set; } = default!;

    //private readonly IdatabaseRepository _repository;
    //public AssignMarkforIntegration(IdatabaseRepository repository)
    //{
    //    _repository = repository;
    //}

    protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        var reclamation = Data.Get(context);
        string RecJson = JsonConvert.SerializeObject(reclamation);

        //var response = _repository.AsignarParaIntegrar(reclamation);

        //var message = $"Hello, {RecJson}!";

        //context.SetResult(message);



        await Task.CompletedTask;

    }
}
