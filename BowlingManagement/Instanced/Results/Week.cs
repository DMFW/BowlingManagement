using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingManagement
{
  public class Week
  {

    private int _weekNo;
    private DateTime _date;
    private Dictionary<int, Fixture> _dctFixtures;

    private Dictionary<Player, PlayerWeek> _playerWeekDatabase;
    private Dictionary<Player, PlayerWeek> _playerWeekCalculated;

    public Week(int weekNo)
    {
      _dctFixtures = new Dictionary<int, Fixture>();
      _weekNo = weekNo;
    }

    public int WeekNo
    {
      get { return _weekNo; }
      set { _weekNo = value; }
    }

    public DateTime Date
    {
      get { return _date; }
      set { _date = value; }
    }

    public Dictionary<int, Fixture> Fixtures
    {
      get { return _dctFixtures; }
    }

    public PlayerWeek PlayerWeekDatabase(Player player)
    {
      if (!_playerWeekDatabase.ContainsKey(player))
      {
        PlayerWeek playerWeek = new PlayerWeek(player, this);
        _playerWeekDatabase.Add(player, playerWeek); 
      }
      return _playerWeekDatabase[player];
    }

    public PlayerWeek PlayerWeekCalculated(Player player)
    {
      if (!_playerWeekCalculated.ContainsKey(player))
      {
        PlayerWeek playerWeek = new PlayerWeek(player, this);
        _playerWeekCalculated.Add(player, playerWeek);
      }
      return _playerWeekCalculated[player];
    }

    public void ValidateFixtures()
    {

      // Just check we have no team referenced more than once in the same week.
      // This is only a small part of validating fixtures but it is the part that belongs with this class.

      Dictionary<Team, string> dctTeamPlaying = new Dictionary<Team, string>();
      foreach (Fixture fixture in _dctFixtures.Values)
      {
        if (dctTeamPlaying.ContainsKey(fixture.HomeTeam))
        {
          BowlingManagement.Static.Logs.Status.AddLogMessage("Week " + _weekNo + " has more than one fixture for " + fixture.HomeTeam.Name + " [" + fixture.HomeTeam.Number + "]", Log.MessageSeverity.Error);
        }
        else
        {
          dctTeamPlaying.Add(fixture.HomeTeam, fixture.HomeTeam.Name);
        }
        if (dctTeamPlaying.ContainsKey(fixture.AwayTeam))
        {
          BowlingManagement.Static.Logs.Status.AddLogMessage("Week " + _weekNo + " has more than one fixture for " + fixture.AwayTeam.Name + " [" + fixture.AwayTeam.Number + "]", Log.MessageSeverity.Error);
        }
        else
        {
          dctTeamPlaying.Add(fixture.AwayTeam, fixture.AwayTeam.Name);
        }
      }
    }
  }
}
