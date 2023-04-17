using CYBERNUKE.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CYBERNUKE.Core;
using CYBERNUKE.MVVM.Model;

namespace CYBERNUKE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Map Variables
        public string mapToLoad = "TestMap";
        public string currentMap = "";
        public List<Map> mapList;
        private List<string> loadedMaps; //contains all loaded map names

        //Combat Variables
        public string enemyPartyName = "LesserZombieHorde";

        //Town Variables
        public string townToLoad = "Tranquility";
        public string currentTown = "";

        public MainWindow()
        {
            InitializeComponent();

            // Initialize lists
            mapList = new List<Map>();
            loadedMaps = new List<string>();
        }

        //Public method for creating and accessing maps in the mapList
        public int Get_Map()
        {
            int index;

            //Checks array for maps
            bool has = loadedMaps.Contains(mapToLoad);

            if (!has) //If not in list
            {
                //Add map to loaded list, create new map
                loadedMaps.Add(mapToLoad);
                mapList.Add(new Map());
                index = loadedMaps.IndexOf(mapToLoad);
            }
            else //If it is in the list
            {
                index = loadedMaps.IndexOf(mapToLoad);
            }

            return index;
        }
    }
}
