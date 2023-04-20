using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CYBERNUKE.GameData.UserControls;
using CYBERNUKE.MVVM.Model;

namespace CYBERNUKE.MVVM.View
{
    /// <summary>
    /// Interaction logic for CombatView.xaml
    /// </summary>
    public partial class CombatView : UserControl
    {
        /// <summary>
        /// 1. Load Players into ListPlayerTargets
        /// 2. Load Enemies into ListEnemyTargets
        /// 3. Gather all Dexterity Stats
        /// 4. Insert into TurnOrderBoxList from highest to lowest Dex
        /// 5. Render TurnOrderBoxList
        /// 6. Call UpdateList each turn
        /// 7. Remove enemy/player from list when killed
        /// </summary>

        //File reader
        private StreamReader input;

        //Vars
        int enemyCount; //Number of enemies in combat
        int playerCount; //Number of players in combat

        //Arrays/Lists
        Character[] ListPlayerTargets = new Character[4]; //List of all player targets (Max 4)
        EnemyBox[] ListEnemyTargets = new EnemyBox[6]; //List of all enemy targets (Max 6)
        List<TurnOrderBox> TurnOrderBoxList = new List<TurnOrderBox>();
        int numTurnOrderBoxes = 0;

        //string[] ListPlayerSkills; //List of the current character's skills, initialized on turn start
        //string[] ListInventory; //List of usable items

        public CombatView()
        {
            InitializeComponent();
            ScaleText();

            // Get Players, add them to ListPlayerTargets
            for (int i = 0; i < 3; i++)
            {
                if (((MainWindow)Application.Current.MainWindow).CharacterList[i] != null)
                {
                    ListPlayerTargets[i] = ((MainWindow)Application.Current.MainWindow).CharacterList[i];
                    playerCount++;
                }
            }
            // Add PlayersBoxes
            for (int i = 0; i < playerCount; i++)
            {
                AddPlayerBox(i);
            }

            // Initialize StreamReader to Enemy Party file
            // enemyParty is pulled from MainWindow.xaml.cs
            string enemyParty = ((MainWindow)Application.Current.MainWindow).enemyPartyName;
            input = new StreamReader("GameData/EnemyData/EnemyParty/" + enemyParty + ".txt");
            enemyCount = Int32.Parse(input.ReadLine());
            ResizeEnemyGrid();
            // Add EnemyBoxes
            for (int i = 0; i < enemyCount; i++)
            {
                AddEnemyBox(input.ReadLine(), i);
            }

            // Close StreamReader
            input.Close();
            // Combat Start Text
            ControlPanel_Left_TextBlock.Text = "!! COMBAT START !!";
            // Turn Order Update
        }

        private void TurnOrder_Init()
        {
            //Sort elements by dex stat
        }
        private void TurnOrder_Update()
        {
            // Flush turn order display
            CombatMenu_TurnOrderPanel.Children.Clear();

            //1. Get first item in list
            TurnOrderBox temp = TurnOrderBoxList.First();
            //2. Add it to the end of the list
            TurnOrderBoxList.Add(temp);
            //3. Remove it from the front of the list
            TurnOrderBoxList.RemoveAt(0);

            // Display new list on screen
            for (int i = 0; i < numTurnOrderBoxes; i++)
            {
                CombatMenu_TurnOrderPanel.Children.Add(TurnOrderBoxList[i]);
            }
        }
        private void AddTurnOrder(TurnOrderBox box)
        {
            // Add box to boxlist
            TurnOrderBoxList.Add(box);
        }

        //Private method for adding an enemy from a file and with an index
        private void AddEnemyBox(string enemyName, int index)
        {
            // Initialize new enemy, assign it to index
            EnemyBox enemy = new EnemyBox(enemyName, index);
            ListEnemyTargets[index] = enemy;

            CombatMenu_EnemyBoxPanel.Children.Add(enemy);
        }

        //
        private void AddPlayerBox(int index)
        {
            // Player vars
            string name = ListPlayerTargets[index].getName();
            int currenthp = ListPlayerTargets[index].getCurrentHP();
            int maxhp = ListPlayerTargets[index].getMaxHP();
            int currentsp = ListPlayerTargets[index].getCurrentSP();
            int maxsp = ListPlayerTargets[index].getMaxSP();

            // Create new player box
            PlayerBox player = new PlayerBox(name, currenthp, maxhp, currentsp, maxsp);

            CombatMenu_PlayerBoxPanel.Children.Add(player);
        }

        //Private method for resizing CombatMenu_EnemyBoxPanel UniformGrid
        private void ResizeEnemyGrid()
        {
            switch (enemyCount)
            {
                case 1:
                    CombatMenu_EnemyBoxPanel.Columns = 1;
                    CombatMenu_EnemyBoxPanel.Rows = 1;
                    break;

                case 2:
                    CombatMenu_EnemyBoxPanel.Columns = 2;
                    CombatMenu_EnemyBoxPanel.Rows = 1;
                    break;

                case 3:
                    CombatMenu_EnemyBoxPanel.Columns = 3;
                    CombatMenu_EnemyBoxPanel.Rows = 1;
                    break;

                case 4:
                    CombatMenu_EnemyBoxPanel.Columns = 2;
                    CombatMenu_EnemyBoxPanel.Rows = 2;
                    break;

                case 5:
                    CombatMenu_EnemyBoxPanel.Columns = 3;
                    CombatMenu_EnemyBoxPanel.Rows = 2;
                    break;

                case 6:
                    CombatMenu_EnemyBoxPanel.Columns = 3;
                    CombatMenu_EnemyBoxPanel.Rows = 2;
                    break;

                default:
                    break;
            }
        }

        //Private classes for scaling text with resolution
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
            ControlPanelFontSize.FontSize = size;
            ControlPanelFontSize.Margin = new Thickness(0, size + 8, 0, 0);
        }

        ///Main Option Buttons
        private void CPBP_MAIN_FIGHT_Click(object sender, RoutedEventArgs e)
        {
            // Hide Main Menu
            ControlPanel_ButtonPanel_MAIN.Visibility = Visibility.Hidden;
            // Show Fight Menu
            ControlPanel_ButtonPanel_FIGHT.Visibility = Visibility.Visible;
        }
        private void CPBP_MAIN_ITEM_Click(object sender, RoutedEventArgs e)
        {
            // Hide Main Menu

            // Show Inventory

            // Exit Inventory if player chooses to

            // When item is selected, show player/enemy targets (whatever is applicable)

            // Use item, then end turn

        }
        private void CPBP_MAIN_GUARD_Click(object sender, RoutedEventArgs e)
        {
            // Make character guard, then end turn

        }
        private void CPBP_MAIN_ANALYZE_Click(object sender, RoutedEventArgs e)
        {
            // Hide Main Menu

            // Show Analyze Targets
            //Either show both player and enemy targets, or show one at a time and switch between

            // Show status of selected target (TURN DOES NOT END)

            // Return to Main Menu 

        }
        private void CPBP_MAIN_ESCAPE_Click(object sender, RoutedEventArgs e)
        {
            // Random chance to escape or to fail escape

            // If successful, return to overworld/whever fight started

            // If failed, end turn

        }

        ///Fight Option Buttons 
        private void CPBP_FIGHT_MELEE_Click(object sender, RoutedEventArgs e)
        {
            // Shows enemy targets and waits for user to select one or cancel

            // If enemy is selected, start attack and end turn

            // If player cancels, return to base menu

        }
        private void CPBP_FIGHT_RANGED_Click(object sender, RoutedEventArgs e)
        {
            // Shows enemy targets and waits for user to select one or cancel

            // If enemy is selected, start attack and end turn

            // If player cancels, return to base menu

        }
        private void CPBP_FIGHT_SKILL_Click(object sender, RoutedEventArgs e)
        {
            // Shows character's skills and waits for user to select one or cancel

            // If skill is selected, show appropriate targetting menu (healing skills for players, lethal skills for enemies, etc)

            // If player cancels, return to skill menu

            // If player cancels again, return to fight menu

        }
        private void CPBP_FIGHT_RETURN_Click(object sender, RoutedEventArgs e)
        {
            // Hide Fight Menu
            ControlPanel_ButtonPanel_FIGHT.Visibility = Visibility.Hidden;
            // Show Main Menu
            ControlPanel_ButtonPanel_MAIN.Visibility = Visibility.Visible;
        }
    }
}
