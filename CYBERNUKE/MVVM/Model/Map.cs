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
        public LocationData[] locationData;
        public List<string> locationNames;
        //objectdata
        //npcdata
        public int mapWidth;
        public int mapHeight;
        public int encounterChance;
        public string mapName;

        public Map()
        {
            // Initialize list
            locationNames = new List<string>();

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

            // Location Input
            int numLocations = Int32.Parse(input.ReadLine());
            locationData = new LocationData[numLocations];
            locationNames.Capacity = numLocations;
            if (numLocations != 0)
            {
                for (int i = 0; i < numLocations; i++)
                {
                    locationNames.Add(input.ReadLine());
                    int x = Int32.Parse(input.ReadLine());
                    int y = Int32.Parse(input.ReadLine());
                    locationData[i] = new LocationData(x, y);
                }
            }

            // NPC Location Input

            // Object Location Input

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

        public int Get_Spawn_Index(string locationName)
        {
            int index;

            //Checks array for maps
            bool has = locationNames.Contains(locationName);

            if (!has) //If not in list
            {
                return index = 0;
            }
            //else
            return index = locationNames.IndexOf(locationName);
        }
        
    }
}
