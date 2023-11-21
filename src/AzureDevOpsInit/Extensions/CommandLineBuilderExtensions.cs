using System.CommandLine;
using System.CommandLine.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AzureDevOpsInit;

public static class CommandLineBuilderExtensions
{
    public static CommandLineBuilder AddAllCommands(this CommandLineBuilder builder, ServiceProvider services)
    {
        foreach (var command in services.GetServices<Command>())
        {
            builder.Command.Add(command);
        }
        return builder;
    }
}