using System.CommandLine;
using System.CommandLine.DragonFruit;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using DBot.Input;

namespace DBot.Director
{
  public class DirectorModule : IHostedService
  {
    private readonly ConsoleInput consoleInput;
    private readonly RootCommand rootCommand;
    private readonly IConfiguration configuration;
    private readonly ILogger<DirectorModule> logger;

    public DirectorModule(
      ConsoleInput consoleInput,
      IConfiguration configuration,
      ILogger<DirectorModule> logger)
    {
      this.consoleInput = consoleInput;
      this.configuration = configuration;
      this.logger = logger;

      var m = typeof(DirectorModule);

      var configSetCmd = new Command("set");
      configSetCmd.ConfigureFromMethod(m.GetMethod(nameof(SetConfigValue)), this);

      rootCommand = new RootCommand()
      {
        new Command("config")
        {
          configSetCmd
        }
      };
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      consoleInput.Received += OnReceiveConsoleInput;

      return Task.CompletedTask;
    }

    private void OnReceiveConsoleInput(object sender, string input)
    {
      rootCommand.Invoke(input);
    }

    public void SetConfigValue(string path, string value)
    {
      configuration[path] = value;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
  }
}