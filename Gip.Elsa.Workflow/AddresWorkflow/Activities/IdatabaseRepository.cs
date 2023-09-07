using System.Dynamic;

namespace Gip.Elsa.Workflow.AddresWorkflow.Activities;

public interface IdatabaseRepository
{
    object AsignarParaIntegrar(ExpandoObject reclamation);
}