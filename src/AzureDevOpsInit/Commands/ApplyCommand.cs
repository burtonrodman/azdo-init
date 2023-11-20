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

    public ApplyCommand(
        ILogger<ApplyCommand> logger,
        GitRepo gitRepo)
        : base("apply", "apply settings from azdo-init.yml to the project.")
    {
        _logger = logger;
        _gitRepo = gitRepo;

        this.SetHandler(Execute);
    }

    public Task<int> Execute()
    {
        _gitRepo.EnsureExists();
        
        // TODO - validate account
        // TODO - validate project
        
        var config = ReadConfiguration();
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

    private AzureDevOpsInitConfiguration ReadConfiguration()
    {
        var fullPath = Path.GetFullPath("azdo-init.yml");
        if (!File.Exists(fullPath))
        {
            _logger.LogError($"{fullPath} not found.");
            throw new FileNotFoundException("azdo-init.yml not found.");
        }
        _logger.LogInformation($"applying settings from {fullPath}.");

        var serializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        return serializer.Deserialize<AzureDevOpsInitConfiguration>(File.ReadAllText(fullPath));
    }
}
