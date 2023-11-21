using AzureDevOpsInit.Models;

using Microsoft.Extensions.Logging;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AzureDevOpsInit.Services;

public interface IInitConfigurationProvider
{
    void CreateFromTemplate();
    AzureDevOpsInitConfiguration? Read();
}

public class InitConfigurationProvider : IInitConfigurationProvider
{
    private readonly ILogger<InitConfigurationProvider> _logger;

    public InitConfigurationProvider(ILogger<InitConfigurationProvider> logger)
    {
        _logger = logger;
    }
    
    public void CreateFromTemplate()
    {
        var fullPath = Path.GetFullPath("azdo-init.yml");
        if (File.Exists(fullPath))
        {
            _logger.LogTrace("{fullPath} already exists.", fullPath);
        }
        else
        {
            using (var stream = typeof(InitConfigurationProvider).Assembly.GetManifestResourceStream("AzureDevOpsInit.template.yml")!)
            using (var reader = new StreamReader(stream))
            {
                File.WriteAllText(fullPath, reader.ReadToEnd());
            }
            _logger.LogInformation("created {fullPath}", fullPath);
        }
    }

    public AzureDevOpsInitConfiguration? Read()
    {
        var fullPath = Path.GetFullPath("azdo-init.yml");
        if (File.Exists(fullPath))
        {
            _logger.LogInformation("applying settings from {fullPath}.", fullPath);

            var serializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            return serializer.Deserialize<AzureDevOpsInitConfiguration>(File.ReadAllText(fullPath));
        }

        _logger.LogWarning("{fullPath} not found.", fullPath);
        return null;
    }
}