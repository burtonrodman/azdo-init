using System.CommandLine.Builder;
using System.CommandLine.Parsing;

using AzureDevOpsInit.Auditors;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureDevOpsInit;

public class Program
{
    static async Task<int> Main(string[] args)
    {
        var assembly = typeof(Program).Assembly;

        var config = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(assembly.Location))
            .AddJsonFile("appsettings.json", false)
            .Build();

        var serviceProvider = new ServiceCollection()
            .AddLogging(builder => {
                builder.AddConsole();
            })
            .AddAllAsSingleton<Command>()
            .AddAllAsSingleton<IAuditor>()
            .AddTransient<GitRepo>()
            .AddSingleton<IInitConfigurationProvider, InitConfigurationProvider>()
            .BuildServiceProvider();

        var parser = new CommandLineBuilder()
            .AddAllCommands(serviceProvider)
            .UseDefaults()
            .Build();

        try
        {
            return await parser.InvokeAsync(args).ConfigureAwait(false);
        }
        catch (CommandException ex)
        {
            Console.WriteLine(ex.Message);
            return ex.GetExitCode();
        }
    }
}