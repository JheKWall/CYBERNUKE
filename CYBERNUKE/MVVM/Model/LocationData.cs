using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYBERNUKE.MVVM.Model
{
    public class LocationData
    {
        public string locationName;
        public int locationCoordY;
        public int locationCoordX;

        public LocationData(string name, int x, int y)
        {
            locationName = name;
            locationCoordY = y;
            locationCoordX = x;
        }
    }
}
