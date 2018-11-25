using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingManagement
{
  public class Player
  {
    private string _code;
    private string _firstName;
    private string _lastName;
    private string _sex;
    private int _seasonCFHcap;

    private Dictionary<int, PlayerWeek> _playerSeason;
   
    public string Code
    {
      get { return _code; }
      set { _code = value; }
    }

    public string FirstName
    {
      get { return _firstName; }
      set { _firstName = value; }
    }

    public string LastName
    {
      get { return _lastName; }
      set { _lastName = value; }
    }

    public string Sex
    {
      get { return _sex; }
      set { _sex = value; }
    }
    public int SeasonCFHcap
    {
      // The season carry forward handicap for the player.
      get { return _seasonCFHcap; }
      set { _seasonCFHcap = value; }
    }
    public Dictionary<int, PlayerWeek> PlayerSeason
    {
      get { return _playerSeason; }
    }
   
  }
}
