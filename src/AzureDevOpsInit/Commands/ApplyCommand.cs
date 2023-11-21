using System;
using System.CommandLine;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

using static AzureDevOpsInit.ProcessUtilities;

namespace AzureDevOpsInit.Commands;

public class ApplyCommand : Command
{
    private readonly ILogger<ApplyCommand> _logger;
    private readonly GitRepo _gitRepo;
    private readonly IInitConfigurationProvider _initConfigurationProvider;

    public ApplyCommand(
        ILogger<ApplyCommand> logger,
        GitRepo gitRepo,
        IInitConfigurationProvider initConfigurationProvider)
        : base("apply", "apply settings from azdo-init.yml to the project.")
    {
        _logger = logger;
        _gitRepo = gitRepo;
        _initConfigurationProvider = initConfigurationProvider;
        this.SetHandler(Execute);
    }

    public Task<int> Execute()
    {
        _gitRepo.EnsureExists();
        
        // TODO - validate account
        // TODO - validate project
        
        var config = _initConfigurationProvider.Read();
        if (config is null) return Task.FromResult(1);

        if (string.IsNullOrWhiteSpace(config.Repository))
        {
            config.Repository = new DirectoryInfo(Environment.CurrentDirectory).Name;
        }

        var projectFiles = Directory.GetFiles(Environment.CurrentDirectory, "*.csproj", SearchOption.AllDirectories);
        if (!projectFiles.Any())
        {
            ExecuteProcess("dotnet", "new console");
            _gitRepo.StageAllFiles();
            _gitRepo.Commit("initial commit");
        }

        var output = ExecuteProcess("az", $"repos show --org https://dev.azure.com/{config.Account} --project {config.Project} --repository {config.Repository}");
        if (output.Contains("does not exist")) {
            var createOutput = ExecuteProcess("az", $"repos create --org https://dev.azure.com/{config.Account} --project {config.Project} --repository {config.Repository}");
            _logger.LogInformation(createOutput);
        }

        return Task.FromResult(0);
    }
}
