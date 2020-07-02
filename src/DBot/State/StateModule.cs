using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DBot.State
{
  public class StateModule : IHostedService, IUpdatableModule
  {
    private readonly ILogger<StateModule> logger;

    public StateModule(ILogger<StateModule> logger)
    {
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