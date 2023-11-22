// repository exists
// has all the remote branches
// default branch is set 

using AzureDevOpsInit.Auditors;

public class RemoteRepoAuditor : IAuditor
{
    public int Ordinal => 20;

    public Task<string?> Audit(AzureDevOpsInitConfiguration config, Dictionary<string, object> state)
    {
        throw new NotImplementedException();
    }
}