using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYBERNUKE.MVVM.Model
{
    public class ObjectData
    {
        int objectCoordinateX;
        int objectCoordinateY;

        public ObjectData(string name, int x, int y)
        {
            objectCoordinateX = x;
            objectCoordinateY = y;
        }

        //TODO: method that gives item? tied to item id? money?
    }
}
