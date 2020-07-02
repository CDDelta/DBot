using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DBot.Perception
{
  public class PerceptionModule : IHostedService
  {
    private readonly ILogger logger;

    public PerceptionModule(
        ILogger<PerceptionModule> logger,
        PointCloudBuilder pointCloudBuilder)
    {
      this.logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}