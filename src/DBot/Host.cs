using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DBot
{
  class Host
  {
    static void Main(string[] args)
    {
      var serviceCollection = new ServiceCollection();
      ConfigureServices(serviceCollection);
      var system = new System(serviceCollection);
    }

    static private void ConfigureServices(IServiceCollection serviceCollection)
    {
      serviceCollection
        .AddLogging(logging =>
          {
            logging.AddDebug();
          });
    }
  }
}
