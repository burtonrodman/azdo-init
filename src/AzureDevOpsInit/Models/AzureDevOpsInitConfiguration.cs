namespace AzureDevOpsInit.Models;

public class AzureDevOpsInitConfiguration
{
    public string? Version { get; set; }
    public string? Account { get; set; }
    public string? Project { get; set; }
    public string? Repository { get; set; }
    public List<BranchConfiguration>? Branches { get; set; }
    public List<PipelineConfiguration>? Pipelines { get; set; }
}
