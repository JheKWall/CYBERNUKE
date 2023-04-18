using System;
using System.Collections.Generic;
using System.Data;
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
        //Map Vars
        Window overworldWindow;

        //string mapToLoad;
        char[,] mapData;
        char[,] dynamicMap;
        int currentIndex;
        int mapWidth;
        int mapHeight;

        //Player Vars
        int playerPosY;
        int playerPosX;
        bool hasControl;

        public OverworldView()
        {
            InitializeComponent();
            ScaleText();
            Map_First_Render();

            hasControl = true;
        }

        //Private method for loading into another map
        private void Next_Map(string mapName)
        {
            ((MainWindow)Application.Current.MainWindow).mapToLoad = mapName;
            ((MainWindow)Application.Current.MainWindow).Get_Map();
            Map_First_Render();
        }

        //Private method for loading a map to the overworldview
        private void Map_First_Render()
        {
            // Clear MapDisplay
            MapDisplay.Text = "";

            // Get Map Index
            currentIndex = ((MainWindow)Application.Current.MainWindow).Get_Map();

            // Get Map Vars
            mapWidth = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].mapWidth;
            mapHeight = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].mapHeight;
            string mapName = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].mapName;
            string currentMap = ((MainWindow)Application.Current.MainWindow).currentMap;

            // Initialize map data lists
            mapData = new char[mapHeight, mapWidth];
            dynamicMap = new char[mapHeight, mapWidth];
            mapData = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].mapData;
            // Deep copy of mapdata into dynamicmap
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    dynamicMap[i, j] = mapData[i, j];
                }
            }

            // Set Map Name
            MapDisplay_Location.Text = mapName;

            // Set Player Spawn on dynamicMap
            int spawnIndex = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].Get_Spawn_Index(currentMap);
            playerPosY = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].locationData[spawnIndex].locationCoordY;
            playerPosX = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].locationData[spawnIndex].locationCoordX;
            dynamicMap[playerPosY, playerPosX] = '☢';

            // Render Map to Screen
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    MapDisplay.Text += dynamicMap[i, j];
                }
            }

            // Set new current map
            ((MainWindow)Application.Current.MainWindow).currentMap = ((MainWindow)Application.Current.MainWindow).mapToLoad;
        }

        private void Encounter_Chance()
        {
            int minPercent = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].encounterMin;
            int maxPercent = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].encounterMax;

            Random rand = new Random();
            int result = rand.Next(minPercent, maxPercent);
            //If result is blah blah blah, trigger a combat encounter
            //Also randomize which enemy party to fight
        }

        //Private method for updating the on-screen map
        private void Map_Update_Render()
        {
            // Clear MapDisplay
            MapDisplay.Text = "";

            // Deep copy of mapdata into dynamicmap
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    dynamicMap[i, j] = mapData[i, j];
                }
            }
            dynamicMap[playerPosY, playerPosX] = '☢';
            // Render Map to Screen
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    MapDisplay.Text += dynamicMap[i, j];
                }
            }
        }

        //Private method for validating player moves
        //1 == up, 2 == left, 3 == right, 4 == down
        private void Validate_Move(int move)
        {

        }

        //Private method for updates on player move
        private void Player_Move()
        {

        }

        //Private method for starting combat
        private void Start_Combat()
        {

        }


        private void Button_Menu_Click(object sender, RoutedEventArgs e)
        {
            // Open Pause Menu
        }
        private void Button_Up_Click(object sender, RoutedEventArgs e)
        {
            // Validate movement method
            
            // Move Up
            playerPosY--;

            // Call Player Move

            // Update Map
            Map_Update_Render();
        }
        private void Button_Left_Click(object sender, RoutedEventArgs e)
        {
            // Validate movement method

            // Move Left
            playerPosX--;

            // Call Player Move

            // Update Map
            Map_Update_Render();
        }
        private void Button_Interact_Click(object sender, RoutedEventArgs e)
        {
            // Interact with current spot on map
        }
        private void Button_Right_Click(object sender, RoutedEventArgs e)
        {
            // Validate movement method

            // Move Right
            playerPosX++;

            // Call Player Move

            // Update Map
            Map_Update_Render();
        }
        private void Button_Down_Click(object sender, RoutedEventArgs e)
        {
            // Validate movement method

            // Move Down
            playerPosY++;

            // Call Player Move

            // Update Map
            Map_Update_Render();
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
            if (hasControl)
            {
                // potential memory leak if not unloaded ? (uncertain question mark)
                var window = Window.GetWindow(this);
                overworldWindow = window;
                window.KeyDown += HandleKeyPress;
            }
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

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            overworldWindow.KeyDown -= HandleKeyPress;
        }
    }
}
