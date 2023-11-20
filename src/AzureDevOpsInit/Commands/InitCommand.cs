
using System.CommandLine;
using System.IO;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace AzureDevOpsInit.Commands;

public class InitCommand : Command
{
    private readonly ILogger<InitCommand> _logger;

    public InitCommand(ILogger<InitCommand> logger)
        : base("init", "Initialize a azdo-init.yml file in the current folder.")
    {
        this.SetHandler(Execute);
        _logger = logger;
    }

    public Task<int> Execute()
    {
        var fullPath = Path.GetFullPath("azdo-init.yml");
        if (File.Exists(fullPath))
        {
            _logger.LogTrace($"{fullPath} already exists.");
        }
        else 
        {
            using (var stream = typeof(InitCommand).Assembly.GetManifestResourceStream("AzureDevOpsInit.template.yml"))
            using (var reader = new StreamReader(stream))
            {
                File.WriteAllText(fullPath, reader.ReadToEnd());
            }
            _logger.LogInformation($"created {fullPath}");
        }

        return Task.FromResult(0);
    }
}