using MathNet.Numerics.LinearAlgebra;

namespace DBot.Perception
{
  public interface IDepthMapAcquirer
  {
    /// <summary>
    /// Returns a depth map of the current visible scene.
    /// </summary>
    Matrix<double> Acquire();
  }
}