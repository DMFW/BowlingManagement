using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingManagement
{
    abstract class PinfallMeasure
    {

        // Anything that has a pinfall and a handicap.
        // Inherited by indvividual games & series and team games and series.

        int _cfHcap;
        int _scratchPinfall;
 
        public int CFHcap
        {
            get { return _cfHcap; }
            set { _cfHcap = value; }
        }

        public int ScratchPinfall
        {
            get { return _scratchPinfall; }
            set { _scratchPinfall = value; }
        }

        public int HcapPinfall
        {
            get { return _scratchPinfall + _cfHcap; }
        }
    }
}
