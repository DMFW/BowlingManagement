using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingManagement
{
  public class LogMessageArgs : EventArgs
  {
    private string _message;
    private BowlingManagement.Log.MessageSeverity _severity;

    public LogMessageArgs(string message, BowlingManagement.Log.MessageSeverity severity)
    {
      _message = message;
      _severity = severity;
    }

    public string Message
    {
      get { return _message; }
      set { _message = value; }
    }

    public BowlingManagement.Log.MessageSeverity Severity
    {
      get { return _severity; }
      set { _severity = value; }
    }

  }
}
