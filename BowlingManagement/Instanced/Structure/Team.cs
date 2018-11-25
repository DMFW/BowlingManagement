using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BowlingManagement
{
  public class Team
  {

    private short _number;
    private string _code;
    private string _name;

    private Dictionary<string, Player> _teamPlayers;

    public short Number
    {
      get { return _number; }
      set { _number = value; }
    }

    public string Code
    {
      get { return _code; }
      set { _code = value; }
    }

    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    public Dictionary<string, Player> Players
    {
      get { return _teamPlayers; }
    }

    public void LoadPlayers(bool isCurrentSeason)
    { 

      string sqlTeamPlayers;
      MySqlDataReader rdrTeamPlayers = null;

      try
      {
        _teamPlayers = new Dictionary<string, Player>();
        if (isCurrentSeason)
        {
          sqlTeamPlayers = "SELECT * from teamplayers inner join players on teamplayers.player = players.playerCode where team = '" + _code + "'";

          MySqlCommand cmdTeamPlayers = new MySqlCommand(sqlTeamPlayers);
          cmdTeamPlayers.Connection = Static.Database.CNBowling;
          cmdTeamPlayers.Prepare();

          rdrTeamPlayers = cmdTeamPlayers.ExecuteReader();
          if (!rdrTeamPlayers.HasRows)
          {
            BowlingManagement.Static.Logs.Status.AddLogMessage("No players found for team " + _name, Log.MessageSeverity.Error);
            rdrTeamPlayers.Close();
          }
          else
          {
            int cfHcap = -1;
            while (rdrTeamPlayers.Read())
            {
              Player player = new Player();
              player.Code = rdrTeamPlayers["player"].ToString();
              player.FirstName = rdrTeamPlayers["firstName"].ToString();
              player.LastName = rdrTeamPlayers["lastName"].ToString();
              player.Sex = rdrTeamPlayers["sex"].ToString();
              if (!Int32.TryParse(rdrTeamPlayers["hcap"].ToString(), out cfHcap)) { cfHcap = -1; }
              player.SeasonCFHcap = cfHcap;
              _teamPlayers.Add(player.Code, player);
            }
          }
          rdrTeamPlayers.Close();
          BowlingManagement.Static.Logs.Status.AddLogMessage(_teamPlayers.Count + " players loaded for team " + _name, Log.MessageSeverity.Info);
        }
        else
        {
          // Will be totally different and not important now so come back to this later...
        }
      }
      catch(Exception e)
      {
        BowlingManagement.Static.Logs.Status.AddLogMessage(e.Message,Log.MessageSeverity.Error);
        if (rdrTeamPlayers != null)
        {
          if (!rdrTeamPlayers.IsClosed)
          {
            rdrTeamPlayers.Close();
          }
        }
      }
    }
  }
}
