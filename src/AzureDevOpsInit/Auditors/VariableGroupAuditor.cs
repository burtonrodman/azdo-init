// variable group(s) exist
using AzureDevOpsInit.Auditors;

public class VariableGroupAuditor : IAuditor
{
    public int Ordinal => 90;

    public Task<string?> Audit(AzureDevOpsInitConfiguration config, Dictionary<string, object> state)
    {
        throw new NotImplementedException();
    }
}