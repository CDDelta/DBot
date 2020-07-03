using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DBot.Perception
{
  public class PerceptionModule : IHostedService, IUpdatableModule
  {
    private readonly PerceptionOptions options;
    private readonly ILogger logger;

    public PerceptionModule(
        PointCloudBuilder pointCloudBuilder,
        IOptions<PerceptionOptions> options,
        ILogger<PerceptionModule> logger)
    {
      this.options = options.Value;
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