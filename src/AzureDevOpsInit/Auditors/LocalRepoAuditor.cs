// is git repo
// has all the local branches

using System.Linq;

using AzureDevOpsInit.Auditors;

using Microsoft.Extensions.Logging;

public class LocalRepoAuditor : IAuditor
{
    private readonly ILogger<LocalRepoAuditor> _logger;
    private readonly GitRepo _gitRepo;

    public LocalRepoAuditor(
        ILogger<LocalRepoAuditor> logger,
        GitRepo gitRepo)
    {
        _logger = logger;
        _gitRepo = gitRepo;
    }

    public int Ordinal => 10;

    public Task<string?> Audit(AzureDevOpsInitConfiguration config, Dictionary<string, object> state)
    {
        if (!_gitRepo.Exists()) {
            _logger.LogError("local git repo not found.");
            return Task.FromResult<string?>("local git repo not found.");
        }

        var branches = _gitRepo.GetBranches();
        foreach (var branch in config.Branches)
        {
            if (!branches.Contains(branch.Name))
            {
                _logger.LogWarning("branch {branch} not found.", branch.Name);
            }
        }

        return Task.FromResult<string?>(null);
    }
}