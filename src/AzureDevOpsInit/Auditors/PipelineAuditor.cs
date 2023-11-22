// pipeline(s) exist

using AzureDevOpsInit.Auditors;

public class PipelineAuditor : IAuditor
{
    public int Ordinal => 80;

    public Task<string?> Audit(AzureDevOpsInitConfiguration config, Dictionary<string, object> state)
    {
        throw new NotImplementedException();
    }
}