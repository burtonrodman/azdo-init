
using System.CommandLine;
using System.IO;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace AzureDevOpsInit.Commands;

public class AuditCommand : Command
{
    private readonly ILogger<AuditCommand> _logger;

    public AuditCommand(
        ILogger<AuditCommand> logger,
        IEnumerable<IAuditor> auditors)
        : base("audit", "Audit the project's configuration based on settings in an azdo-init.yml file in the current folder.")
    {
        this.SetHandler(Execute);
        _logger = logger;
    }

    public Task<int> Execute()
    {
        var fullPath = Path.GetFullPath("azdo-init.yml");
        if (!File.Exists(fullPath))
        {
            _logger.LogTrace($"{fullPath} not found.");
            return Task.FromResult(1);
        }



        return Task.FromResult(0);
    }
}