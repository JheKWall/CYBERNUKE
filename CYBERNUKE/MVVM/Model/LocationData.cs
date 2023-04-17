using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYBERNUKE.MVVM.Model
{
    public class LocationData
    {
        public int locationCoordY;
        public int locationCoordX;

        public LocationData(int x, int y)
        {
            locationCoordY = y;
            locationCoordX = x;
        }
    }
}
