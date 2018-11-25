using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingManagement
{
  public class SeasonValidator
  {
    private Season _seasonDatabase;
    private Season _seasonCalculated;
    public void Load(string seasonYear)
    {
      InitialiseDatabaseSeason(seasonYear);
      _seasonCalculated = _seasonDatabase.ShallowCopy();
    }

    private void InitialiseDatabaseSeason(string seasonYear)
    {
      _seasonDatabase = new BowlingManagement.Season();
      _seasonDatabase.Parameters.Load(seasonYear);
      if (seasonYear == "Current")
      {
        _seasonDatabase.LoadStructure(true, "");
      }
      else
      {
        // This route will need more testing
        _seasonDatabase.LoadStructure(false, seasonYear);
      }

      _seasonDatabase.LoadPlayData();
     
    }
  }
}
