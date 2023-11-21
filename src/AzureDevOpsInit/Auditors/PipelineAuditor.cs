// pipeline(s) exist

using AzureDevOpsInit.Auditors;

public class PipelineAuditor : IAuditor
{
    public IEnumerable<string> Dependencies => [];

    public Task<string?> Audit(AzureDevOpsInitConfiguration config, Dictionary<string, object> state)
    {
        throw new NotImplementedException();
    }
}