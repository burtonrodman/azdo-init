using System.CommandLine;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace AzureDevOpsInit;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAllCommands(this IServiceCollection services)
    {
        var commandType = typeof(Command);
        var commands = typeof(Program).Assembly
            .GetExportedTypes()
            .Where(t => commandType.IsAssignableFrom(t));
        foreach (var command in commands)
        {
            services.AddSingleton(commandType, command);
        }

        return services;
    }
}