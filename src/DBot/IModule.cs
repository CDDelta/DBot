using Microsoft.Extensions.DependencyInjection;

namespace DBot
{
  public interface IModule
  {
    void Initialise(IServiceCollection serviceCollection);
  }
}