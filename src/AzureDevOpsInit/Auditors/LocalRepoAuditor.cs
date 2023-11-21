// is git repo
// has all the local branches

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

    public IEnumerable<string> Dependencies => [];

    public Task<string?> Audit(AzureDevOpsInitConfiguration config, Dictionary<string, object> state)
    {
        if (!_gitRepo.Exists()) {
            _logger.LogError("local git repo not found.");
            return "local git repo not found.";
        }
        return null;
    }
}