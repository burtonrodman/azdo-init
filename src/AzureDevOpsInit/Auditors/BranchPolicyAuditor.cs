// branch policies are set

using AzureDevOpsInit.Auditors;

public class BranchPolicyAuditor : IAuditor
{
    public IEnumerable<string> Dependencies => [ "remoteRepository" ];

    public Task<string?> Audit(AzureDevOpsInitConfiguration config, Dictionary<string, object> state)
    {
        throw new NotImplementedException();
    }
}