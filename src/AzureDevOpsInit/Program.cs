using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.IO;
using System.Threading.Tasks;
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
            .AddJsonFile("appsettings.json", true)
            .Build();

        var serviceProvider = new ServiceCollection()
            .AddLogging(builder => {
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddConsole();
            })
            .AddAllAsSingleton<Command>()
            .AddAllAsSingleton<IAuditor>()
            .AddTransient<GitRepo>()
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