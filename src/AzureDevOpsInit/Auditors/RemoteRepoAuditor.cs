// repository exists
// has all the remote branches
// default branch is set 

using AzureDevOpsInit.Auditors;

public class RemoteRepoAuditor : IAuditor
{
    public IEnumerable<string> Dependencies => [ "localRepo" ];

    public Task<string?> Audit(AzureDevOpsInitConfiguration config, Dictionary<string, object> state)
    {
        throw new NotImplementedException();
    }
}