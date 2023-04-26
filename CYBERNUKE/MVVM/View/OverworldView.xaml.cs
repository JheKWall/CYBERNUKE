using CYBERNUKE.GameData.UserControls;
using CYBERNUKE.MVVM.Model;
using CYBERNUKE.MVVM.ViewModel;
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
        string mapTeleport;
        string townTeleport;
        string objectActivate;
        int objectIndex;
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

            pauseMenu = new PauseMenu(PauseMenuOverlayContainer);
            OverworldView_CharacterMenuContainer.Children.Add(pauseMenu);

            hasControl = true;
        }

        #region Map Loading Methods
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

            // Set Map Name & Encounter %
            MapDisplay_Location.Text = mapName;
            MapDisplay_EnemyPercent.Text = encounterChance + "%";

            // Initialize map data lists
            mapData = new char[mapHeight, mapWidth];
            dynamicMap = new char[mapHeight, mapWidth];
            mapData = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].mapData;

            #region Set Player Spawn on dynamicMap
            // Check if "returnToSavedPos" is true (set when entering combat)
            if (((MainWindow)Application.Current.MainWindow).returnToSavedPos)
            {
                playerPosY = ((MainWindow)Application.Current.MainWindow).currentPosY;
                playerPosX = ((MainWindow)Application.Current.MainWindow).currentPosX;
            }
            else
            {
                int spawnIndex;

                //If coming from Town
                if (((MainWindow)Application.Current.MainWindow).TownToMap)
                {
                    string currentTown = ((MainWindow)Application.Current.MainWindow).currentTown;
                    spawnIndex = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].Get_Spawn_Index(currentTown);

                    if (spawnIndex == -1) //-1 means it couldnt find the mapname in the location data, so it defaults to default spawn location
                    {
                        playerPosY = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].defaultSpawnY;
                        playerPosX = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].defaultSpawnX;
                    }
                    else
                    {
                        playerPosY = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].townTeleportLocationData[spawnIndex].locationCoordY;
                        playerPosX = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].townTeleportLocationData[spawnIndex].locationCoordX;
                    }
                }
                //Else you're coming from another map/just spawning in
                else
                {
                    spawnIndex = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].Get_Spawn_Index(currentMap);

                    if (spawnIndex == -1) //-1 means it couldnt find the mapname in the location data, so it defaults to default spawn location
                    {
                        playerPosY = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].defaultSpawnY;
                        playerPosX = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].defaultSpawnX;
                    }
                    else
                    {
                        playerPosY = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].teleportLocationData[spawnIndex].locationCoordY;
                        playerPosX = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].teleportLocationData[spawnIndex].locationCoordX;
                    }
                }
            }
            #endregion
            ((MainWindow)Application.Current.MainWindow).returnToSavedPos = false;
            ((MainWindow)Application.Current.MainWindow).TownToMap = false;

            #region Remove Encounter Tile if Enemy is Defeated
            // If you return as isEncounter is true, remove the encounter at your position
            if (((MainWindow)Application.Current.MainWindow).isEncounter)
            {
                int locationIndex = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].Get_Enemy_Pos(playerPosX, playerPosY);
                ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].Remove_Location(4, playerPosX, playerPosY);
                mapData[playerPosY, playerPosX] = '⬜';
            }
            #endregion
            // Clear isEncounter
            ((MainWindow)Application.Current.MainWindow).isEncounter = false;

            #region Remove Object Tile & their Target if Object is Activated
            for (int i = 0; i < ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].objectLocationData.Count; i++)
            {
                if (((MainWindow)Application.Current.MainWindow).mapList[currentIndex].objectLocationData[i].boolVar)
                {
                    mapData[((MainWindow)Application.Current.MainWindow).mapList[currentIndex].objectLocationData[i].locationCoordY,
                        ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].objectLocationData[i].locationCoordX] = '⬜';

                    mapData[((MainWindow)Application.Current.MainWindow).mapList[currentIndex].objectLocationData[i].targetCoordY,
                        ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].objectLocationData[i].targetCoordX] = '⬜';
                }
            }
            #endregion

            #region Copy mapData to dynamicMap
            // Deep copy of mapdata into dynamicmap
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    dynamicMap[i, j] = mapData[i, j];
                }
            }
            #endregion
            dynamicMap[playerPosY, playerPosX] = '☢';

            #region Render Map to Screen
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    MapDisplay.Text += dynamicMap[i, j];
                }
            }
            #endregion

            // Set new current map
            ((MainWindow)Application.Current.MainWindow).currentMap = ((MainWindow)Application.Current.MainWindow).mapToLoad;
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
        //Private method for loading into another map
        private void Next_Map()
        {
            //Called when map navigate is prompted (Display_Prompt_Map)
            //((MainWindow)Application.Current.MainWindow).mapToLoad = mapTeleport;
            //((MainWindow)Application.Current.MainWindow).Get_Map();
            Map_First_Render();
        }
        #endregion

        #region Player-Related Methods
        //Private method for loading player boxes
        private void Load_Player_Boxes()
        {
            playerCount = ((MainWindow)Application.Current.MainWindow).numPartyMembers;
            ListCharacters.Capacity = playerCount;

            // Get Players
            for (int i = 0; i < playerCount; i++)
            {
                ListCharacters.Insert(i, ((MainWindow)Application.Current.MainWindow).CharacterList[i]);
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
        //Private method for updating overworld boxes
        private void Update_Player_Boxes()
        {
            OV_PB_Pos1.Children.Clear();
            OV_PB_Pos2.Children.Clear();
            OV_PB_Pos3.Children.Clear();
            OV_PB_Pos4.Children.Clear();

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
        //Private method for regenerating player SP
        private void Regenerate_Player_SP()
        {
            for (int i = 0; i < playerCount; i++)
            {
                ListCharacters[i].rechargeSP(30);
            }
        }
        //Private method for healing players
        private void Heal_Player_HP()
        {
            for (int i = 0; i < playerCount; i++)
            {
                ListCharacters[i].healHP(15);
            }
        }
        #endregion

        #region Player Move Update Methods
        //Private method for updates on player move
        private void Player_Move()
        {
            //Regenerate Player SP every step
            Regenerate_Player_SP();
            Heal_Player_HP();
            Update_Player_Boxes();

            if (encounterChance != 0)
            {
                Encounter_Chance();
            }

            //0 == teleport, 1 == town teleport, 2 == npc, 3 == object, 4 == enemy
            int tile = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].Check_Player_Pos(playerPosX, playerPosY);
            int locationIndex;

            switch (tile)
            {
                case -1: //not on a special tile
                    break;

                case 0: //Teleport
                    locationIndex = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].Get_Teleport_Pos(playerPosX, playerPosY);
                    Display_Prompt_Map(((MainWindow)Application.Current.MainWindow).mapList[currentIndex].teleportLocationData[locationIndex].locationName);
                    break;

                case 1: // Town Teleport
                    locationIndex = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].Get_TownTeleport_Pos(playerPosX, playerPosY);
                    Display_Prompt_Town(((MainWindow)Application.Current.MainWindow).mapList[currentIndex].townTeleportLocationData[locationIndex].locationName);
                    break;

                case 2: //NPC
                    locationIndex = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].Get_NPC_Pos(playerPosX, playerPosY);
                    Init_Dialogue(((MainWindow)Application.Current.MainWindow).mapList[currentIndex].npcLocationData[locationIndex].locationName);
                    break;

                case 3: //Object
                    locationIndex = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].Get_Object_Pos(playerPosX, playerPosY);
                    objectIndex = locationIndex;

                    //Check if object is activated, if it isnt then proceed
                    if (!((MainWindow)Application.Current.MainWindow).mapList[currentIndex].objectLocationData[locationIndex].boolVar)
                    {
                        Display_Object_Prompt(((MainWindow)Application.Current.MainWindow).mapList[currentIndex].objectLocationData[locationIndex].locationName);
                    }
                    break;

                case 4: //Enemy
                    locationIndex = ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].Get_Enemy_Pos(playerPosX, playerPosY);
                    ((MainWindow)Application.Current.MainWindow).isEncounter = true;
                    Display_Prompt_Combat(((MainWindow)Application.Current.MainWindow).mapList[currentIndex].enemyLocationData[locationIndex].locationName);
                    break;
            }
        }
        private void Encounter_Chance()
        {
            //1. Generate a number from 0 to 100
            //2. If it falls below the encounter chance, congrats you are being attacked
            //3. Else, continue on with your day
            Random rand = new Random();
            int chance = rand.Next(0, 200);
            if (chance <= encounterChance)
            {
                //4. Get random enemy party
                int index = rand.Next(((MainWindow)Application.Current.MainWindow).enemyPartyList.Count);
                string enemyParty = ((MainWindow)Application.Current.MainWindow).enemyPartyList[index];
                Display_Prompt_Combat(enemyParty);
            }
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
        #endregion

        #region DialoguePrompt
        //Public & private method for displaying popups
        public void Init_Dialogue(string dialogueName) //Call when starting prompt
        {
            //Weird bug sometimes where spamming 'Interact' at a teleport spot opens combat and teleport prompt which messes up index and crashes
            PopUpContainer.Visibility = Visibility.Hidden;
            CombatPromptContainer.Visibility = Visibility.Hidden;

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
        #endregion
        #region CombatPrompt
        private void Display_Prompt_Combat(string enemyParty)
        {
            //0. Remove Player Control
            hasControl = false;

            //1. Set enemyParty to fight
            ((MainWindow)Application.Current.MainWindow).enemyPartyName = enemyParty;

            //2.. Show Combat Prompt
            PopUpContainer.Visibility = Visibility.Visible;
            CombatPromptContainer.Visibility = Visibility.Visible;
        }
        private void CombatContinue_Click(object sender, RoutedEventArgs e)
        {
            // Set true, return to this position when combat is over (if you win that is, hehe)
            ((MainWindow)Application.Current.MainWindow).returnToSavedPos = true;

            // Switch to combat view
            var viewModel = (OverworldViewModel)DataContext;
            if (viewModel.NavigateCombatViewCommand.CanExecute(null))
            {
                viewModel.NavigateCombatViewCommand.Execute(null);
            }
        }
        #endregion
        #region TownPrompt
        private void Display_Prompt_Town(string townName) //Ex: TranquilityTown (NameTown)
        {
            // Hide Combat Prompt //Weird bug sometimes where spamming 'Interact' at a teleport spot opens combat and teleport prompt which fucks index and crashes
            PopUpContainer.Visibility = Visibility.Hidden;
            CombatPromptContainer.Visibility = Visibility.Hidden;

            // Set townTeleport 
            townTeleport = townName;

            // Remove Player Control
            hasControl = false;

            // Set Town Prompt
            Town_Prompt.Text = "ENTER THE TOWN OF TRANQUILITY?";

            // Show Town Prompt
            PopUpContainer.Visibility = Visibility.Visible;
            TownPromptContainer.Visibility = Visibility.Visible;
        }

        //Map Prompts
        private void Town_Prompt_Yes_Click(object sender, RoutedEventArgs e)
        {
            // Set mapdata in MainWindow
            ((MainWindow)Application.Current.MainWindow).townToLoad = townTeleport;

            // Switch to town view
            var viewModel = (OverworldViewModel)DataContext;
            if (viewModel.NavigateTownViewCommand.CanExecute(null))
            {
                viewModel.NavigateTownViewCommand.Execute(null);
            }
        }
        private void Town_Prompt_No_Click(object sender, RoutedEventArgs e)
        {
            // Hide Town Prompt
            PopUpContainer.Visibility = Visibility.Hidden;
            TownPromptContainer.Visibility = Visibility.Hidden;

            // Give Player Control back
            hasControl = true;
        }
        #endregion
        #region MapPrompt
        private void Display_Prompt_Map(string mapName)
        {
            // Hide Combat Prompt //Weird bug sometimes where spamming 'Interact' at a teleport spot opens combat and teleport prompt which fucks index and crashes
            PopUpContainer.Visibility = Visibility.Hidden;
            CombatPromptContainer.Visibility = Visibility.Hidden;

            // Set mapTeleport
            mapTeleport = mapName;

            // Remove Player Control
            hasControl = false;

            // Call GetMap() in MainWindow to init map
            ((MainWindow)Application.Current.MainWindow).mapToLoad = mapTeleport;
            int mapIndex = ((MainWindow)Application.Current.MainWindow).Get_Map();

            // Set Map Prompt
            string nextMapName = ((MainWindow)Application.Current.MainWindow).mapList[mapIndex].mapName;
            Map_Prompt.Text = "GO TO " + nextMapName.ToUpper() + "?";

            // Show Map Prompt
            PopUpContainer.Visibility = Visibility.Visible;
            MapPromptContainer.Visibility = Visibility.Visible;
        }
        private void Map_Prompt_Yes_Click(object sender, RoutedEventArgs e)
        {
            // Hide Map Prompt
            PopUpContainer.Visibility = Visibility.Hidden;
            MapPromptContainer.Visibility = Visibility.Hidden;

            // Set mapToLoad in MainWindow with mapTeleport
            ((MainWindow)Application.Current.MainWindow).mapToLoad = mapTeleport;

            // Call Next_Map()
            Next_Map();

            // Return Control
            hasControl = true;
        }
        private void Map_Prompt_No_Click(object sender, RoutedEventArgs e)
        {
            // Hide Map Prompt
            PopUpContainer.Visibility = Visibility.Hidden;
            MapPromptContainer.Visibility = Visibility.Hidden;

            // Return Control
            hasControl = true;
        }
        #endregion
        #region ObjectPrompt
        private void Display_Object_Prompt(string objectName)
        {
            // Hide Combat Prompt //Weird bug sometimes where spamming 'Interact' at a teleport spot opens combat and teleport prompt which fucks index and crashes
            PopUpContainer.Visibility = Visibility.Hidden;
            CombatPromptContainer.Visibility = Visibility.Hidden;

            // Set objectActivate
            objectActivate = objectName;

            // Remove Player Control
            hasControl = false;

            #region Get Dialogue
            input = new StreamReader("GameData/Dialogue/" + objectName + ".txt");
            #endregion

            // Set Prompt Dialogue
            input.ReadLine();
            Object_Prompt.Text = input.ReadLine();

            // Close Streamreader
            input.Close();

            // Show Object Prompt
            PopUpContainer.Visibility = Visibility.Visible;
            ObjectPromptContainer.Visibility = Visibility.Visible;
        }

        private void Object_Prompt_Yes_Click(object sender, RoutedEventArgs e)
        {
            // Toggle Object
            ((MainWindow)Application.Current.MainWindow).mapList[currentIndex].objectLocationData[objectIndex].boolVar = true;

            // returnToSavedPos true
            ((MainWindow)Application.Current.MainWindow).returnToSavedPos = true;

            // Hide Object Prompt
            PopUpContainer.Visibility = Visibility.Hidden;
            ObjectPromptContainer.Visibility = Visibility.Hidden;

            // Return Control
            hasControl = true;

            // Map_First_Render()
            Map_First_Render();
        }
        private void Object_Prompt_No_Click(object sender, RoutedEventArgs e)
        {
            // Hide Object Prompt
            PopUpContainer.Visibility = Visibility.Hidden;
            ObjectPromptContainer.Visibility = Visibility.Hidden;

            // Return Control
            hasControl = true;
        }
        #endregion

        #region Control Panel Buttons
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
        #endregion

        #region Meta Methods
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

            // Just incase pause menu is bugged
            if (PauseMenuOverlayContainer.Visibility == Visibility.Hidden)
            {
                pauseMenuOpen = false;
            }

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
        #endregion
    }
}
