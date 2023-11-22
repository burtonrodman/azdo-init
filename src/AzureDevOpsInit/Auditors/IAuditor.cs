using AzureDevOpsInit.Models;

namespace AzureDevOpsInit.Auditors;

public interface IAuditor
{
    int Ordinal { get; }
    Task<string?> Audit(AzureDevOpsInitConfiguration config, Dictionary<string, object> state);
}
