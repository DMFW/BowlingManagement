using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingManagement
{
  public class DatabaseStateChangeArgs : EventArgs
  {
    private bool _open;

    public DatabaseStateChangeArgs(bool open)
    {
      _open = open;
    }

    public bool Open
    {
      get { return _open; }
      set { _open = value; }
    }

  }
}
