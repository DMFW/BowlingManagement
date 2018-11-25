using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace BowlingManagement
{
  public class Season
  {
    private SeasonParameters _parameters;
    private bool _isCurrentSeason;
    private bool _reflectsDatabase;
     
    private string _year;
    private Dictionary<int, Team> _dctTeamsByNumber;
    private Dictionary<string, Team> _dctTeamsByCode;
    private Dictionary<string, Player> _dctPlayers;

    private Dictionary<int, Week> _dctWeeks;
    private List<Fixture> _lstFixtures;

    private Dictionary<string, PlayerWeek> _dctPlayerSeason;
    private Dictionary<string, TeamSeason> _dctTeamSeason;

    #region Constructors
    public Season()
    {
      _parameters=new SeasonParameters();
    }
    #endregion

    public Season ShallowCopy()
    {
      return (Season)this.MemberwiseClone();
    }
    public SeasonParameters Parameters
    {
      get { return _parameters; }
      set { _parameters = value; }
    }

    public bool IsCurrentSeason
    {
      get { return _isCurrentSeason; }
      set { _isCurrentSeason = value; }
    }

    public bool ReflectsDatabase
    {
      get { return _reflectsDatabase; }
    }

    #region LoadStructure

    // This region is responsible for loading the structural data about a season from the database
    // which is an an "expected" relationship, so that later validation can mainly
    // use the resulting memory structures.

    // The structural data is defined as the league parameters, the teams, the players and the fixtures.
    // Basically it is everything except game data.

    public void LoadStructure(bool isCurrentSeason, string year)
    {

      _isCurrentSeason = isCurrentSeason;
      _year = year;
     
      LoadTeams();
      LoadFixtures();
    }

    private void LoadTeams()
    {
      try
      {

        string sqlTeams;

        _dctPlayers = new Dictionary<string, Player>();
        _dctTeamsByNumber = new Dictionary<int, Team>();
        _dctTeamsByCode = new Dictionary<string, Team>();

        if (_isCurrentSeason)
        {
          sqlTeams = "SELECT * from teamnumbers inner join teams on teamnumbers.team = teams.teamCode";
        }
        else
        {
          sqlTeams = "SELECT distinct team from teamseasonhist where year = '" + _parameters.Year + "' inner join teams on teamseasonhist.team = teams.teamCode";
        }

        MySqlCommand cmdTeams = new MySqlCommand(sqlTeams);
        cmdTeams.Connection = Static.Database.CNBowling;
        cmdTeams.Prepare();

        MySqlDataReader rdrTeam = cmdTeams.ExecuteReader();
        if (!rdrTeam.HasRows)
        {
          BowlingManagement.Static.Logs.Status.AddLogMessage("No teams in the database",Log.MessageSeverity.Error);
          rdrTeam.Close();
        }
        else
        {
          while (rdrTeam.Read())
          {
            Team team = new Team();
            team.Code = rdrTeam["team"].ToString();
            team.Name = rdrTeam["teamName"].ToString();
            team.Number = Int16.Parse(rdrTeam["number"].ToString());
            _dctTeamsByNumber.Add(team.Number, team);
            _dctTeamsByCode.Add(team.Code, team);
          }
        }
        rdrTeam.Close();
        BowlingManagement.Static.Logs.Status.AddLogMessage(_dctTeamsByCode.Count + " teams initialised", Log.MessageSeverity.Info);

        foreach (Team team in _dctTeamsByCode.Values)
        {
          team.LoadPlayers(_isCurrentSeason);
          foreach (Player player in team.Players.Values)
          {
            if (_dctPlayers.ContainsKey(player.Code))
            {
              BowlingManagement.Static.Logs.Status.AddLogMessage(player.Code + " has been added to multiple teams.", Log.MessageSeverity.Warning);
            }
            else
            { 
              _dctPlayers.Add(player.Code, player);
            }
          }
        }

        BowlingManagement.Static.Logs.Status.AddLogMessage(_dctTeamsByCode.Count + " teams players loaded", Log.MessageSeverity.Info);

      }
      catch(Exception e)
      {
        BowlingManagement.Static.Logs.Status.AddLogMessage("Error loading teams", Log.MessageSeverity.Error);
        BowlingManagement.Static.Logs.Status.AddLogMessage(e.Message, Log.MessageSeverity.Error);
      }
    }

    private void LoadFixtures()
    {
      try
      {
        Week week;
        Fixture fixture;
        string sqlFixtures;

        // Fixtures are added to a list of all fixtures and an object structure based on weeks.

        _lstFixtures = new List<Fixture>();
        _dctWeeks = new Dictionary<int, Week>();

        if (_isCurrentSeason)
        {
          sqlFixtures = "SELECT fixtures.week, fixtures.matchNo, fixtures.team1, fixtures.team2, hometeam.team AS hometeam, awayteam.team AS awayteam "
            + "FROM fixtures " 
            + "INNER JOIN teamnumbers hometeam ON fixtures.team1 = hometeam.number "
            + "INNER JOIN fixturedates on fixtures.week = fixturesdates.week"
            + "teamnumbers awayteam ON fixtures.team2 = awayteam.number";
        }
        else
        {
          // To be determined
          sqlFixtures = "";
        }
        
        MySqlCommand cmdFixtures = new MySqlCommand(sqlFixtures);
        cmdFixtures.Connection = Static.Database.CNBowling;
        cmdFixtures.Prepare();

        MySqlDataReader rdrFixture = cmdFixtures.ExecuteReader();
        if (!rdrFixture.HasRows)
        {
          BowlingManagement.Static.Logs.Status.AddLogMessage("No fixtures in the database", Log.MessageSeverity.Error);
          rdrFixture.Close();
        }
        else
        {
          while (rdrFixture.Read())
          {
            fixture = new Fixture(this);
            fixture.Week = Int32.Parse(rdrFixture["week"].ToString());
            fixture.MatchNo = Int32.Parse(rdrFixture["matchNo"].ToString());

            if (_dctTeamsByNumber.ContainsKey(Int16.Parse(rdrFixture["team1"].ToString())))
            { fixture.HomeTeam = _dctTeamsByNumber[Int16.Parse(rdrFixture["team1"].ToString())]; }

            if (_dctTeamsByNumber.ContainsKey(Int16.Parse(rdrFixture["team2"].ToString())))
            { fixture.AwayTeam = _dctTeamsByNumber[Int16.Parse(rdrFixture["team2"].ToString())]; }

            if (_dctWeeks.ContainsKey(fixture.Week))
            {
              week = _dctWeeks[fixture.Week];
            }
            else
            {
              week = new Week(fixture.Week);
              DateTime weekDate;
              if (DateTime.TryParse(rdrFixture["date"].ToString(), out weekDate))
              {
                week.Date = weekDate;
              }
              else
              {
                BowlingManagement.Static.Logs.Status.AddLogMessage("Week " + week.WeekNo + " has an invalid date", Log.MessageSeverity.Error);
              }
              _dctWeeks.Add(week.WeekNo, week);
            }
            week.Fixtures.Add(fixture.MatchNo, fixture);
            _lstFixtures.Add(fixture);
          }
          rdrFixture.Close();
        }
        BowlingManagement.Static.Logs.Status.AddLogMessage(_dctWeeks.Count + " weeks loaded with " + _lstFixtures.Count + " fixtures.", Log.MessageSeverity.Info);
      }
      catch (Exception e)
      {
        BowlingManagement.Static.Logs.Status.AddLogMessage(e.Message, Log.MessageSeverity.Error);
      }
    }

    #endregion

    #region LoadPlayData

    // This region is responsible for loading all playing data.
    // Database and calculated season objects start with the same base objects loaded here,
    // before the calculated season creates its own copies of everything that can be re-derived
    // (which is everything except the scratch player game scores).

    public void LoadPlayData()
    {
      LoadPlayerGames();
    }
     
    private void LoadPlayerGames()
    {
      string sqlPlayerGames;
      if (_isCurrentSeason)
      {
        sqlPlayerGames = "SELECT * FROM playergame "
          + "LEFT JOIN teamweeklylineup ON playergame.player = teamweeklylineup.player AND playergame.week = teamweeklylineup.week";
      }
      else
      {
        // To be determined
        sqlPlayerGames = "";
      }

      MySqlCommand cmdPlayerGames = new MySqlCommand(sqlPlayerGames);
      cmdPlayerGames.Connection = Static.Database.CNBowling;
      cmdPlayerGames.Prepare();

      MySqlDataReader rdrPlayerGames = cmdPlayerGames.ExecuteReader();
      if (!rdrPlayerGames.HasRows)
      {
        BowlingManagement.Static.Logs.Status.AddLogMessage("No player games in the database", Log.MessageSeverity.Warning);
        rdrPlayerGames.Close();
      }
      else
      {
        while (rdrPlayerGames.Read())
        {
          if (_dctPlayers.ContainsKey(rdrPlayerGames["player"].ToString()))
          {
            Player player = _dctPlayers[rdrPlayerGames["player"].ToString()];


            Team team = new Team();
            team.Code = rdrTeam["team"].ToString();
            team.Name = rdrTeam["teamName"].ToString();
            team.Number = Int16.Parse(rdrTeam["number"].ToString());
            _dctTeamsByNumber.Add(team.Number, team);
            _dctTeamsByCode.Add(team.Code, team);
          }
          else
          {
            BowlingManagement.Static.Logs.Status.AddLogMessage("Game found for unregistered player " + rdrPlayerScratchGames["player"].ToString() + " in week " + rdrPlayerScratchGames["week"].ToString(), Log.MessageSeverity.Warning);
          }
        }
      }
      rdrPlayerGames.Close();

    }

    #endregion

   
    #region Recalculate

    // Calculate everything from scratch, assuming only that carry forward handicaps
    // and individual player scratch scores are correct and everything else is derived.

    public void Recalculate()
    {
      CreateCalculationObjects();
    }

    private void CreateCalculationObjects()
    {
      _reflectsDatabase = false;
    }

    #endregion

    #region Validate

    // Validation will mainly use objects in memory, prepared by the Load methods.
    // In addition, validation will still deploy some SQL, but only for picking up the unexpected
    // such as free floating data that wasn't included in loaded inner joins and similar conceptual analysis.
    public void Validate()
    {
      ValidateFixtures();
      ValidatePlayerTeamRegistration();
      ValidatePlayerPinfallHistory();
      ValidateTeamPinfallHistory();
      ValidateMatchScores();
    }

    private void ValidateFixtures()
    {

      BowlingManagement.Static.Logs.Status.AddLogMessage("Validating fixtures", Log.MessageSeverity.Info);

      foreach (Week week in _dctWeeks.Values)
      {
        // Validate fixtures within each week
        week.ValidateFixtures();
      }

      // Now we should go on to validate more global properties of the fixture set
      // such as, does every team have the same number of home and away games,
      // does every team have the same number of games, does every team play every other team
      // the same number of times, is the lane balancing correct etc... etc...

      // This is to be coded later.

    }

    private void ValidatePlayerTeamRegistration()
    {

      // Validate that we do not have scores from players who are not registered with teams
      // There are limited circumstances when this is permitted (if a player is de-registered)
      // so an error of this kind will be treated as a warning.

      ValidatePlayerTeamRegistration_UnknownPlayers();

      // Validate that for any given team we only have the expected number of players, playing for them
      // in any given week. This will pick up orphaned score records caused where the web site
      // fails to delete records from scores assigned initially to the wrong player.

      ValidatePlayerTeamRegistration_ExcessPlayers();
    }

    private void ValidatePlayerTeamRegistration_UnknownPlayers()
    {

      string sUnreqisteredPlayersSQL;
      string sDeleteSQLTemplate;
      string sDeleteSQL;
      string sMessageTemplate;
      string sMessage;

      BowlingManagement.Static.Logs.Status.AddLogMessage("Validating player team registration", Log.MessageSeverity.Info);

      if (_isCurrentSeason)
      {
        sUnreqisteredPlayersSQL = "Select week, player as activeplayer, 'playergame' as filetype from playergame where player not in (select distinct player from teamplayers) union select week, player as activeplayer, 'playerseason' as filetype from playerseason where player not in (select distinct player from teamplayers) order by activeplayer";
      }
      else
      {
        sUnreqisteredPlayersSQL = ""; // To be completed
      }

      MySqlCommand cmdUnregisteredPlayers = new MySqlCommand(sUnreqisteredPlayersSQL);
      cmdUnregisteredPlayers.Connection = Static.Database.CNBowling;
      cmdUnregisteredPlayers.Prepare();

      MySqlDataReader rdrUnregisteredPlayers = cmdUnregisteredPlayers.ExecuteReader();
      if (!rdrUnregisteredPlayers.HasRows)
      {
        BowlingManagement.Static.Logs.Status.AddLogMessage("No unregistered players found", Log.MessageSeverity.Info);
      }
      else
      {

        sDeleteSQLTemplate = "Delete from {0} where week = {1} and player = '{2}';";
        sMessageTemplate = "Unregistered player {0} found on {1} in week {2};";

        while (rdrUnregisteredPlayers.Read())
        {
          string sDeleteFile = rdrUnregisteredPlayers["filetype"].ToString();
          string sDeleteWeek = rdrUnregisteredPlayers["week"].ToString();
          string sDeletePlayer = rdrUnregisteredPlayers["activeplayer"].ToString();
          sDeleteSQL = string.Format(sDeleteSQLTemplate, sDeleteFile, sDeleteWeek, sDeletePlayer);
          sMessage = string.Format(sMessageTemplate, sDeletePlayer, sDeleteFile, sDeleteWeek);
          BowlingManagement.Static.Logs.Status.AddLogMessage(sMessage, Log.MessageSeverity.Warning);
          BowlingManagement.Static.Logs.UpdateSQLLog.AddLogMessage(sDeleteSQL, Log.MessageSeverity.Warning);
        }
      }
      rdrUnregisteredPlayers.Close();
    }

    private void ValidatePlayerTeamRegistration_ExcessPlayers()
    {
      //
    }
    private void ValidatePlayerPinfallHistory()
    {

      // Assuming only that the carry forward handicap is correct, and scratch scores are correct
      // validate that each player game has the correct handicap pinfall.
      // Check also that we do not have spurious player records.

      BowlingManagement.Static.Logs.Status.AddLogMessage("Validating player pinfall history", Log.MessageSeverity.Info);
      return;

      foreach (Team team in _dctTeamsByNumber.Values)
      {
        foreach (Player player in team.Players.Values)
        {

          // First read the raw game data
          string sqlPlayerGames = "SELECT * from playergame where player=@playerCode ORDER BY week, gameNo";
          MySqlCommand cmdPlayerGames = new MySqlCommand(sqlPlayerGames);
          cmdPlayerGames.Parameters.Add(new MySqlParameter("@playerCode", MySqlDbType.String));
          cmdPlayerGames.Connection = Static.Database.CNBowling;

          MySqlDataReader rdrPlayerGames = cmdPlayerGames.ExecuteReader();
          if (!rdrPlayerGames.HasRows)
          {
            // This is fine, the player just hasn't played yet.
            rdrPlayerGames.Close();
            return;
          }
          else
          {
            while (rdrPlayerGames.Read())
            {
              Week week;
              if (_dctWeeks.ContainsKey(Int32.Parse(rdrPlayerGames["week"].ToString())))
              {
                week = _dctWeeks[Int32.Parse(rdrPlayerGames["week"].ToString())];
                PlayerWeek playerWeek = week.PlayerWeekDatabase(player);
                playerWeek.AddScratchScore(Int32.Parse(rdrPlayerGames["gameNo"].ToString()), Int32.Parse(rdrPlayerGames["scratchGame"].ToString()));
              }
              else
              {
                BowlingManagement.Static.Logs.Status.AddLogMessage("Games found for " + rdrPlayerGames["player"] + " in an invalid week " + rdrPlayerGames["week"].ToString(), Log.MessageSeverity.Error);
              }
            }
          }

          Dictionary<string, PlayerWeek> dctPlayerSeason = new Dictionary<string, PlayerWeek>();

          string sqlPlayerSeason = "SELECT * from playerseason where player=@playerCode ORDER BY week";
          MySqlCommand cmdPlayerSeason = new MySqlCommand(sqlPlayerSeason);
          cmdPlayerSeason.Parameters.Add(new MySqlParameter("@playerCode", MySqlDbType.String));
          cmdPlayerSeason.Connection = Static.Database.CNBowling;
          cmdPlayerSeason.Prepare();

          MySqlDataReader rdrPlayerSeason = cmdPlayerSeason.ExecuteReader();
          if (!rdrPlayerSeason.HasRows)
          {
            // This is fine, the player just hasn't played yet.
            rdrPlayerSeason.Close();
            return;
          }
          else
          {
            while (rdrPlayerSeason.Read())
            {
              
            }
          }
        }
      }

    }

    private void ValidateTeamPinfallHistory()
    {
      // Working with the corrected pinfall history of individual players
      // check that the team total pin falls for scratch and handicap are correct and rolled forward properly. 
    }

    private void ValidateMatchScores()
    {

      // This routine assumes we now have valid player and team pinfall history for handicap and scratch.
      // Recalculate all personal and team match scores and validate against the database.

      try
      {
          _dctTeamSeason = new Dictionary<string, TeamSeason>();

          string sqlTeamScores = "SELECT * from teamgame ORDER BY week, team";
          MySqlCommand cmdTeamScores = new MySqlCommand(sqlTeamScores);
          cmdTeamScores.Parameters.Add(new MySqlParameter("@week", MySqlDbType.Int16));
          cmdTeamScores.Parameters.Add(new MySqlParameter("@team", MySqlDbType.String));
          cmdTeamScores.Connection = Static.Database.CNBowling;
          cmdTeamScores.Prepare();

          MySqlDataReader rdrTeamScore = cmdTeamScores.ExecuteReader();
          if (!rdrTeamScore.HasRows)
          {
            BowlingManagement.Static.Logs.Status.AddLogMessage("No team game data found",Log.MessageSeverity.Warning);
            rdrTeamScore.Close();
            return;
          }
          else
          {
            while (rdrTeamScore.Read())
            {

            Team workingTeam = _dctTeamsByCode.Values.SingleOrDefault
            (t => (t.Code == rdrTeamScore["team"].ToString()));

            Fixture workingFixture = _lstFixtures.SingleOrDefault
                (f =>
                ((f.Week == Int16.Parse(rdrTeamScore["week"].ToString())) &&
                ((f.HomeTeam.Code == rdrTeamScore["team"].ToString()) || (f.AwayTeam.Code == rdrTeamScore["team"].ToString()))));

                if (workingFixture != null)
                {
                    workingFixture.AddScratchPinfallTeam(workingTeam, Int16.Parse(rdrTeamScore["gameNo"].ToString()), Int32.Parse(rdrTeamScore["scratchGame"].ToString()));
                    workingFixture.AddHcapPinfallTeam(workingTeam, Int16.Parse(rdrTeamScore["gameNo"].ToString()), Int32.Parse(rdrTeamScore["hcapGame"].ToString()));
                }
                else
                {
                    BowlingManagement.Static.Logs.Status.AddLogMessage(string.Format("Can't find fixture to update team game data for week {0}, team {1}", rdrTeamScore["week"].ToString(), rdrTeamScore["team"].ToString()), Log.MessageSeverity.Error);
                }

            }
            rdrTeamScore.Close();
        }
      }
      catch (Exception e)
      {
          BowlingManagement.Static.Logs.Status.AddLogMessage(e.Message, Log.MessageSeverity.Error);
      }
    }
    public void UpdateResultsFromPlayerData()
    {
      try
      {
          _dctTeamSeason = new Dictionary<string, TeamSeason>();
          _dctPlayerSeason = new Dictionary<string, PlayerWeek>();

          string sqlPlayerData = "SELECT teamweeklylineup.team, teamweeklylineup.week, teamweeklylineup.playerNo, teamweeklylineup.player, playergame.scratchGame, playergame.gameNo, playergame.hcapGame " +
                          "FROM teamweeklylineup INNER JOIN " +
                          "playergame ON teamweeklylineup.week = playergame.week AND teamweeklylineup.player = playergame.player " +
                          "ORDER BY teamweeklylineup.team, teamweeklylineup.week, playergame.gameNo, teamweeklylineup.playerNo";

          MySqlDataAdapter adr = new MySqlDataAdapter(sqlPlayerData, Static.Database.CNBowling);
          adr.SelectCommand.CommandType = CommandType.Text;
          DataTable dt = new DataTable();
          adr.Fill(dt); //opens and closes the DB connection automatically (fetches from pool)

          foreach (DataRow dr in dt.Rows)
          {
            PlayerWeek player;
            if (_dctPlayerSeason.ContainsKey(dr["player"].ToString()))
            {
                playerSeason = _dctPlayerSeason[dr["player"].ToString()];
            }
            else
            {
               // playerSeason = new PlayerSeason(dr["player"].ToString());
               // _dctPlayerSeason.Add(playerSeason.Code, playerSeason);
            }

            Team workingTeam = _dctTeamsByCode.Values.SingleOrDefault
                    (t => t.Code == dr["team"].ToString());

            Fixture workingFixture = _lstFixtures.SingleOrDefault
                  (f =>
                  ((f.Week == Int16.Parse(dr["week"].ToString())) &&
                  ((f.HomeTeam.Code == dr["team"].ToString()) || (f.AwayTeam.Code == dr["team"].ToString()))));

              if (workingFixture != null)
              {
                  workingFixture.AddScratchPinfallPlayers(workingTeam, Int16.Parse(dr["gameNo"].ToString()), Int32.Parse(dr["scratchGame"].ToString()));
                  workingFixture.AddHcapPinfallPlayers(workingTeam, Int16.Parse(dr["gameNo"].ToString()), Int32.Parse(dr["hcapGame"].ToString()));
                  workingFixture.AddPlayer(workingTeam, dr["player"].ToString());
              }
              else
              {
                  BowlingManagement.Static.Logs.Status.AddLogMessage(string.Format("Can't find fixture to update player game data for week {0}, team {1}", dr["week"].ToString(), dr["team"].ToString()),Log.MessageSeverity.Error);
              }
          }
      }
      catch (Exception ex)
      {
          BowlingManagement.Static.Logs.Status.AddLogMessage(ex.Message, Log.MessageSeverity.Error);
      }
    }
    public void AnalyseFixtures()
    {
        int invalidGameCount = 0;
        int validGameCount = 0;
        int lastInvalidWeek = 0;
        TeamSeason homeTeamSeason;
        TeamSeason awayTeamSeason;

        foreach (Fixture fixture in _lstFixtures)
        {
            fixture.CalculateCorrectTeamScore();
            fixture.OutputResults();

            if (_dctTeamSeason.ContainsKey(fixture.HomeTeam.Code))
            {
                homeTeamSeason = _dctTeamSeason[fixture.HomeTeam.Code];
            }
            else
            {
                homeTeamSeason = new TeamSeason();
                homeTeamSeason.Team = fixture.HomeTeam.Code;
                _dctTeamSeason.Add(homeTeamSeason.Team, homeTeamSeason);
            }

            if (_dctTeamSeason.ContainsKey(fixture.AwayTeam.Code))
            {
                awayTeamSeason = _dctTeamSeason[fixture.AwayTeam.Code];
            }
            else
            {
                awayTeamSeason = new TeamSeason();
                awayTeamSeason.Team = fixture.AwayTeam.Code;
                _dctTeamSeason.Add(awayTeamSeason.Team, awayTeamSeason);
            }

            homeTeamSeason.UpdateWithFixture(fixture);
            awayTeamSeason.UpdateWithFixture(fixture);

        }

        invalidGameCount = _lstFixtures.Count(f => f.ScoringError == true);
        validGameCount = _lstFixtures.Count - invalidGameCount;
        lastInvalidWeek = _lstFixtures.Where(f => f.ScoringError == true).Max(f => f.Week);

        BowlingManagement.Static.Logs.Status.AddLogMessage(string.Format("SUMMARY: Total Valid Games {0}, Total Invalid Games {1}, Last Invalid Week {2}", validGameCount, invalidGameCount, lastInvalidWeek),Log.MessageSeverity.Error);
        List<Fixture> lstErrorFixtures = _lstFixtures.Where(f => (f.ScoringDiscrepancy == true)).ToList();

        if (lstErrorFixtures.Count == 0)
        {
            BowlingManagement.Static.Logs.Status.AddLogMessage(string.Format("SUMMARY: No games have inconsistent errors that would affect the score"), Log.MessageSeverity.Error);
        }
        else
        {
            foreach (Fixture errorFixture in lstErrorFixtures)
            {
                BowlingManagement.Static.Logs.Status.AddLogMessage(string.Format("ERROR: Fixture week {0} for match {1} has different score errors on the home ({2}) and away ({3}) teams", errorFixture.Week, errorFixture.MatchNo, errorFixture.HomeTeam.Code, errorFixture.AwayTeam.Code),Log.MessageSeverity.Error);
                BowlingManagement.Static.Logs.Status.AddLogMessage(string.Format("Scratch Home/Away errors are {0},{1} : {2},{3} : {4},{5}", errorFixture.HomeScratchPinfallError(1), errorFixture.AwayScratchPinfallError(1), errorFixture.HomeScratchPinfallError(2), errorFixture.AwayScratchPinfallError(2), errorFixture.HomeScratchPinfallError(3), errorFixture.AwayScratchPinfallError(3)),Log.MessageSeverity.Error);
                BowlingManagement.Static.Logs.Status.AddLogMessage(string.Format("Handicap Home/Away errors are {0},{1} : {2},{3} : {4},{5}", errorFixture.HomeHcapPinfallError(1), errorFixture.AwayHcapPinfallError(1), errorFixture.HomeHcapPinfallError(2), errorFixture.AwayHcapPinfallError(2), errorFixture.HomeHcapPinfallError(3), errorFixture.AwayHcapPinfallError(3)), Log.MessageSeverity.Error);
            }
        }
    }
    #endregion
  }
}
