using System.CommandLine;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace AzureDevOpsInit;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAllAsSingleton<T>(this IServiceCollection services)
    {
        var keyType = typeof(T);
        var implementations = typeof(Program).Assembly
            .GetExportedTypes()
            .Where(t => !keyType.IsInterface)
            .Where(t => keyType.IsAssignableFrom(t));
        foreach (var impl in implementations)
        {
            services.AddSingleton(keyType, impl);
        }

        return services;
    }
}