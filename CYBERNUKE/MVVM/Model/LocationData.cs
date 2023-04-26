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

        //Extra Vars for Objects
        public bool boolVar;
        public int targetCoordY;
        public int targetCoordX;

        public LocationData(string name, int x, int y)
        {
            locationName = name;
            locationCoordY = y;
            locationCoordX = x;
        }

        //Object Location Data Constructor
        public LocationData(string name, int x, int y, int targetx, int targety)
        {
            locationName = name;
            locationCoordY = y;
            locationCoordX = x;
            targetCoordY = targety;
            targetCoordX = targetx;
        }

        public void Toggle_Bool()
        {
            if (boolVar)
            {
                boolVar = false;
            }
            else
            {
                boolVar = true;
            }
        }
    }
}
