using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingManagement
{
    class TeamSeason
    {
        private string _team;
        private Dictionary<int, TeamWeek> _weeklyResults;

        public TeamSeason()
        {

        }

        public string Team
        {
            get { return _team; }
            set { _team = value; }
        }

        public void UpdateWithFixture(Fixture updateFixture)
        {

        }
    }
}
