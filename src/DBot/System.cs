using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Configuration;

using DBot.Input;
using DBot.Director;
using DBot.Perception;
using DBot.State;
using DBot.Planning;
using DBot.Control;

namespace DBot
{
  public class System
  {
    public IServiceProvider Services { get; }
    public ILogger Logger { get; }

    private readonly InputModule input;
    private readonly DirectorModule director;
    private readonly PerceptionModule perception;
    private readonly StateModule state;
    private readonly PlanningModule planning;
    private readonly ControlModule control;

    public System(IServiceCollection serviceCollection)
    {
      ConfigureServices(serviceCollection);

      var loggerFactory = new LoggerFactory();
      loggerFactory.AddProvider(new DebugLoggerProvider());
      Logger = loggerFactory.CreateLogger<System>();

      Logger.LogInformation("Initialising input module...");
      input = new InputModule();
      input.Initialise(serviceCollection);

      Logger.LogInformation("Initialising director module...");
      director = new DirectorModule();
      director.Initialise(serviceCollection);

      Logger.LogInformation("Initialising perception module...");
      perception = new PerceptionModule();
      perception.Initialise(serviceCollection);

      Logger.LogInformation("Initialising state module...");
      state = new StateModule();
      state.Initialise(serviceCollection);

      Logger.LogInformation("Initialising planning module...");
      planning = new PlanningModule();
      planning.Initialise(serviceCollection);

      Logger.LogInformation("Initialising control module...");
      control = new ControlModule();
      control.Initialise(serviceCollection);

      Logger.LogInformation("Starting update loop...");
      Services = serviceCollection.BuildServiceProvider();
      Start();

      Logger.LogInformation("DBot initialised successfully.");

      Console.ReadLine();
    }

    public async void Start()
    {
      while (true)
      {
        Update();
        await Task.Delay(8);
      }
    }

    public void Update()
    {
    }

    private void ConfigureServices(IServiceCollection serviceCollection)
    {
      IConfigurationBuilder configBuilder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddYamlFile("config.yaml", optional: false, reloadOnChange: true);

      IConfiguration config = configBuilder.Build();

      serviceCollection.AddOptions();
      serviceCollection.Configure<PerceptionOptions>(config.GetSection(PerceptionOptions.Perception));
    }
  }
}