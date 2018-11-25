using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingManagement
{
  class PlayerContest
  {

    // Represents a single match up between a position in both teams
    // which scores against one another. Usually these will be the same player
    // but could vary if there is a substitution in game.

    // The substitution will only be to or from blind at present as this is
    // all that is permitted by the current web site code.

    Season _season;
    int _lineUpPosition;

    Dictionary<int, PlayerGame> _dctHomeTeamPlayerGames;
    Dictionary<int, PlayerGame> _dctAwayTeamPlayerGames;

    public PlayerContest(Season season)
    {
      _season = season;
    }

    public int LineUpPosition
    {
        get { return _lineUpPosition; }
        set { _lineUpPosition = value; }
    }

      public int Score(MatchAgentRole scoreRole, ScoreType scoreType)
      {
          int score = 0;
          int scoringSeries = 0;
          int opposingSeries = 0;

          Dictionary<int, PlayerGame> dctScoringGames;
          Dictionary<int, PlayerGame> dctOpposingGames;

          if (scoreRole == MatchAgentRole.home)
          {
              dctScoringGames = _dctHomeTeamPlayerGames;
              dctOpposingGames = _dctAwayTeamPlayerGames;
          }
          else
          {
              dctScoringGames = _dctAwayTeamPlayerGames;
              dctOpposingGames = _dctHomeTeamPlayerGames;
          }

          foreach (PlayerGame scoringGame in dctScoringGames.Values)
          {
              switch (scoreType)
              {
                  case ScoreType.handicap:
                      scoringSeries = scoringSeries + scoringGame.HcapPinfall;
                      break;
                  case ScoreType.scratch:
                      scoringSeries = scoringSeries + scoringGame.ScratchPinfall;
                      break;
              }

              if (dctOpposingGames.ContainsKey(scoringGame.GameNo))
              {
                  PlayerGame opposingGame = dctOpposingGames[scoringGame.GameNo];
                  switch (scoreType)
                  {
                      case ScoreType.handicap:
                          if (scoringGame.HcapPinfall > opposingGame.HcapPinfall)
                          {
                              score = score + _season.Parameters.PlayerGamePts;
                          }
                          if (scoringGame.HcapPinfall == opposingGame.HcapPinfall)
                          {
                              score = score + (_season.Parameters.PlayerGamePts / 2);
                          }
                          opposingSeries = opposingSeries + opposingGame.HcapPinfall;
                          break;
                      case ScoreType.scratch:
                          if (scoringGame.ScratchPinfall > opposingGame.ScratchPinfall)
                          {
                              score = score + _season.Parameters.PlayerGamePts;
                          }
                          if (scoringGame.ScratchPinfall == opposingGame.ScratchPinfall)
                          {
                              score = score + (_season.Parameters.PlayerGamePts / 2);
                          }
                          opposingSeries = opposingSeries + opposingGame.ScratchPinfall;
                          break;
                  }
              }
          }
          if (scoringSeries > opposingSeries)
          {
              score = score + _season.Parameters.PlayerSeriesPts;
          }
          if (scoringSeries == opposingSeries)
          {
              score = score + (_season.Parameters.PlayerSeriesPts / 2);
          }
          return score;
      }
   }
}