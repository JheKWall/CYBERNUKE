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

namespace CYBERNUKE.MVVM.View
{
    /// <summary>
    /// Interaction logic for CombatView.xaml
    /// </summary>
    public partial class CombatView : UserControl
    {
        /// <summary>
        /// TERMINOLOGY: Each set of buttons is called a "MENU" not to be confused with the combat MENU
        /// 
        /// TODO LIST:
        /// 1. Generating Buttons that each have different functionality
        /// 2. Determine which menu to display
        /// 3. Generate Enemy/Player Boxes
        /// 4. Link Enemy/Player Boxes to their respective Turn Order Boxes
        /// </summary>

        int enemyCount; //Number of enemies in combat
        int playerCount; //Number of players in combat

        //string[] ListPlayerTargets; //List of all player targets (Max 4)
        EnemyBox[] ListEnemyTargets = new EnemyBox[6]; //List of all enemy targets (Max 6)
        //string[] ListPlayerSkills; //List of the current character's skills, initialized on turn start
        //string[] ListInventory; //List of usable items

        //TODO: Initialize player/enemy count on combat start
        //TODO: Get enemy party composition on combat start
        public CombatView()
        {
            InitializeComponent();
            ScaleText();

            enemyCount = 6;
            playerCount = 0;

            /*
            // Add Players
            // TODO: File input(?) for player characters & stats
            for (int i = 0; i < playerCount; i++)
            {
                AddPlayer();
            }*/

            // Add Enemies
            // TODO: File input for enemy party and individual enemies
            for (int i = 0; i < enemyCount; i++)
            {
                AddEnemy(i);
            }

            // Combat Start Text
            ControlPanel_Left_TextBlock.Text = "!! COMBAT START !!";
        }

        //
        //TODO: Take file input for a specific enemy
        private void AddEnemy(int index)
        {
            // Initialize new enemy, assign it to index
            EnemyBox enemy = new EnemyBox("Lesser_Zombie");
            ListEnemyTargets[index] = enemy;

            CombatMenu_EnemyBoxPanel.Children.Add(enemy);

            // Create new turn order box for that enemy

        }

        //
        //TODO: Take file input for a character
        private void AddPlayer()
        {
            // Create new player box

            // Create new turn order box for that player

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
