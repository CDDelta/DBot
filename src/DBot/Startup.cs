using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

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
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureHostConfiguration(configBuilder =>
            {
              //configBuilder.AddYamlFile("config.yaml", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((hostContext, services) =>
            {
              var config = hostContext.Configuration;
              services
                .AddOptions()
                .Configure<PerceptionOptions>(config.GetSection(PerceptionOptions.Perception));

              services.AddHostedService<InputModule>();

              services.AddHostedService<DirectorModule>();

              services
                .AddHostedService<PerceptionModule>()
                .AddSingleton(typeof(PointCloudBuilder));

              services.AddHostedService<StateModule>();

              services.AddHostedService<PlanningModule>();

              services.AddHostedService<ControlModule>();
            });
  }
}
