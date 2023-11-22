
using System.Linq;

using AzureDevOpsInit.Auditors;

using Microsoft.Extensions.Logging;

namespace AzureDevOpsInit.Commands;

public class AuditCommand : Command
{
    private readonly ILogger<AuditCommand> _logger;
    private readonly IEnumerable<IAuditor> _auditors;
    private readonly IInitConfigurationProvider _initConfigurationProvider;

    public AuditCommand(
        ILogger<AuditCommand> logger,
        IEnumerable<IAuditor> auditors,
        IInitConfigurationProvider initConfigurationProvider)
        : base("audit", "Audit the project's configuration based on settings in an azdo-init.yml file in the current folder.")
    {
        _logger = logger;
        _auditors = auditors;
        _initConfigurationProvider = initConfigurationProvider;
        this.SetHandler(Execute);
    }

    public async Task<int> Execute()
    {
        var config = _initConfigurationProvider.Read();
        if (config is null) return 1;

        var state = new Dictionary<string, object>();
        foreach (var auditor in _auditors.OrderBy(x => x.Ordinal))
        {
            await auditor.Audit(config, state);
        }

        return 0;
    }
}