using AzureDevOpsInit.Models;

namespace AzureDevOpsInit.Auditors;

public interface IAuditor
{
    IEnumerable<string> Dependencies { get; }
    Task<string?> Audit(AzureDevOpsInitConfiguration config, Dictionary<string, object> state);
}
