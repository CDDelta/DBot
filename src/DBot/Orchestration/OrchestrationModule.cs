using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace DBot.Orchestration
{
  public class OrchestrationModule : IHostedService
  {
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<OrchestrationModule> logger;

    public OrchestrationModule(
      IServiceProvider serviceProvider,
      ILogger<OrchestrationModule> logger)
    {
      this.serviceProvider = serviceProvider;
      this.logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      var modules = serviceProvider
        .GetServices<IHostedService>()
        .OfType<IUpdatableModule>();

      return Task.Run(async () =>
      {
        while (!cancellationToken.IsCancellationRequested)
        {
          foreach (var module in modules)
            await module.UpdateAsync(cancellationToken);
        }
      }, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
  }
}