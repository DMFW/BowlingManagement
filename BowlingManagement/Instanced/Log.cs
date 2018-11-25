using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingManagement
{

  public delegate void LogMessageHandler(object sender, LogMessageArgs e);

  public class Log
  {

    public enum MessageSeverity
    {
      Info=0,
      Warning=1,
      Error=2
    }

    private string _logPath;
    private bool _timeStampLog = false;

    public event LogMessageHandler LogMessage;
   
    public Log(string logPath)
    {
      _logPath = logPath;
    }

    public bool TimeStampLog
    {
      get { return _timeStampLog; }
      set { _timeStampLog = value; }
    }
    public void Clear()
    {
      try
      {
        System.IO.File.Delete(_logPath);
      }
      catch(Exception e)
      {
          // Ignore if we can't do it
      }
    }

    public void AddLogMessage(string message, MessageSeverity severity)
    {
      WriteLine(message);
      // Raise an event to any listeners that want to so something else with the message
      LogMessageArgs e = new LogMessageArgs(message, severity);
      LogMessage(this, e);
    }

    private void WriteLine(string logLine)
    {
      System.IO.StreamWriter sw = System.IO.File.AppendText(_logPath);
      try
      {
        if (_timeStampLog)
        {
          sw.WriteLine(DateTime.Now + " " + logLine);
        }
        else
        {
          sw.WriteLine(logLine);
        }
      }
      finally
      {
        sw.Close();
      }
    }
  }
}
