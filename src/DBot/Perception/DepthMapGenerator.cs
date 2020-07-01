using System;
using MathNet.Numerics.LinearAlgebra;
using Emgu.TF.Lite;

namespace DBot.Perception
{
  public class DepthMapGenerator : IDisposable
  {
    /// <summary>
    /// The number of pixel steps to take for each sample of the depth map.
    /// </summary>
    const int DEPTH_MAP_STRIDE_LENGTH = 1;


    const string MODEL_PATH = "";

    private Interpreter modelInterpreter;

    public DepthMapGenerator()
    {
      var modelBuffer = new FlatBufferModel(MODEL_PATH);
      modelInterpreter = new Interpreter(modelBuffer);
      modelInterpreter.AllocateTensors();
    }

    public Matrix<double> Generate()
    {
      return null;
    }

    public void Dispose()
    {
      modelInterpreter.Dispose();
    }
  }
}