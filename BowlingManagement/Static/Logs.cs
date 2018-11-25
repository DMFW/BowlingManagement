using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingManagement.Static
{
    static class Logs
    {
      private static BowlingManagement.Log _statusLog = new BowlingManagement.Log(@"C:\Logs\BowlingManagement.log");
      private static BowlingManagement.Log _updateSQLLog = new BowlingManagement.Log(@"C:\Logs\BowlingManagementSQL.log");

      public static Log Status
      {        
          get { return _statusLog; }
      }
      public static Log UpdateSQLLog
      {
          get { return _updateSQLLog; }
      }
    }
}
