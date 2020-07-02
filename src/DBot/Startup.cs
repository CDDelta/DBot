using System;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using DBot.Orchestration;
using DBot.Input;
using DBot.Director;
using DBot.Perception;
using DBot.State;
using DBot.Planning;
using DBot.Control;

namespace DBot
{
  class Startup
  {
    static void Main(string[] args)
    {
      var host = CreateHostBuilder(args).UseConsoleLifetime().Build();
      host.RunAsync();

      var consoleInput = host.Services.GetService<ConsoleInput>();
      while (true)
      {
        var line = Console.ReadLine();
        consoleInput.OnReceived(line);
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        (new HostBuilder())
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureHostConfiguration(configBuilder =>
            {
              configBuilder.SetBasePath(Directory.GetCurrentDirectory());
              configBuilder.AddYamlFile("config.yaml", optional: false, reloadOnChange: true);
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
              logging.AddDebug();
            })
            .ConfigureServices((hostContext, services) =>
            {
              var config = hostContext.Configuration;
              services.AddOptions();

              services
                .AddHostedService<InputModule>()
                .AddSingleton<ConsoleInput>();

              services.AddHostedService<DirectorModule>();

              services
                .AddHostedService<PerceptionModule>()
                .Configure<PerceptionOptions>(config.GetSection(PerceptionOptions.Perception))
                .AddSingleton<PointCloudBuilder>();

              services.AddHostedService<StateModule>();

              services.AddHostedService<PlanningModule>();

              services.AddHostedService<ControlModule>();

              services.AddHostedService<OrchestrationModule>();
            });
  }
}
