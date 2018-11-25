using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace BowlingManagement.Static
{

  public delegate void DatabaseStateChangeHandler(DatabaseStateChangeArgs e);

  public static class Database
  {

    private static MySqlConnection _cnBowlingDB;
    public static event DatabaseStateChangeHandler DatabaseStateChange;

    public static bool Open(string connectionString)
    {
      try
      {
        if (_cnBowlingDB != null)
        {
          if (_cnBowlingDB.State == ConnectionState.Open) { _cnBowlingDB.Close(); }
          _cnBowlingDB.Dispose();
        }
        _cnBowlingDB = new MySqlConnection(connectionString);
        _cnBowlingDB.Open();
        if (_cnBowlingDB.State == ConnectionState.Open)
        {
          BowlingManagement.Static.Logs.Status.AddLogMessage("Database " + connectionString + " opened successfully.", Log.MessageSeverity.Info);
        }
        DatabaseStateChangeArgs e = new DatabaseStateChangeArgs(_cnBowlingDB.State == ConnectionState.Open);
        DatabaseStateChange(e);
        return (_cnBowlingDB.State == ConnectionState.Open);
      }
      catch(Exception e)
      {
        BowlingManagement.Static.Logs.Status.AddLogMessage("Database " + connectionString + " could not be opened." + Environment.NewLine + e.Message, Log.MessageSeverity.Error);
        if (_cnBowlingDB == null)
        {
          return false;
        }
        else
        {
          return (_cnBowlingDB.State == ConnectionState.Open);
        }
      }
    }

    public static bool Close()
    {
      try
      {
        if (_cnBowlingDB != null)
        {
          if (_cnBowlingDB.State == ConnectionState.Open) { _cnBowlingDB.Close(); }
          _cnBowlingDB.Dispose();
        }
        BowlingManagement.Static.Logs.Status.AddLogMessage("Database closed successfully.", Log.MessageSeverity.Info);
        DatabaseStateChangeArgs e = new DatabaseStateChangeArgs(_cnBowlingDB.State == ConnectionState.Open);
        DatabaseStateChange(e);
        return true;
      }
      catch (Exception e)
      {
        if (_cnBowlingDB == null)
        {
          return true;
        }
        else
        {
          return (_cnBowlingDB.State == ConnectionState.Closed);
        }
      }
    }
    public static MySqlConnection CNBowling
    {
      get { return _cnBowlingDB; }
    }
  }
}
