using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYBERNUKE.MVVM.Model
{
    public class LocationData
    {
        public int locationCoordX;
        public int locationCoordY;

        public LocationData(int x, int y)
        {
            locationCoordX = x;
            locationCoordY = y;
        }
    }
}
