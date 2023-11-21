
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
        this.SetHandler(Execute);
        _logger = logger;
        _auditors = auditors;
        _initConfigurationProvider = initConfigurationProvider;
    }

    public async Task<int> Execute()
    {
        var config = _initConfigurationProvider.Read();
        if (config is null) return 1;

        var state = new Dictionary<string, object>();
        foreach (var auditor in _auditors)
        {
            await auditor.Audit(state);
        }

        return 0;
    }
}