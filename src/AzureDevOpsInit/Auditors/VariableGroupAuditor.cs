// variable group(s) exist
using AzureDevOpsInit.Auditors;

public class VariableGroupAuditor : IAuditor
{
    public IEnumerable<string> Dependencies => [];

    public Task<string?> Audit(AzureDevOpsInitConfiguration config, Dictionary<string, object> state)
    {
        throw new NotImplementedException();
    }
}