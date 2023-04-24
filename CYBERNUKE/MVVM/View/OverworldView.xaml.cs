using CYBERNUKE.GameData.UserControls;
using CYBERNUKE.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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
        //Streamreader
        private StreamReader input;

        //Prompt Vars
        //I HATE THREADING!!! I HATE THREADING!!! I HATE THREADING!!! I HATE THREADING!!! I HATE THREADING!!! I HATE THREADING!!! I HATE THREADING!!! 
        bool inPrompt = false;
        int numPromptRuns; //How many times the prompt needs to run
        int promptIndex; //Current run index

        //Dialogue Vars
        string[] dialogueArray;

        //Character Menu
        PauseMenu pauseMenu;
        bool pauseMenuOpen = false;
        
        //Map Vars
        Window overworldWindow;
        int encounterChance;

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
        int playerCount = 0;
        List<Character> ListCharacters = new List<Character>();

        public OverworldView()
        {
            InitializeComponent();
            ScaleText();
            Map_First_Render();
            Load_Player_Boxes();

            pauseMenu = new PauseMenu();
            OverworldView_CharacterMenuContainer.Children.Add(pauseMenu);

            hasControl = true;
        }

        //Private method for loading into another map
        private void Next_Map(string mapName)
        {
            ((MainWindow)Application.Current.MainWindow).mapToLoad = mapName;
            ((MainWindow)Application.Current.MainWindow).Get_Map();
            Map_First_Render();
        }

        //Private method for loading player boxes
        private void Load_Player_Boxes()
        {
            playerCount = ((MainWindow)Application.Current.MainWindow).numPartyMembers;
            ListCharacters.Capacity = playerCount;

            // Get Players
            for (int i = 0; i < playerCount; i++)
            {
                ListCharacters.Add(((MainWindow)Application.Current.MainWindow).CharacterList[i]);
            }

            // Add Overworld Boxes
            for (int i = 0; i < playerCount; i++)
            {
                // Create Overworld Box
                string name = ListCharacters[i].getName();
                int currenthp = ListCharacters[i].getCurrentHP();
                int maxhp = ListCharacters[i].getMaxHP();
                int currentsp = ListCharacters[i].getCurrentSP();
                int maxsp = ListCharacters[i].getMaxSP();

                OverworldBox overworldbox = new OverworldBox(name, currenthp, maxhp, currentsp, maxsp);

                switch (i)
                {
                    case 0:
                        OV_PB_Pos1.Children.Add(overworldbox);
                        break;
                    case 1:
                        OV_PB_Pos2.Children.Add(overworldbox);
                        break;
                    case 2:
                        OV_PB_Pos3.Children.Add(overworldbox);
                        break;
                    case 3:
                        OV_PB_Pos4.Children.Add(overworldbox);
                        break;
                }
            }
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
            encounterChance = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].encounterChance;

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

            // Set Map Name & Encounter %
            MapDisplay_Location.Text = mapName;
            MapDisplay_EnemyPercent.Text = encounterChance + "%";

            // Set Player Spawn on dynamicMap
            // Check if "returnToSavedPos" is true (set when entering combat)
            if (((MainWindow)Application.Current.MainWindow).returnToSavedPos)
            {
                playerPosY = ((MainWindow)Application.Current.MainWindow).currentPosY;
                playerPosX = ((MainWindow)Application.Current.MainWindow).currentPosX;
            }
            else
            {
                int spawnIndex = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].Get_Spawn_Index(currentMap);
                playerPosY = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].locationData[spawnIndex].locationCoordY;
                playerPosX = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].locationData[spawnIndex].locationCoordX;
            }
            dynamicMap[playerPosY, playerPosX] = '☢';
            ((MainWindow)Application.Current.MainWindow).returnToSavedPos = false;

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
            //1. Generate a number from 0 to 100
            //2. If it falls below the encounter chance, congrats you are being attacked
            //3. Else, continue on with your day
            Random rand = new Random();
            int chance = rand.Next(0, 100);
            if (chance <= encounterChance)
            {
                //4. Get random enemy party
                int index = rand.Next(((MainWindow)Application.Current.MainWindow).enemyPartyList.Count);
                string enemyParty = ((MainWindow)Application.Current.MainWindow).enemyPartyList[index];
                Start_Combat(enemyParty);
            }
        }

        //Private method for updating the on-screen map
        private void Map_Update_Render()
        {
            // Save PlayerPos
            ((MainWindow)Application.Current.MainWindow).currentPosY = playerPosY;
            ((MainWindow)Application.Current.MainWindow).currentPosX = playerPosX;

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

            // Run Player Move (for events)
            Player_Move();
        }

        //Private method for validating player moves
        //1 == up, 2 == left, 3 == right, 4 == down
        private bool Validate_Move(int move)
        {
            bool valid;
            switch (move)
            {
                case 1:
                    valid = Check_Coordinates(playerPosY - 1, playerPosX);
                    if (valid)
                    {
                        return true;
                    }
                    return false;
                case 2:
                    valid = Check_Coordinates(playerPosY, playerPosX - 1);
                    if (valid)
                    {
                        return true;
                    }
                    return false;
                case 3:
                    valid = Check_Coordinates(playerPosY, playerPosX + 1);
                    if (valid)
                    {
                        return true;
                    }
                    return false;
                case 4:
                    valid = Check_Coordinates(playerPosY + 1, playerPosX);
                    if (valid)
                    {
                        return true;
                    }
                    return false;
                default:
                    break;
            }
            return true;
        }
        private bool Check_Coordinates(int Y, int X)
        {
            if (dynamicMap[Y, X] == '⬛')
            {
                return false;
            }
            if (dynamicMap[Y, X] == '⛞')
            {
                Init_Dialogue("LockedDoor");
                return false;
            }
            return true;
        }

        //Public & private method for displaying popups
        public void Init_Dialogue(string dialogueName) //Call when starting prompt
        {
            #region Get Dialogue
            input = new StreamReader("GameData/Dialogue/" + dialogueName + ".txt");
            numPromptRuns = Int32.Parse(input.ReadLine());
            dialogueArray = new string[numPromptRuns];
            for (int i = 0; i < numPromptRuns; i++)
            {
                dialogueArray[i] = input.ReadLine();
            }
            #endregion

            // Close Streamreader
            input.Close();

            // Display Dialogue
            promptIndex = 0;
            Display_Dialogue();
        }
        private void Display_Dialogue()
        {
            if (promptIndex < numPromptRuns) //If within runs
            {
                //0. Remove Player Control
                hasControl = false;

                //1. Show dialogue box
                PopUpContainer.Visibility = Visibility.Visible;
                DialogueContainer.Visibility = Visibility.Visible;

                //2. Show dialogue in dialogueArray at promptIndex
                Dialogue_Text.Text = dialogueArray[promptIndex];

                //3. Show Continue Button
                DialogueContinue.Visibility = Visibility.Visible;
            }
            else //If exiting prompt run
            {
                //4. Re-hide dialogue box
                PopUpContainer.Visibility = Visibility.Hidden;
                DialogueContainer.Visibility = Visibility.Hidden;

                //6. Give player control back :)
                hasControl = true;
            }
        }
        private void Display_Prompt_Combat()
        {
            //0. Remove Player Control
            hasControl = false;

            //1. Show Combat Prompt
            PopUpContainer.Visibility = Visibility.Visible;
            CombatPromptContainer.Visibility = Visibility.Visible;
        }
        private void Display_Prompt_Town()
        {
            //0. Remove Player Control
            hasControl = false;

            //1. Show Town Prompt
            PopUpContainer.Visibility = Visibility.Visible;
            TownPromptContainer.Visibility = Visibility.Visible;

            //1.5 Set Town Prompt & Values Correctly

            //(Continues if player chooses 'No')
            //2. Hide Town Prompt
            PopUpContainer.Visibility = Visibility.Hidden;
            TownPromptContainer.Visibility = Visibility.Hidden;

            //3. Give Player Control back
            hasControl = true;
        }
        //Private method for prompting user (yes/no question)
        private void Display_Prompt()
        {

        }

        //Private method for updates on player move
        private void Player_Move()
        {
            if (encounterChance != 0)
            {
                Encounter_Chance();
            }

            //if lands on area teleport
            //1. show prompt and ask user if they want to go
            //yes = teleport (call Next_Map with the map to load)
            //no = do nothing

            //if lands on npc
            //1. show dialogue box and put npc dialogue in
            //2. click to skip dialogue/next dialogue/exit dialogue
            //3. hide dialogue box

            //if lands on object
            //1. show prompt and ask user choice about item
            //yes = do thing
            //no = do nothing
        }

        //Private method for starting combat
        private void Start_Combat(string enemyParty)
        {
            // Set true, return to this position when combat is over (if you win that is, hehe)
            ((MainWindow)Application.Current.MainWindow).returnToSavedPos = true;

            // Set enemyParty to fight
            ((MainWindow)Application.Current.MainWindow).enemyPartyName = enemyParty;

            // Switch to combat menu
            Display_Prompt_Combat();
        }

        //Control Panel Buttons
        private void Button_Menu_Click(object sender, RoutedEventArgs e)
        {
            if (hasControl)
            {
                if (pauseMenuOpen) // If Pause Menu Open, Close
                {
                    PauseMenuOverlayContainer.Visibility = Visibility.Hidden;
                    pauseMenuOpen = false;
                }
                else // If Pause Menu Closed, Open
                {
                    PauseMenuOverlayContainer.Visibility = Visibility.Visible;
                    pauseMenuOpen = true;
                }
            }
        }
        private void Button_Up_Click(object sender, RoutedEventArgs e)
        {
            if (hasControl)
            {
                // Validate movement method
                bool valid = Validate_Move(1);
                // Move Up
                if (valid)
                {
                    playerPosY--;
                    Map_Update_Render();
                }
            }
        }
        private void Button_Left_Click(object sender, RoutedEventArgs e)
        {
            if (hasControl)
            {
                // Validate movement method
                bool valid = Validate_Move(2);
                // Move Left
                if (valid)
                {
                    playerPosX--;
                    Map_Update_Render();
                }
            }
        }
        private void Button_Interact_Click(object sender, RoutedEventArgs e)
        {
            if (hasControl)
            {
                // Interact with current spot on map
                // Does this by running map update again, essentially acting like you moved to the current tile again
                Map_Update_Render();
            }
        }
        private void Button_Right_Click(object sender, RoutedEventArgs e)
        {
            if (hasControl)
            {
                // Validate movement method
                bool valid = Validate_Move(3);
                // Move Right
                if (valid)
                {
                    playerPosX++;
                    Map_Update_Render();
                }
            }
        }
        private void Button_Down_Click(object sender, RoutedEventArgs e)
        {
            if (hasControl)
            {
                // Validate movement method
                bool valid = Validate_Move(4);
                // Move Down
                if (valid)
                {
                    playerPosY++;
                    Map_Update_Render();
                }
            }
        }
        private void Button_Map_Click(object sender, RoutedEventArgs e)
        {
            if (hasControl)
            {
                // Open local map
            }
        }

        //Map Prompts
        private void Prompt_Yes_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Prompt_No_Click(object sender, RoutedEventArgs e)
        {

        }

        //Dialogue Continue Button
        private void DialogueContinue_Click(object sender, RoutedEventArgs e)
        {
            // Increment Prompt Index
            promptIndex++;

            // Hide button
            DialogueContinue.Visibility = Visibility.Hidden;

            // Call Display Dialogue again
            Display_Dialogue();
        }

        //Town No Button


        //Private methods for scaling text with resolution
        private void ScaleText()
        {
            switch (Application.Current.MainWindow.Width)
            {
                case 1366:
                    ChangeFontSize(0);
                    break;
                case 1600:
                    ChangeFontSize(1);
                    break;
                case 1920:
                    ChangeFontSize(2);
                    break;
                default:
                    break;
            }
        }
        private void ChangeFontSize(int size)
        {
            switch (size)
            {
                case 0: //1366
                    MapDisplayFontSize.FontSize = 32;
                    MapClickAnywhereFontSize.FontSize = 22;
                    break;
                case 1: //1600
                    MapDisplayFontSize.FontSize = 35;
                    MapClickAnywhereFontSize.FontSize = 25;
                    break;
                case 2: //1920
                    MapDisplayFontSize.FontSize = 45;
                    MapClickAnywhereFontSize.FontSize = 35;
                    break;
            }
        }

        //Methods for handling keyboard input
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // potential memory leak if not unloaded ? (uncertain question mark)
            var window = Window.GetWindow(this);
            overworldWindow = window;
            window.KeyDown += HandleKeyPress;
        }
        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            // Does not animate the button. The button animation is held within the Button's style. This only calls the base button's click, not the style.

            // If the pause menu is closed and player has control, then they can hit any button.
            // If the pause menu is open then the player can only hit the menu button to close it
            // If the player has no control then no buttons can be pressed

            if (!pauseMenuOpen && hasControl) //If the pause menu is closed and player has control
            {
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
            else
            {
                if (pauseMenuOpen) //If pause menu is open
                {
                    switch (e.Key)
                    {
                        case Key.Q:
                            Button_Menu.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                            break;
                    }
                }
            }
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            overworldWindow.KeyDown -= HandleKeyPress;
        }
    }
}
