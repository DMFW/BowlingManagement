using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingManagement
{
  public class PlayerWeek
  {
    private Player _player;
    private Week _week;
    Dictionary<int, int> _dctScratchGame;
    Dictionary<int, int> _dctHcapGame;

    int _hcapCF;
    int _hcapBF;

    public PlayerWeek(Player player, Week week)
    {
      _player = player;
      _week = week;
    }

    public void AddScratchScore(int gameNo, int scratchPinfall)
    {
      _dctScratchGame.Add(gameNo, scratchPinfall);
    }

    public int HcapCF
    {
      get { return _hcapCF; }
      set { _hcapCF = value; }
    }

    public int HcapBF
    {
      get { return _hcapBF; }
      set { _hcapBF = value; }
    }

    public void CalculateHandicapPinfall(int hcapBF)
    {
      _hcapBF = hcapBF;
      foreach (KeyValuePair<int, int> kvpScratchGame in _dctScratchGame)
      {
        _dctHcapGame.Add(kvpScratchGame.Key, kvpScratchGame.Value + hcapBF);
      }
    }

  }
}
