using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DBot.Perception
{
  public class PerceptionModule : IHostedService, IUpdatableModule
  {
    private readonly IOptionsMonitor<PerceptionOptions> optionsMonitor;
    private readonly ILogger logger;

    public PerceptionModule(
        PointCloudBuilder pointCloudBuilder,
        IOptionsMonitor<PerceptionOptions> optionsMonitor,
        ILogger<PerceptionModule> logger)
    {
      this.optionsMonitor = optionsMonitor;
      this.logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public async Task UpdateAsync(CancellationToken cancellationToken)
    {
      logger.LogInformation("Updating...");
      await Task.Delay(1000);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
  }
}