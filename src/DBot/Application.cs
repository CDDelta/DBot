using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using DBot.Input;
using DBot.Perception;
using DBot.State;
using DBot.Planning;
using DBot.Controls;

namespace DBot
{
  public class Application
  {
    public IServiceProvider Services { get; }
    public ILogger Logger { get; }

    private readonly InputModule input;
    private readonly PerceptionModule perception;
    private readonly StateModule state;
    private readonly PlanningModule planning;
    private readonly ControlsModule controls;

    public Application(IServiceCollection serviceCollection)
    {
      ConfigureServices(serviceCollection);
      Services = serviceCollection.BuildServiceProvider();
      Logger = Services.GetRequiredService<ILoggerFactory>()
              .CreateLogger<Application>();

      Logger.LogInformation("Initialising input module...");
      input = new InputModule();
      input.Initialise(serviceCollection);

      Logger.LogInformation("Initialising perception module...");
      perception = new PerceptionModule();
      perception.Initialise(serviceCollection);

      Logger.LogInformation("Initialising state module...");
      state = new StateModule();
      state.Initialise(serviceCollection);

      Logger.LogInformation("Initialising planning module...");
      planning = new PlanningModule();
      planning.Initialise(serviceCollection);

      Logger.LogInformation("Initialising controls module...");
      controls = new ControlsModule();
      controls.Initialise(serviceCollection);

      Logger.LogInformation("Starting update loop...");
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

    }
  }
}