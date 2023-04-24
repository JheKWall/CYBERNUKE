using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CYBERNUKE.MVVM.Model
{
    public class Map
    {
        //StreamReader input
        private StreamReader input;

        //Map Vars
        public string mapToLoad;
        public char[,] mapData;
        public LocationData[] teleportLocationData;
        public LocationData[] objectLocationData;
        public LocationData[] npcLocationData;
        public LocationData[] enemyLocationData;
        public int mapWidth;
        public int mapHeight;
        public int encounterChance;
        public string mapName;
        public int defaultSpawnX;
        public int defaultSpawnY;

        public Map()
        {
            // Get map to load
            mapToLoad = ((MainWindow)Application.Current.MainWindow).mapToLoad;
            // Read that map's data
            Read_Map_Data(mapToLoad);
        }

        //Private method for reading in map data from a map txt file
        private void Read_Map_Data(string mapFileName)
        {
            // Initialize StreamReader to MapData.txt
            input = new StreamReader("GameData/Maps/" + mapFileName + ".txt");

            // Map Name Input
            mapName = input.ReadLine();

            // Initialize map char array to map size
            mapHeight = Int32.Parse(input.ReadLine());
            mapWidth = Int32.Parse(input.ReadLine());
            mapData = new char[mapHeight, mapWidth];

            // Encounter Chances
            encounterChance = Int32.Parse(input.ReadLine());

            // Default Spawn
            defaultSpawnX = Int32.Parse(input.ReadLine());
            defaultSpawnY = Int32.Parse(input.ReadLine());

            #region Spawn Location Input
            int numLocations = Int32.Parse(input.ReadLine());
            teleportLocationData = new LocationData[numLocations];
            if (numLocations != 0)
            {
                for (int i = 0; i < numLocations; i++)
                {
                    string name = input.ReadLine();
                    int x = Int32.Parse(input.ReadLine());
                    int y = Int32.Parse(input.ReadLine());
                    teleportLocationData[i] = new LocationData(name, x, y);
                }
            }
            #endregion

            #region NPC Location Input
            numLocations = Int32.Parse(input.ReadLine());
            objectLocationData = new LocationData[numLocations];
            if (numLocations != 0)
            {
                for (int i = 0; i < numLocations; i++)
                {
                    string name = input.ReadLine();
                    int x = Int32.Parse(input.ReadLine());
                    int y = Int32.Parse(input.ReadLine());
                    objectLocationData[i] = new LocationData(name, x, y);
                }
            }
            #endregion

            #region Object Location Input
            numLocations = Int32.Parse(input.ReadLine());
            npcLocationData = new LocationData[numLocations];
            if (numLocations != 0)
            {
                for (int i = 0; i < numLocations; i++)
                {
                    string name = input.ReadLine();
                    int x = Int32.Parse(input.ReadLine());
                    int y = Int32.Parse(input.ReadLine());
                    npcLocationData[i] = new LocationData(name, x, y);
                }
            }
            #endregion

            #region Enemy Location Input
            numLocations = Int32.Parse(input.ReadLine());
            enemyLocationData = new LocationData[numLocations];
            if (numLocations != 0)
            {
                for (int i = 0; i < numLocations; i++)
                {
                    string name = input.ReadLine();
                    int x = Int32.Parse(input.ReadLine());
                    int y = Int32.Parse(input.ReadLine());
                    enemyLocationData[i] = new LocationData(name, x, y);
                }
            }
            #endregion

            // Input map data
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    mapData[i, j] = (char)input.Read();
                }
            }

            // Update current map
            ((MainWindow)Application.Current.MainWindow).currentMap = mapToLoad;
        }

        public int Get_Spawn_Index(string spawnLocationName)
        {
            //Check location array for map
            for (int i = 0; i < teleportLocationData.Length; i++)
            {
                if (teleportLocationData[i].locationName == spawnLocationName)
                {
                    return i;
                }
            }
            return -1;
        }

        //Public methods for checking player position against 
        public int Check_Player_Pos(int posX, int posY)
        {
            //0 == teleport, 1 == npc, 2 == object, 3 == enemy
            int inspector;

            //Teleport Location Check
            inspector = Get_Teleport_Pos(posX, posY);
            if (inspector != -1)
            {
                return 0;
            }

            //NPC Check
            inspector = Get_NPC_Pos(posX, posY);
            if (inspector != -1)
            {
                return 1;
            }

            //Object Check
            inspector = Get_Object_Pos(posX, posY);
            if (inspector != -1)
            {
                return 2;
            }

            //Enemy Check
            inspector = Get_Enemy_Pos(posX, posY);
            if (inspector != -1)
            {
                return 3;
            }

            return -1;
        }
        public int Get_Teleport_Pos(int posX, int posY)
        {
            //Teleport Location Get
            for (int i = 0; i < teleportLocationData.Length; i++)
            {
                if ((posX == teleportLocationData[i].locationCoordX) && (posY == teleportLocationData[i].locationCoordY))
                {
                    return i;
                }
            }
            return -1; //not found
        }
        public int Get_NPC_Pos(int posX, int posY)
        {
            //NPC Location Get
            for (int i = 0; i < npcLocationData.Length; i++)
            {
                if ((posX == npcLocationData[i].locationCoordX) && (posY == npcLocationData[i].locationCoordY))
                {
                    return i;
                }
            }
            return -1; //not found
        }
        public int Get_Object_Pos(int posX, int posY)
        {
            //Object Location Get
            for (int i = 0; i < objectLocationData.Length; i++)
            {
                if ((posX == objectLocationData[i].locationCoordX) && (posY == objectLocationData[i].locationCoordY))
                {
                    return i;
                }
            }
            return -1; //not found
        }
        public int Get_Enemy_Pos(int posX, int posY)
        {
            //Enemy Location Get
            for (int i = 0; i < enemyLocationData.Length; i++)
            {
                if ((posX == enemyLocationData[i].locationCoordX) && (posY == enemyLocationData[i].locationCoordY))
                {
                    return i;
                }
            }
            return -1; //not found
        }
    }
}
