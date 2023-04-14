using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CYBERNUKE.MVVM.View
{
    /// <summary>
    /// Interaction logic for OverworldView.xaml
    /// </summary>
    public partial class OverworldView : UserControl
    {
        //File reader
        private StreamReader input;
        //Map data
        string mapToLoad;
        char[,] mapData;
        int mapWidth;
        int mapHeight;
        int encounterMin;
        int encounterMax;

        public OverworldView()
        {
            InitializeComponent();
            ScaleText();

            // Gets map to load from main window.
            mapToLoad = ((MainWindow)Application.Current.MainWindow).mapToLoad;
            Read_Map_Data(mapToLoad);
        }

        //Private method for handling character movement and map updates
        private void Map_Movement()
        {

        }

        //Private method for loading a map to the overworldview
        private void Load_Map()
        {
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    MapDisplay.Text += mapData[i, j];
                }
            }
        }

        //Private method for reading in map data from a map txt file
        private void Read_Map_Data(string mapName)
        {
            // Initialize StreamReader to MapData.txt
            input = new StreamReader("GameData/Maps/" + mapName + ".txt");

            // Read Map Name
            MapDisplay_Location.Text = input.ReadLine();

            // Initialize map char array to map size
            mapWidth = Int32.Parse(input.ReadLine());
            mapHeight = Int32.Parse(input.ReadLine());
            mapData = new char[mapHeight, mapWidth];

            // Encounter Chances
            encounterMin = Int32.Parse(input.ReadLine());
            encounterMax = Int32.Parse(input.ReadLine());

            // Input map data
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    mapData[i, j] = (char)input.Read();
                }
            }

            // Update current map
            ((MainWindow)Application.Current.MainWindow).currentMap = mapName;

            // Load Map
            Load_Map();
        }

        private void Button_Menu_Click(object sender, RoutedEventArgs e)
        {
            // Open Pause Menu
        }
        private void Button_Up_Click(object sender, RoutedEventArgs e)
        {
            // Go Up on map
        }
        private void Button_Left_Click(object sender, RoutedEventArgs e)
        {
            // Go Left on map
        }
        private void Button_Interact_Click(object sender, RoutedEventArgs e)
        {
            // Interact with current spot on map
        }
        private void Button_Right_Click(object sender, RoutedEventArgs e)
        {
            // Go Right on map
        }
        private void Button_Down_Click(object sender, RoutedEventArgs e)
        {
            // Go Down on map
        }
        private void Button_Map_Click(object sender, RoutedEventArgs e)
        {
            // Open local map
        }


        //Methods for text scaling//Private classes for scaling text with resolution
        private void ScaleText()
        {
            switch (Application.Current.MainWindow.Width)
            {
                case 1366:
                    break;
                case 1600:
                    ChangeFontSize(35);
                    break;
                case 1920:
                    ChangeFontSize(45);
                    break;
                default:
                    break;
            }
        }
        private void ChangeFontSize(double size)
        {
            MapDisplayFontSize.FontSize = size;
        }

        //Methods for handling keyboard input
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // potential memory leak if not unloaded ? (uncertain question mark)
            var window = Window.GetWindow(this);
            window.KeyDown += HandleKeyPress;
        }
        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            // Does not animate the button. The button animation is held within the Button's style. This only calls the base button's click, not the style.

            switch (e.Key)
            {
                case Key.Q:
                    Button_Menu.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    break;

                case Key.W:
                    Button_Up.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    break;

                case Key.A:
                    Button_Left.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    break;

                case Key.S:
                    Button_Interact.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    break;

                case Key.D:
                    Button_Right.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    break;

                case Key.X:
                    Button_Down.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    break;

                case Key.C:
                    Button_Map.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    break;

                default:
                    break;
            }
        }

    }
}
