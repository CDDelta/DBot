using System.CommandLine;
using System.CommandLine.DragonFruit;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace DBot.Director
{
  public class DirectorModule : IHostedService
  {
    private readonly IConfiguration configuration;
    private readonly ILogger<DirectorModule> logger;

    public DirectorModule(
      IConfiguration configuration,
      ILogger<DirectorModule> logger)
    {
      this.configuration = configuration;
      this.logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      var m = typeof(DirectorModule);

      var configSetCmd = new Command("set");
      configSetCmd.ConfigureFromMethod(m.GetMethod(nameof(SetConfigValue)), this);

      var rootCmd = new RootCommand()
      {
        new Command("config")
        {
          configSetCmd
        }
      };

      return Task.CompletedTask;
    }

    public void SetConfigValue(string path, string value)
    {
      configuration[path] = value;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
  }
}