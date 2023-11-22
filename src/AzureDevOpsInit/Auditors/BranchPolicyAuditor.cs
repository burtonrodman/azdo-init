// branch policies are set

using AzureDevOpsInit.Auditors;

public class BranchPolicyAuditor : IAuditor
{
    public int Ordinal => 100;

    public Task<string?> Audit(AzureDevOpsInitConfiguration config, Dictionary<string, object> state)
    {
        throw new NotImplementedException();
    }
}