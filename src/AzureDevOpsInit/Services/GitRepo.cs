
using Microsoft.Extensions.Logging;

using static AzureDevOpsInit.ProcessUtilities;

namespace AzureDevOpsInit.Services;

public class GitRepo
{
    private readonly ILogger<GitRepo> logger;

    public GitRepo(ILogger<GitRepo> logger)
    {
        this.logger = logger;
    }

    public void EnsureExists()
    {
        if (!Exists())
        {
            Init();
            logger.LogInformation("repo initialized.");
        }
        else
        {
            logger.LogTrace("repo exists.");
        }
    }

    public bool Exists()
    {
        return ExecuteProcess("git", "rev-parse", "--is-inside-work-tree").StartsWith("true");
    }

    public static void Init()
    {
        ExecuteProcess("git", "init");
    }

    public void StageAllFiles()
    {
        ExecuteProcess("git", "add .");
    }

    public void Commit(string message)
    {
        ExecuteProcess("git", $"commit -m \"{message}\"");
    }
}
