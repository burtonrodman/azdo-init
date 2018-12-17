using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static azdo_init.HelpCommand;

namespace azdo_init
{
    class Program
    {
        static void Main(string[] args)
        {
            // if (IsHelpCommand(args.FirstOrDefault())) {
            //     ShowHelp();
            // }

            var logger = new ConsoleLogger();

            var gitrepo = new GitRepo(logger);
            gitrepo.EnsureExists();

            var initfile = new InitFile(logger);
            if (!initfile.Exists())
            {
                initfile.Create();
                return;
            }

        }

    }
}
