namespace AzureDevOpsInit.Auditors;

public interface IAuditor
{
    IEnumerable<string> Dependencies { get; }
    Task<string?> Audit();
}
