using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYBERNUKE.MVVM.Model
{
    class NPCData
    {
        string NPCName;
        int NPCCoordX;
        int NPCCoordY;

        public NPCData(string name, int x, int y)
        {
            NPCName = name;
            NPCCoordX = x;
            NPCCoordY = y;
        }

        //TODO: method that loads up dialogue
    }
}
