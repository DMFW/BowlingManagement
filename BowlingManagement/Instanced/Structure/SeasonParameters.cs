using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace BowlingManagement
{
  public class SeasonParameters
  {
    private bool _isCurrent;
    private string _year;
    private string _leagueName;
    private int _noTeams;
    private int _noWeeks;
    private int _noLanes;
    private int _startLane;
    private int _blindScore;
    private int _hcapBase;
    private int _hcapPercent;
    private int _noPlayers;
    private int _playersPerWeek;
    private int _teamGamePts;
    private int _teamSeriesPts;
    private int _playerGamePts;
    private int _playerSeriesPts;
    private int _cfHcapWeeks;
    private static Player _blindPlayer;
    private static PlayerGame _blindPlayerGame;

    private int _hcapMax;

    public string Year
    {
        get { return _year; }
        set { _year = value; }
    }

    public string LeagueName
    {
        get { return _leagueName; }
        set { _leagueName = value; }
    }

    public int NoTeams
    {
        get { return _noTeams; }
        set { _noTeams = value; }
    }

    public int NoWeeks
    {
        get { return _noWeeks; }
        set { _noWeeks = value; }
    }

    public int NoLanes
    {
        get { return _noLanes; }
        set { _noLanes = value; }
    }

    public int StartLane
    {
        get { return _startLane; }
        set { _startLane = value; }
    }

    public int BlindScore
    {
        get { return _blindScore; }
        set { _blindScore = value; }
    }

    public int HcapBase
    {
        get { return _hcapBase; }
        set { _hcapBase = value; }
    }

    public int HcapPercent
    {
        get { return _hcapPercent; }
        set { _hcapPercent = value; }
    }

    public int NoPlayers
    {
        get { return _noPlayers; }
        set { _noPlayers = value; }
    }

    public int PlayersPerWeek
    {
        get { return _playersPerWeek; }
        set { _playersPerWeek = value; }
    }

    public int TeamGamePts
    {
        get { return _teamGamePts; }
        set { _teamGamePts = value; }
    }

    public int TeamSeriesPts
    {
        get { return _teamSeriesPts; }
        set { _teamSeriesPts = value; }
    }

    public int PlayerGamePts
    {
        get { return _playerGamePts; }
        set { _playerGamePts = value; }
    }

    public int PlayerSeriesPts
    {
        get { return _playerSeriesPts; }
        set { _playerSeriesPts = value; }
    }

    public int CFHcapWeeks
    {
        get { return _cfHcapWeeks; }
        set { _cfHcapWeeks = value; }
    }

    public int HcapMax
    {
        get { return _hcapMax; }
        set { _hcapMax = value; }
    }

    public bool Load(string year)
    {

      string sqlLeagueParams;
      if (year == "Current")
      {
        sqlLeagueParams = "SELECT * from leagueparams order by Year desc";
      }
      else
      {
        sqlLeagueParams = "SELECT * from leagueparams where Year = '" + year + "'";
      }

      MySqlCommand cmdLeagueParams = new MySqlCommand(sqlLeagueParams);
      cmdLeagueParams.Connection = BowlingManagement.Static.Database.CNBowling;

      cmdLeagueParams.Prepare();

      MySqlDataReader rdrLeagueParams = cmdLeagueParams.ExecuteReader();
      if (!rdrLeagueParams.HasRows)
      {
          BowlingManagement.Static.Logs.Status.AddLogMessage("No league parameters in the database for year " + year,Log.MessageSeverity.Error);
          rdrLeagueParams.Close();
          return false;
      }
      else
      {
        rdrLeagueParams.Read();

        _year = rdrLeagueParams["Year"].ToString();
        _leagueName = rdrLeagueParams["leagueName"].ToString();
        _noTeams = Int32.Parse(rdrLeagueParams["noTeams"].ToString());
        _noLanes = Int32.Parse(rdrLeagueParams["noLanes"].ToString());
        _startLane = Int32.Parse(rdrLeagueParams["startLane"].ToString());
        _blindScore = Int32.Parse(rdrLeagueParams["blindScore"].ToString());
        _hcapBase = Int32.Parse(rdrLeagueParams["hcapBase"].ToString());
        _hcapPercent = Int32.Parse(rdrLeagueParams["hcapPercent"].ToString());
        // Ignore the scratch League switch. All leagues are alike in respect of calculations
        _noPlayers = Int32.Parse(rdrLeagueParams["noPlayers"].ToString());
        _playersPerWeek = Int32.Parse(rdrLeagueParams["playersPerWeek"].ToString());
        _teamGamePts = Int32.Parse(rdrLeagueParams["teamGamePts"].ToString());
        _teamSeriesPts = Int32.Parse(rdrLeagueParams["teamSeriesPts"].ToString());
        _playerGamePts = Int32.Parse(rdrLeagueParams["playerGamePts"].ToString());
        _playerSeriesPts = Int32.Parse(rdrLeagueParams["playerSeriesPts"].ToString());
        _cfHcapWeeks = Int32.Parse(rdrLeagueParams["cfHcapWeeks"].ToString());

        rdrLeagueParams.Close();
      }
      // Fields that aren't in the league paramaters table (yet)
      _hcapMax = 80;

      BowlingManagement.Static.Logs.Status.AddLogMessage("Season parameters for " + _leagueName + " " + _year + " loaded", Log.MessageSeverity.Info);
      return true;
    }

    Player BlindPlayer
    {
      get
      {
        if (_blindPlayer != null) return _blindPlayer;
        Player blindPlayer = new Player();
        blindPlayer.Code = "blind";
        blindPlayer.LastName = "Blind";
        return _blindPlayer;
      }
    }

    PlayerGame BlindPlayerGame
    {
      get
      {
        if (_blindPlayerGame != null) return _blindPlayerGame;
        _blindPlayerGame = new PlayerGame();
        _blindPlayerGame.Player = BlindPlayer;
        _blindPlayerGame.ScratchPinfall = 0;
        _blindPlayerGame.CFHcap = this.BlindScore;
        return _blindPlayerGame;
      }
    }
  }
}
