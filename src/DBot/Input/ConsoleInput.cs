using System;

namespace DBot.Input
{
  public class ConsoleInput
  {
    public event EventHandler<string> Received;

    public void OnReceived(string input) => Received?.Invoke(this, input);
  }
}