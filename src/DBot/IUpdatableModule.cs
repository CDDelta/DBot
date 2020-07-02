using System.Threading;
using System.Threading.Tasks;

namespace DBot
{
  public interface IUpdatableModule
  {
    Task UpdateAsync(CancellationToken cancellationToken);
  }
}