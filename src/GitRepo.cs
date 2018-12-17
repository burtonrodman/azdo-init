using System;
using static azdo_init.ProcessUtilities;
namespace azdo_init
{

    public class GitRepo
    {
        private readonly ILogger logger;

        public GitRepo(ILogger logger)
        {
            this.logger = logger;
        }

        public void EnsureExists()
        {
            if (!Exists())
            {
                Init();
                logger.WriteAction("repo initialized.");
            }
            else
            {
                logger.WriteInformation("repo exists.");
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

    }
}