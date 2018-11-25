using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingManagement
{
    class TeamWeek
    {

        private int _week;
        private int _lane;
        private string _opponents;
        private int _teamPts;
        private int _ptsFor;
        private int _ptsAgainst;
        private int _teamHcap;
        private int _scratchSeries;
        private int _hcapSeries;

        public int Week
        {
            get { return _week; }
            set { _week = value; }
        }
        public int Lane
        {
            get { return _lane; }
            set { _lane = value; }
        }
        public string Opponents
        {
            get { return _opponents; }
            set { _opponents = value; }
        }
        public int TeamPts
        {
            get { return _teamPts; }
            set { _teamPts = value; }
        }

        public int PtsFor
        {
            get { return _ptsFor; }
            set { _ptsFor = value; }
        }
        public int PtsAgainst
        {
            get { return _ptsAgainst; }
            set { _ptsAgainst = value; }
        }

        public int TeamHcap
        {
            get { return _teamHcap; }
            set { _teamHcap = value; }
        }
        public int ScratchSeries
        {
            get { return _scratchSeries; }
            set { _scratchSeries = value; }
        }
        public int HcapSeries
        {
            get { return _hcapSeries; }
            set { _hcapSeries = value; }
        }

    }
}
