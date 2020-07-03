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
    private readonly ILogger<DirectorModule> logger;

    public DirectorModule(
      ConsoleInput consoleInput,
      ILogger<DirectorModule> logger)
    {
      this.consoleInput = consoleInput;
      this.logger = logger;

      var m = typeof(DirectorModule);

      rootCommand = new RootCommand()
      {
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

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
  }
}