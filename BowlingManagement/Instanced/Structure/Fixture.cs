using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace BowlingManagement
{
  public class Fixture
  {

    private Season _parentSeason;
    private int _week;
    private int _matchNo;

    private Team _homeTeam;
    private Team _awayTeam;

    private int[] _homeScratchPinfallTeam = new int[4];
    private int[] _awayScratchPinfallTeam = new int[4];

    private int[] _homeHcapPinfallTeam = new int[4];
    private int[] _awayHcapPinfallTeam = new int[4];

    private int[] _homeScratchPinfallPlayers = new int[4];
    private int[] _awayScratchPinfallPlayers = new int[4];

    private int[] _homeHcapPinfallPlayers = new int[4];
    private int[] _awayHcapPinfallPlayers= new int[4];

    private int[] _homeScratchPinfallCalculated = new int[4];
    private int[] _awayScratchPinfallCalculated = new int[4];

    private int[] _homeHcapPinfallCalculated = new int[4];
    private int[] _awayHcapPinfallCalculated = new int[4];

    private int[] _homeScratchPinfallError = new int[4];
    private int[] _awayScratchPinfallError = new int[4];

    private int[] _homeHcapPinfallError = new int[4];
    private int[] _awayHcapPinfallError = new int[4];

    private Dictionary<string, string> _dctHomePlayers = new Dictionary<string, string>();
    private Dictionary<string, string> _dctAwayPlayers = new Dictionary<string, string>();

    public Fixture(Season fixtureSeason)
    {
        _parentSeason = fixtureSeason;
    }

    public int Week
    {
        get { return _week; }
        set { _week = value; }
    }

    public int MatchNo
    {
        get { return _matchNo; }
        set { _matchNo = value; }
    }

    public Team HomeTeam
    {
      get { return _homeTeam; }
      set { _homeTeam = value; }
    }

    public Team AwayTeam
    {
      get { return _awayTeam; }
      set { _awayTeam = value; }
    }

    public void AddScratchPinfallTeam(Team team, int game, int pinfall)
    {
      if (team == _homeTeam)
      {
         _homeScratchPinfallTeam[game] = _homeScratchPinfallTeam[game] + pinfall;
      }

      if (team == _awayTeam)
      {
         _awayScratchPinfallTeam[game] = _awayScratchPinfallTeam[game] + pinfall;
      }

      if ((team != _homeTeam) && (team != _awayTeam))
      {
          BowlingManagement.Static.Logs.Status.AddLogMessage(String.Format("AddScratchPinfallTeam: Attempting to update fixture week {0} for {1} vs {2} with invalid team {3}", _week,_homeTeam.Code,_awayTeam.Code,team.Code),Log.MessageSeverity.Error);
      }              
    }
    public void AddHcapPinfallTeam(Team team, int game, int pinfall)
    {
        if (team == _homeTeam)
        {
            _homeHcapPinfallTeam[game] = _homeHcapPinfallTeam[game] + pinfall;
        }

        if (team == _awayTeam)
        {
            _awayHcapPinfallTeam[game] = _awayHcapPinfallTeam[game] + pinfall;
        }

        if ((team != _homeTeam) && (team != _awayTeam))
        {
            BowlingManagement.Static.Logs.Status.AddLogMessage(String.Format("AddHcapPinfallTeam: Attempting to update fixture week {0} for {1} vs {2} with invalid team {3}", _week, _homeTeam.Code, _awayTeam.Code, team.Code), Log.MessageSeverity.Error);
        }
    }

    public void AddScratchPinfallPlayers(Team team, int game, int pinfall)
    {
      if (team == _homeTeam)
      {
        _homeScratchPinfallPlayers[game] = _homeScratchPinfallPlayers[game] + pinfall;
      }

      if (team == _awayTeam)
      {
        _awayScratchPinfallPlayers[game] = _awayScratchPinfallPlayers[game] + pinfall;
      }

      if ((team != _homeTeam) && (team != _awayTeam))
      {
        BowlingManagement.Static.Logs.Status.AddLogMessage(String.Format("AddScratchPinfallPlayers: Attempting to update fixture week {0} for {1} vs {2} with invalid team {3}", _week, _homeTeam.Code, _awayTeam.Code, team.Code), Log.MessageSeverity.Error);
      }
    }
    public void AddHcapPinfallPlayers(Team team, int game, int pinfall)
    {
        if (team == _homeTeam)
        {
            _homeHcapPinfallPlayers[game] = _homeHcapPinfallPlayers[game] + pinfall;
        }

        if (team == _awayTeam)
        {
            _awayHcapPinfallPlayers[game] = _awayHcapPinfallPlayers[game] + pinfall;
        }

        if ((team != _homeTeam) && (team != _awayTeam))
        {
            BowlingManagement.Static.Logs.Status.AddLogMessage(String.Format("AddHcapPinfallPlayers: Attempting to update fixture week {0} for {1} vs {2} with invalid team {3}", _week, _homeTeam.Code, _awayTeam.Code, team.Code), Log.MessageSeverity.Error);
        }
    }

        public void AddPlayer(Team team, string playerCode)
        {
            if (team == _homeTeam)
            {
                if (_dctHomePlayers.ContainsKey(playerCode))
                { }
                else
                {
                    _dctHomePlayers.Add(playerCode, playerCode);
                }
            }

            if (team == _awayTeam)
            {
                if (_dctAwayPlayers.ContainsKey(playerCode))
                { }
                else
                {
                    _dctAwayPlayers.Add(playerCode, playerCode);
                }
            }

            if ((team != _homeTeam) && (team != _awayTeam))
            {
                BowlingManagement.Static.Logs.Status.AddLogMessage(String.Format("AddToPlayerCount: Attempting to update fixture week {0} for {1} vs {2} with invalid team {3}", _week, _homeTeam.Code, _awayTeam.Code, team.Code), Log.MessageSeverity.Error);
            }
        }

        public void CalculateCorrectTeamScore()
        {
            for (int i = 1; i < 4; i++)
            {
                _homeScratchPinfallCalculated[i] = _homeScratchPinfallPlayers[i];
                _awayScratchPinfallCalculated[i] = _awayScratchPinfallPlayers[i];

                _homeHcapPinfallCalculated[i] = _homeHcapPinfallPlayers[i] + ((2 - _dctHomePlayers.Count) * _parentSeason.Parameters.BlindScore);
                _awayHcapPinfallCalculated[i] = _awayHcapPinfallPlayers[i] + ((2 - _dctAwayPlayers.Count) * _parentSeason.Parameters.BlindScore);

                // Roll forward into the series
                _homeScratchPinfallCalculated[0] = _homeScratchPinfallCalculated[0] + _homeScratchPinfallCalculated[i];
                _awayScratchPinfallCalculated[0] = _awayScratchPinfallCalculated[0] + _awayScratchPinfallCalculated[i];

                // Derive game errors
                _homeScratchPinfallError[i] = _homeScratchPinfallTeam[i] - _homeScratchPinfallCalculated[i];
                _awayScratchPinfallError[i] = _awayScratchPinfallTeam[i] - _awayScratchPinfallCalculated[i];

                _homeHcapPinfallError[i] = _homeHcapPinfallTeam[i] - _homeHcapPinfallCalculated[i];
                _awayHcapPinfallError[i] = _awayHcapPinfallTeam[i] - _awayHcapPinfallCalculated[i];

            }

            // Derive series errors
            _homeScratchPinfallError[0] = _homeScratchPinfallTeam[0] - _homeScratchPinfallCalculated[0];
            _awayScratchPinfallError[0] = _awayScratchPinfallTeam[0] - _awayScratchPinfallCalculated[0];

            _homeHcapPinfallError[0] = _homeHcapPinfallTeam[0] - _homeHcapPinfallCalculated[0];
            _awayHcapPinfallError[0] = _awayHcapPinfallTeam[0] - _awayHcapPinfallCalculated[0];

        }

        public void OutputResults()
        {
            bool SQLCorrections = false;
            for (int i = 1; i < 4; i++)
            {
                if ((_homeScratchPinfallError[i] != 0) | _homeHcapPinfallError[i] !=0)
                {
                    BowlingManagement.Static.Logs.Status.AddLogMessage(string.Format("Home team scores for week {0} match {1} game {2} hometeam {3} are in error by {4} (scratch) and {5} (handicap)", _week, _matchNo, i,_homeTeam.Code, _homeScratchPinfallError[i], _homeHcapPinfallError[i]),Log.MessageSeverity.Error);
                    BowlingManagement.Static.Logs.Status.AddLogMessage(string.Format("Players {0} - Scratch Team/Player {1}/{2} Handicap Team/Player {3}/{4}", _dctHomePlayers.Count, _homeScratchPinfallTeam[i], _homeScratchPinfallPlayers[i], _homeHcapPinfallTeam[i], _homeHcapPinfallPlayers[i]), Log.MessageSeverity.Error);

                    BowlingManagement.Static.Logs.UpdateSQLLog.AddLogMessage("Update teamGame set scratchGame = " + _homeScratchPinfallCalculated[i] + ", hcapGame = " + _homeHcapPinfallCalculated[i] + " where week = " + _week + " and team = '" + _homeTeam.Code + "' and gameNo = " + i + ";", Log.MessageSeverity.Info);
                    SQLCorrections = true;
                }

                if ((_awayScratchPinfallError[i] != 0) | _awayHcapPinfallError[i] != 0)
                {
                    BowlingManagement.Static.Logs.Status.AddLogMessage(string.Format("Away team scores for week {0} match {1} game {2} awayteam {3} are in error by {4} (scratch) and {5} (handicap)", _week, _matchNo, i, _awayTeam.Code, _awayScratchPinfallError[i], _awayHcapPinfallError[i]), Log.MessageSeverity.Error);
                    BowlingManagement.Static.Logs.Status.AddLogMessage(string.Format("Players {0} - Scratch Team/Player {1}/{2} Handicap Team/Player {3}/{4}", _dctAwayPlayers.Count, _awayScratchPinfallTeam[i], _awayScratchPinfallPlayers[i], _awayHcapPinfallTeam[i], _awayHcapPinfallPlayers[i]), Log.MessageSeverity.Error);

                    BowlingManagement.Static.Logs.Status.AddLogMessage("Update teamGame set scratchGame = " + _awayScratchPinfallCalculated[i] + ", hcapGame = " + _awayHcapPinfallCalculated[i] + " where week = " + _week + " and team = '" + _awayTeam.Code + "' and gameNo = " + i + ";", Log.MessageSeverity.Info);
                    SQLCorrections = true;
                }
            }
            if (SQLCorrections == false)
            {
                BowlingManagement.Static.Logs.Status.AddLogMessage(string.Format("Fixture week {0} between {1} and {2} was valid", _week, _homeTeam.Code, _awayTeam.Code),Log.MessageSeverity.Info);
            }
            else
            {
                BowlingManagement.Static.Logs.Status.AddLogMessage(string.Format("Fixture week {0} between {1} and {2} was invalid", _week, _homeTeam.Code, _awayTeam.Code), Log.MessageSeverity.Error);
            }
        }

        public bool ScoringError
        {
            // If the errors in home and away pinfall (scratch and handicap) are equal for all three games
            // the team score will still be correct. Ideally, of course, all errors will be zero.
            get
            {
                bool error = false;
                for (int i = 1; i < 3; i++)
                {
                    if ((_homeScratchPinfallError[i] != 0) |
                        (_awayScratchPinfallError[i] !=0) |
                        (_homeHcapPinfallError[i] !=0 ) |
                        (_awayHcapPinfallError[i] !=0 ))
                    { error = true; }
                }

                return error;
            }
        }

        public bool ScoringDiscrepancy
        {
            // If the errors in home and away pinfall (scratch and handicap) are equal for all three games
            // the team score will still be correct. Ideally, of course, all errors will be zero.
            get
            {
                bool discrepancy = false;
                for (int i = 1; i < 3; i++)
                {
                    if ((_homeScratchPinfallError[i] != _awayScratchPinfallError[i]) |
                       (_homeHcapPinfallError[i] != _awayHcapPinfallError[i]))
                    { discrepancy = true; }
                }

                return discrepancy;
            }
        }

        public int HomeScratchPinfallError(int game)
        {
            return _homeScratchPinfallError[game];
        }
        public int AwayScratchPinfallError(int game)
        {
            return _awayScratchPinfallError[game];
        }
        public int HomeHcapPinfallError(int game)
        {
            return _homeHcapPinfallError[game];
        }
        public int AwayHcapPinfallError(int game)
        {
            return _awayHcapPinfallError[game];
        }

        public void CalculatedScore()
        { }
    }
}
