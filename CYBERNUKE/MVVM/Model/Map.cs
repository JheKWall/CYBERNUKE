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
        public List<LocationData> teleportLocationData;
        public List<LocationData> townTeleportLocationData;
        public List<LocationData> npcLocationData;
        public List<LocationData> objectLocationData;
        public List<LocationData> enemyLocationData;
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

            //1. MAP NAME
            mapName = input.ReadLine();

            //2-3. MAP HEIGHT, WIDTH
            mapHeight = Int32.Parse(input.ReadLine());
            mapWidth = Int32.Parse(input.ReadLine());
            mapData = new char[mapHeight, mapWidth];

            //4. ENCOUNTER CHANCE
            encounterChance = Int32.Parse(input.ReadLine());

            //5-6. DEFAULT SPAWN
            defaultSpawnX = Int32.Parse(input.ReadLine());
            defaultSpawnY = Int32.Parse(input.ReadLine());

            input.ReadLine();
            #region 7-10. MAP <-> MAP TELEPORT LOCATIONS
            int numLocations = Int32.Parse(input.ReadLine());
            //teleportLocationData = new LocationData[numLocations];
            teleportLocationData = new List<LocationData>();
            if (numLocations != 0)
            {
                for (int i = 0; i < numLocations; i++)
                {
                    string name = input.ReadLine();
                    int x = Int32.Parse(input.ReadLine());
                    int y = Int32.Parse(input.ReadLine());

                    LocationData temp = new LocationData(name, x, y);
                    teleportLocationData.Add(temp);
                }
            }
            #endregion

            input.ReadLine();
            #region 11-14. MAP <-> TOWN TELEPORT LOCATIONS
            numLocations = Int32.Parse(input.ReadLine());
            townTeleportLocationData = new List<LocationData>();
            if (numLocations != 0)
            {
                for (int i = 0; i < numLocations; i++)
                {
                    string name = input.ReadLine();
                    int x = Int32.Parse(input.ReadLine());
                    int y = Int32.Parse(input.ReadLine());

                    LocationData temp = new LocationData(name, x, y);
                    townTeleportLocationData.Add(temp);
                }
            }
            #endregion

            input.ReadLine();
            #region 15-18. NPC LOCATIONS
            numLocations = Int32.Parse(input.ReadLine());
            npcLocationData = new List<LocationData>();
            if (numLocations != 0)
            {
                for (int i = 0; i < numLocations; i++)
                {
                    string name = input.ReadLine();
                    int x = Int32.Parse(input.ReadLine());
                    int y = Int32.Parse(input.ReadLine());

                    LocationData temp = new LocationData(name, x, y);
                    npcLocationData.Add(temp);
                }
            }
            #endregion

            input.ReadLine();
            #region 19-22. OBJECT LOCATIONS
            numLocations = Int32.Parse(input.ReadLine());
            objectLocationData = new List<LocationData>();
            if (numLocations != 0)
            {
                for (int i = 0; i < numLocations; i++)
                {
                    string name = input.ReadLine();
                    int x = Int32.Parse(input.ReadLine());
                    int y = Int32.Parse(input.ReadLine());
                    int targetx = Int32.Parse(input.ReadLine());
                    int targety = Int32.Parse(input.ReadLine());

                    LocationData temp = new LocationData(name, x, y, targetx, targety);
                    objectLocationData.Add(temp);
                }
            }
            #endregion

            input.ReadLine();
            #region 23-26. ENEMY LOCATIONS
            numLocations = Int32.Parse(input.ReadLine());
            enemyLocationData = new List<LocationData>();
            if (numLocations != 0)
            {
                for (int i = 0; i < numLocations; i++)
                {
                    string name = input.ReadLine();
                    int x = Int32.Parse(input.ReadLine());
                    int y = Int32.Parse(input.ReadLine());

                    LocationData temp = new LocationData(name, x, y);
                    enemyLocationData.Add(temp);
                }
            }
            #endregion

            //27. MAP DATA
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    mapData[i, j] = (char)input.Read();
                }
            }
        }

        public void Remove_Location(int type, int posX, int posY)
        {
            int index;

            //0 == teleport, 1 == town teleport, 2 == npc, 3 == object, 4 == enemy
            switch (type)
            {
                case 0:
                    index = Get_Teleport_Pos(posX, posY);
                    teleportLocationData.RemoveAt(index);
                    break;

                case 1:
                    index = Get_TownTeleport_Pos(posX, posY);
                    townTeleportLocationData.RemoveAt(index);
                    break;

                case 2:
                    index = Get_NPC_Pos(posX, posY);
                    npcLocationData.RemoveAt(index);
                    break;

                case 3:
                    index = Get_Object_Pos(posX, posY);
                    objectLocationData.RemoveAt(index);
                    break;

                case 4:
                    index = Get_Enemy_Pos(posX, posY);
                    enemyLocationData.RemoveAt(index);
                    break;
            }
        } //Using Location

        public void Remove_Location(int type, int index) //Using Index
        {
            //0 == teleport, 1 == town teleport, 2 == npc, 3 == object, 4 == enemy
            switch (type)
            {
                case 0:
                    teleportLocationData.RemoveAt(index);
                    break;

                case 1:
                    townTeleportLocationData.RemoveAt(index);
                    break;

                case 2:
                    npcLocationData.RemoveAt(index);
                    break;

                case 3:
                    objectLocationData.RemoveAt(index);
                    break;

                case 4:
                    enemyLocationData.RemoveAt(index);
                    break;
            }
        } //Using Location

        public int Get_Spawn_Index(string spawnLocationName)
        {
            //Check for map teleport location
            for (int i = 0; i < teleportLocationData.Count; i++)
            {
                if (teleportLocationData[i].locationName == spawnLocationName)
                {
                    return i;
                }
            }
            //Check for town teleport location
            for (int i = 0; i < townTeleportLocationData.Count; i++)
            {
                if (townTeleportLocationData[i].locationName == spawnLocationName)
                {
                    return i;
                }
            }
            return -1;
        }

        #region Public methods for checking player position against 
        public int Check_Player_Pos(int posX, int posY)
        {
            //0 == teleport, 1 == town teleport, 2 == npc, 3 == object, 4 == enemy
            int inspector;

            //Teleport Location Check
            inspector = Get_Teleport_Pos(posX, posY);
            if (inspector != -1)
            {
                return 0;
            }

            //Town Teleport Location Check
            inspector = Get_TownTeleport_Pos(posX, posY);
            if (inspector != -1)
            {
                return 1;
            }

            //NPC Check
            inspector = Get_NPC_Pos(posX, posY);
            if (inspector != -1)
            {
                return 2;
            }

            //Object Check
            inspector = Get_Object_Pos(posX, posY);
            if (inspector != -1)
            {
                return 3;
            }

            //Enemy Check
            inspector = Get_Enemy_Pos(posX, posY);
            if (inspector != -1)
            {
                return 4;
            }

            return -1;
        }
        public int Get_Teleport_Pos(int posX, int posY)
        {
            //Teleport Location Get
            for (int i = 0; i < teleportLocationData.Count; i++)
            {
                if ((posX == teleportLocationData[i].locationCoordX) && (posY == teleportLocationData[i].locationCoordY))
                {
                    return i;
                }
            }
            return -1; //not found
        }
        public int Get_TownTeleport_Pos(int posX, int posY)
        {
            //Teleport Location Get
            for (int i = 0; i < townTeleportLocationData.Count; i++)
            {
                if ((posX == townTeleportLocationData[i].locationCoordX) && (posY == townTeleportLocationData[i].locationCoordY))
                {
                    return i;
                }
            }
            return -1; //not found
        }
        public int Get_NPC_Pos(int posX, int posY)
        {
            //NPC Location Get
            for (int i = 0; i < npcLocationData.Count; i++)
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
            for (int i = 0; i < objectLocationData.Count; i++)
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
            for (int i = 0; i < enemyLocationData.Count; i++)
            {
                if ((posX == enemyLocationData[i].locationCoordX) && (posY == enemyLocationData[i].locationCoordY))
                {
                    return i;
                }
            }
            return -1; //not found
        }
        #endregion
    }
}
