using System;
using Emgu.TF.Lite;

namespace DBot.Perception
{
  public class DepthMapGenerator : IDisposable
  {
    const string MODEL_PATH = "";

    private Interpreter modelInterpreter;

    public DepthMapGenerator()
    {
      var modelBuffer = new FlatBufferModel(MODEL_PATH);
      modelInterpreter = new Interpreter(modelBuffer);
      modelInterpreter.AllocateTensors();
    }

    public void Generate()
    {

    }

    public void Dispose()
    {
      modelInterpreter.Dispose();
    }
  }
}