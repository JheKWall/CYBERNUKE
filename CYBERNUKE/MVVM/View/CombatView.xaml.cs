using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using CYBERNUKE.GameData.UserControls;
using CYBERNUKE.MVVM.Model;
using CYBERNUKE.MVVM.ViewModel;

namespace CYBERNUKE.MVVM.View
{
    /// <summary>
    /// Interaction logic for CombatView.xaml
    /// </summary>
    public partial class CombatView : UserControl
    {
        //File reader
        private StreamReader input;

        //Vars
        int enemyCount; //Number of enemies in combat
        int playerCount; //Number of players in combat
        int numEnemyDead = 0; //Number of enemies dead
        int numPlayerDead = 0; //Number of players dead

        //Arrays/Lists
        Character[] ListPlayerTargets = new Character[4]; //List of all player targets (Max 4)
        EnemyBox[] ListEnemyTargets = new EnemyBox[6]; //List of all enemy targets (Max 6)
        List<TurnOrderBox> TurnOrderBoxList = new List<TurnOrderBox>(); //Turn order list of all combat participants

        //Turn Order Vars
        int totalTurnOrder = 0; //Total number of turn order instances
        int turnOrderIndex = 0; //Current "first" position in turn order
        bool isPlayerTurn = false; //For deciding if user has control or not
        bool allPlayersDead = false;

        public CombatView()
        {
            InitializeComponent();
            ScaleText();

            //0. If this is an Encounter, Disable Flee Button
            if (((MainWindow)Application.Current.MainWindow).isEncounter)
            {
                CPBP_MAIN_ESCAPE.IsEnabled = false;
            }

            //1. Load Players into ListPlayerTargets
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

            //2. Load Enemies into ListEnemyTargets
            // Initialize StreamReader to Enemy Party file (enemyParty is pulled from MainWindow.xaml.cs)
            string enemyParty = ((MainWindow)Application.Current.MainWindow).enemyPartyName;
            input = new StreamReader("GameData/EnemyData/EnemyParty/" + enemyParty + ".txt");
            enemyCount = Int32.Parse(input.ReadLine());
            // Add EnemyBoxes
            for (int i = 0; i < enemyCount; i++)
            {
                AddEnemyBox(input.ReadLine(), i);
            }
            // Close StreamReader
            input.Close();

            // Start Turn Order Initialization
            TurnOrder_Init();

            // Combat Start Text
            ControlPanel_Right_TextBlock.Text = "!! COMBAT START !!";
        }

        private void TurnOrder_Init()
        {
            //3. Insert into TurnOrderBoxList from highest to lowest Dexterity

            // Initial player insert
            TurnOrderBox player = new TurnOrderBox(ListPlayerTargets[0]);
            ListPlayerTargets[0].Set_TurnOrder(player);
            TurnOrderBoxList.Add(player);
            totalTurnOrder++;

            // Player loop
            for (int i = 1; i < playerCount; i++)
            {
                player = new TurnOrderBox(ListPlayerTargets[i]);
                ListPlayerTargets[i].Set_TurnOrder(player);
                totalTurnOrder++;

                for (int j = 0; j < (TurnOrderBoxList.Count + 1); j++) // Start at the beginning of the list, and go to list count + 1
                {
                    if (j == TurnOrderBoxList.Count) // If j reaches the actual count, just add player to the end of the list
                    {
                        TurnOrderBoxList.Add(player);
                        j = 99; //I understand setting the index to 99 just to exit to loop may not be the most "correct" thing to do but I'm currently one step away from insanity at the hands of the machine gods.
                    }
                    else // Else, check if the player's dex is greater than the current dex, if it is then insert it in there
                    {
                        if (player.Get_Dexterity() > TurnOrderBoxList[j].Get_Dexterity())
                        {
                            TurnOrderBoxList.Insert(j, player);
                            j = 99;
                        }
                    }
                }
            }

            // Enemy loop
            for (int i = 0; i < enemyCount; i++)
            {
                TurnOrderBox enemy = new TurnOrderBox(i, ListEnemyTargets[i]);
                ListEnemyTargets[i].Set_TurnOrder(enemy);
                totalTurnOrder++;

                for (int j = 0; j < (TurnOrderBoxList.Count + 1); j++)
                {
                    if (j == TurnOrderBoxList.Count)
                    {
                        TurnOrderBoxList.Add(enemy);
                        j = 99;
                    }
                    else
                    {
                        if (enemy.Get_Dexterity() > TurnOrderBoxList[j].Get_Dexterity())
                        {
                            TurnOrderBoxList.Insert(j, enemy);
                            j = 99;
                        }
                    }
                }
            }

            // Render Turn Order
            //1. Get first item in list
            TurnOrderBox temp = TurnOrderBoxList.First();
            //1.5 If first is player, set to Active, start player turn
            if (temp.isPlayer)
            {
                temp.tiedChar.currentPlayerBox.ToggleActive();
                isPlayerTurn = true;
            }
            else //Else meaning its an enemy, not player turn
            {
                isPlayerTurn = false;
            }
            //2. Display new list on screen
            Render_TO_Box();
            //3. Start that player/enemy's turn
            if (isPlayerTurn)
            {
                StartPlayerTurn();
            }
            else
            {
                StartEnemyTurn();
            }
        }
        private void TurnOrder_Update()
        {
            // INDEX THAT RUNS THROUGH LIST (as opposed to manually changing the order of the list)
            // The index determines where the "first" turn order is in the list
            // Index == turnOrderIndex

            // Flush turn order display
            Clear_TO_Boxes();

            //0. Verify Turn Order Integrity
            if (turnOrderIndex >= totalTurnOrder)  
            {
                //Rare bug, basically this is needed because if someone dies
                //and the turnOrderIndex is at the last element the turnOrderIndex enters this method with an invalid index
                turnOrderIndex--;
            }

            //1. Get first combatant in list w/ index
            TurnOrderBox temp = TurnOrderBoxList[turnOrderIndex];
            //1.5 If first is player, set Active to false
            if (temp.isPlayer)
            {
                temp.tiedChar.currentPlayerBox.ToggleActive();
            }
            //2. Increment turnOrderIndex (new turn for next combatant)
            if (turnOrderIndex >= totalTurnOrder - 1)
            {
                turnOrderIndex = 0;
            }
            else
            {
                turnOrderIndex++;
            }
            //3. Get new first combatant in list w/ updated index
            temp = TurnOrderBoxList[turnOrderIndex];
            //3.5 If new first is player, set Active to true, start player turn
            if (temp.isPlayer)
            {
                temp.tiedChar.currentPlayerBox.ToggleActive();
                isPlayerTurn = true;
            }
            else //Else meaning its an enemy, not player turn
            {
                isPlayerTurn = false;
            }
            //4. Display new list on screen
            Render_TO_Box();
            //5. Start that player/enemy's turn
            if (isPlayerTurn)
            {
                StartPlayerTurn();
            }
            else
            {
                StartEnemyTurn();
            }
        }
        private void Clear_TO_Boxes()
        {
            TO_Pos1.Children.Clear();
            TO_Pos2.Children.Clear();
            TO_Pos3.Children.Clear();
            TO_Pos4.Children.Clear();
            TO_Pos5.Children.Clear();
            TO_Pos6.Children.Clear();
            TO_Pos7.Children.Clear();
            TO_Pos8.Children.Clear();
            TO_Pos9.Children.Clear();
            TO_Pos10.Children.Clear();
        }
        private void Render_TO_Box()
        {
            for (int i = 0, j = turnOrderIndex; i < totalTurnOrder; i++, j++)
            {
                if (j == totalTurnOrder)
                {
                    j = 0;
                }

                switch (i)
                {
                    case 0:
                        TO_Pos1.Children.Add(TurnOrderBoxList[j]);
                        break;
                    case 1:
                        TO_Pos2.Children.Add(TurnOrderBoxList[j]);
                        break;
                    case 2:
                        TO_Pos3.Children.Add(TurnOrderBoxList[j]);
                        break;
                    case 3:
                        TO_Pos4.Children.Add(TurnOrderBoxList[j]);
                        break;
                    case 4:
                        TO_Pos5.Children.Add(TurnOrderBoxList[j]);
                        break;
                    case 5:
                        TO_Pos6.Children.Add(TurnOrderBoxList[j]);
                        break;
                    case 6:
                        TO_Pos7.Children.Add(TurnOrderBoxList[j]);
                        break;
                    case 7:
                        TO_Pos8.Children.Add(TurnOrderBoxList[j]);
                        break;
                    case 8:
                        TO_Pos9.Children.Add(TurnOrderBoxList[j]);
                        break;
                    case 9:
                        TO_Pos10.Children.Add(TurnOrderBoxList[j]);
                        break;
                }
            }
        }

        private void StartEnemyTurn()
        {
            // CHECK IF ENEMY HAS ENOUGH SP TO ATTACK (if not, rest and regain sp)
            if (TurnOrderBoxList[turnOrderIndex].tiedEnemy.GetSP() < TurnOrderBoxList[turnOrderIndex].tiedEnemy.costSP)
            {
                //rest
                TurnOrderBoxList[turnOrderIndex].tiedEnemy.RechargeSP(15);
                ///BROADCAST ACTION
                ControlPanel_Right_TextBlock.Text += "\n" + TurnOrderBoxList[turnOrderIndex].Get_Name() + " waits and regains 15 SP.";
                ControlPanel_ScrollViewer.ScrollToEnd();

                //end turn
                EndTurn();
            }
            else
            {
                // USE RANDOM TO TARGET A RANDOM PLAYER
                Random rand = new Random();
                int target = rand.Next(0, (playerCount - 1) - numPlayerDead);

                // GET PLAYER'S POSITION IN TURN ORDER LIST
                int playerTurnOrderIndex = 0;
                for (int i = 0; i < totalTurnOrder; i++)
                {
                    if (TurnOrderBoxList[i].Get_Name() == ListPlayerTargets[target].getName())
                    {
                        playerTurnOrderIndex = i;
                        i = 99;
                    }
                }

                // CALCULATE DAMAGE
                int defense = TurnOrderBoxList[playerTurnOrderIndex].tiedChar.getDefense();
                int attack = TurnOrderBoxList[turnOrderIndex].tiedEnemy.amountDamage;
                int strength = TurnOrderBoxList[turnOrderIndex].tiedEnemy.statStrength;
                int attackDamage = CalculateAttack(defense, attack, strength);

                // INCUR SP COST FOR ATTACK
                TurnOrderBoxList[turnOrderIndex].tiedEnemy.AttackLoseSP();

                // ATTACK PLAYER
                TurnOrderBoxList[playerTurnOrderIndex].tiedChar.takeDamage(attackDamage);
                TurnOrderBoxList[playerTurnOrderIndex].tiedChar.currentPlayerBox.UpdateVals();
                ///Broadcast Action
                ControlPanel_Right_TextBlock.Text += "\n" + TurnOrderBoxList[turnOrderIndex].Get_Name() + " attacks " + TurnOrderBoxList[playerTurnOrderIndex].Get_Name() + " and deals " + attackDamage + " damage!";
                ControlPanel_ScrollViewer.ScrollToEnd();

                // CHECK IF PLAYER DIED, IF THEY DID RUN KILLPLAYER()
                if (TurnOrderBoxList[playerTurnOrderIndex].tiedChar.getIsUnconscious())
                {
                    ///Broadcast Action
                    ControlPanel_Right_TextBlock.Text += "\n" + TurnOrderBoxList[playerTurnOrderIndex].Get_Name() + " was knocked out!";
                    ControlPanel_ScrollViewer.ScrollToEnd();

                    TurnOrderBoxList[playerTurnOrderIndex].Prep_Delete();
                    TurnOrderBoxList.RemoveAt(playerTurnOrderIndex);

                    totalTurnOrder--;
                    numPlayerDead++;
                }

                // END TURN
                EndTurn();
            }
        }
        private void StartPlayerTurn()
        {
            // Check if they have enough SP to attack, if not then disable attack
            if (TurnOrderBoxList[turnOrderIndex].tiedChar.getCurrentSP() < TurnOrderBoxList[turnOrderIndex].tiedChar.getMainWeaponSPCost())
            {
                CPBP_MAIN_FIGHT.IsEnabled = false;
            }
            else
            {
                CPBP_MAIN_FIGHT.IsEnabled = true;
            }

            // Disable all enemy targets on start
            CPBP_ATTACK_ENEMYONE.IsEnabled = false;
            CPBP_ATTACK_ENEMYTWO.IsEnabled = false;
            CPBP_ATTACK_ENEMYTHREE.IsEnabled = false;
            CPBP_ATTACK_ENEMYFOUR.IsEnabled = false;
            CPBP_ATTACK_ENEMYFIVE.IsEnabled = false;
            CPBP_ATTACK_ENEMYSIX.IsEnabled = false;

            // Check which enemies are valid targets and enable target buttons as needed
            for (int i = 0; i < totalTurnOrder; i++)
            {
                if (TurnOrderBoxList[i].isEnemy)
                {
                    int enemyIndex = TurnOrderBoxList[i].currentEnemyIndex;

                    switch (enemyIndex)
                    {
                        case 0:
                            CPBP_ATTACK_ENEMYONE.IsEnabled = true;
                            break;
                        case 1:
                            CPBP_ATTACK_ENEMYTWO.IsEnabled = true;
                            break;
                        case 2:
                            CPBP_ATTACK_ENEMYTHREE.IsEnabled = true;
                            break;
                        case 3:
                            CPBP_ATTACK_ENEMYFOUR.IsEnabled = true;
                            break;
                        case 4:
                            CPBP_ATTACK_ENEMYFIVE.IsEnabled = true;
                            break;
                        case 5:
                            CPBP_ATTACK_ENEMYSIX.IsEnabled = true;
                            break;
                    }
                }
            }

            // SHOW MAIN CONTROLS
            ControlPanel_ButtonPanel_MAIN.Visibility = Visibility.Visible;
        }
        private void PlayerAttack(int enemyIndex)
        {
            //0. Get Enemy's position in turn order list
            int enemyTurnOrderIndex = 0; //uh oh
            for (int i = 0; i < totalTurnOrder; i++)
            {
                if (TurnOrderBoxList[i].currentEnemyIndex == enemyIndex)
                {
                    enemyTurnOrderIndex = i;
                    i = 99;
                }
            }

            //1. Calculate attack
            int defense = TurnOrderBoxList[enemyTurnOrderIndex].tiedEnemy.defense;
            int attack = TurnOrderBoxList[turnOrderIndex].tiedChar.getMainWeaponDamage();
            int strength = TurnOrderBoxList[turnOrderIndex].tiedChar.getStatStrength();
            int attackDamage = CalculateAttack(defense, attack, strength);

            //2. Subtract SP cost of attack
            TurnOrderBoxList[turnOrderIndex].tiedChar.incurWeaponSPCost();
            TurnOrderBoxList[turnOrderIndex].tiedChar.currentPlayerBox.UpdateVals();

            //3. Attack enemy
            TurnOrderBoxList[enemyTurnOrderIndex].tiedEnemy.ModifyHP(attackDamage);
            ///Broadcast Action
            ControlPanel_Right_TextBlock.Text += "\n" + TurnOrderBoxList[turnOrderIndex].Get_Name() + " attacks " + TurnOrderBoxList[enemyTurnOrderIndex].Get_Name() + " and deals " + attackDamage + " damage!";
            ControlPanel_ScrollViewer.ScrollToEnd();

            //4. Check if enemy died (yes = talk to enemy vars, no = continue)
            if (TurnOrderBoxList[enemyTurnOrderIndex].tiedEnemy.IsDead)
            {
                ///Broadcast Action
                ControlPanel_Right_TextBlock.Text += "\n" + TurnOrderBoxList[enemyTurnOrderIndex].Get_Name() + " was defeated!";
                ControlPanel_ScrollViewer.ScrollToEnd();

                TurnOrderBoxList[enemyTurnOrderIndex].Prep_Delete();
                TurnOrderBoxList.RemoveAt(enemyTurnOrderIndex);

                totalTurnOrder--;
                numEnemyDead++;
            }

            //5. Remove control
            ControlPanel_ButtonPanel_MAIN.Visibility = Visibility.Hidden;
            ControlPanel_ButtonPanel_ATTACK.Visibility = Visibility.Hidden;
            //6. END TURN
            EndTurn();
        }
        private int CalculateAttack(int defense, int attack, int strength)
        {
            //Victim: Defense (10)
            //Attacker: AttackPower, Strength (40, 6)
            //ATTACK FORMULA: (int)Damage = (AttackPower + (AttackPower * (Strength / 5))) - (Defense)
            //Ex: (40 + (40 * (6 / 10))) - 10 = 54 Damage

            int damage = (attack + (attack * (strength / 10))) - defense;
            if (damage < 0)
            {
                damage = 0;
            }
            return damage;
        }

        private void EndTurn()
        {
            // Check if all enemies or player are dead
            // If all players dead, game over
            if (numPlayerDead == playerCount)
            {
                allPlayersDead = true;
                EnemyEndCombat();
            }
            // If all enemies dead, win
            if (numEnemyDead == enemyCount)
            {
                PlayerEndCombat();
            }
            else
            {
                if (!allPlayersDead)
                {
                    // Make sure players dont have control
                    ControlPanel_ButtonPanel_MAIN.Visibility = Visibility.Hidden;
                    ControlPanel_ButtonPanel_ATTACK.Visibility = Visibility.Hidden;

                    TurnOrder_Update();
                }
            }
        }
        private void PlayerEndCombat() //Player victory or Escaped
        {
            //Apply all damage, sp changes, etc
            for (int i = 0; i < playerCount; i++)
            {
                if (ListPlayerTargets[i].getIsUnconscious())
                {
                    ListPlayerTargets[i].setCurrentHP(10);
                    ListPlayerTargets[i].setIsUnconscious(false);
                }
            }

            //Return to overworld view
            var viewModel = (CombatViewModel)DataContext;
            if (viewModel.NavigateOverworldViewCommand.CanExecute(null))
            {
                viewModel.NavigateOverworldViewCommand.Execute(null);
            }
        }
        private void EnemyEndCombat() //Enemy victory, game over
        {
            //Show game over screen

            //kick player to main menu
            var viewModel = (CombatViewModel)DataContext;
            if (viewModel.NavigateMainMenuViewCommand.CanExecute(null))
            {
                viewModel.NavigateMainMenuViewCommand.Execute(null);
            }
        }

        //Private method for adding an enemy from a file and with an index
        private void AddEnemyBox(string enemyName, int index)
        {
            // Initialize new enemy, assign it to index
            EnemyBox enemy = new EnemyBox(enemyName, index);
            ListEnemyTargets[index] = enemy;

            //1 John 1:9
            //If we confess our sins, he is faithful and just and will forgive us our sins and purify us from all unrighteousness.
            //Sorry for embedding switch statements inside a switch statement.
            switch (enemyCount)
            {
                case 1: //1 Enemy
                    EBD_1_1.Children.Add(enemy);
                    break;

                case 2: //2 Enemies
                    switch (index)
                    {
                        case 0:
                            EBD_2_1.Children.Add(enemy);
                            break;
                        case 1:
                            EBD_2_2.Children.Add(enemy);
                            break;
                    }
                    break;

                case 3: //3 Enemies
                    switch (index)
                    {
                        case 0:
                            EBD_3_1.Children.Add(enemy);
                            break;
                        case 1:
                            EBD_3_2.Children.Add(enemy);
                            break;
                        case 2:
                            EBD_3_3.Children.Add(enemy);
                            break;
                    }
                    break;

                case 4: //4 Enemies
                    switch (index)
                    {
                        case 0:
                            EBD_4_1.Children.Add(enemy);
                            break;
                        case 1:
                            EBD_4_2.Children.Add(enemy);
                            break;
                        case 2:
                            EBD_4_3.Children.Add(enemy);
                            break;
                        case 3:
                            EBD_4_4.Children.Add(enemy);
                            break;
                    }
                    break;

                case 5: //5 Enemies
                    switch (index)
                    {
                        case 0:
                            EBD_5_1.Children.Add(enemy);
                            break;
                        case 1:
                            EBD_5_2.Children.Add(enemy);
                            break;
                        case 2:
                            EBD_5_3.Children.Add(enemy);
                            break;
                        case 3:
                            EBD_5_4.Children.Add(enemy);
                            break;
                        case 4:
                            EBD_5_5.Children.Add(enemy);
                            break;
                    }
                    break;

                case 6: //6 Enemies
                    switch (index)
                    {
                        case 0:
                            EBD_6_1.Children.Add(enemy);
                            break;
                        case 1:
                            EBD_6_2.Children.Add(enemy);
                            break;
                        case 2:
                            EBD_6_3.Children.Add(enemy);
                            break;
                        case 3:
                            EBD_6_4.Children.Add(enemy);
                            break;
                        case 4:
                            EBD_6_5.Children.Add(enemy);
                            break;
                        case 5:
                            EBD_6_6.Children.Add(enemy);
                            break;
                    }
                    break;
            }
        }
        //Private method for adding a new player from ListPlayerTargets + index
        private void AddPlayerBox(int index)
        {
            // Player vars
            string name = ListPlayerTargets[index].getName();
            int currenthp = ListPlayerTargets[index].getCurrentHP();
            int maxhp = ListPlayerTargets[index].getMaxHP();
            int currentsp = ListPlayerTargets[index].getCurrentSP();
            int maxsp = ListPlayerTargets[index].getMaxSP();

            // Create new player box
            PlayerBox player = new PlayerBox(name, currenthp, maxhp, currentsp, maxsp, ListPlayerTargets[index]);
            // Reference it to Character
            ListPlayerTargets[index].Set_PlayerBox(player);

            switch (index)
            {
                case 0:
                    CM_Player1_Pos.Children.Add(player);
                    break;

                case 1:
                    CM_Player2_Pos.Children.Add(player);
                    break;

                case 2:
                    CM_Player3_Pos.Children.Add(player);
                    break;

                case 3:
                    CM_Player4_Pos.Children.Add(player);
                    break;
            }
        }

        ///Main Option Buttons
        private void CPBP_MAIN_FIGHT_Click(object sender, RoutedEventArgs e)
        {
            // Hide Main Menu
            ControlPanel_ButtonPanel_MAIN.Visibility = Visibility.Hidden;
            // Show Fight Menu
            ControlPanel_ButtonPanel_ATTACK.Visibility = Visibility.Visible;
        }
        private void CPBP_MAIN_WAIT_Click(object sender, RoutedEventArgs e)
        {
            // Recharge 15 SP
            TurnOrderBoxList[turnOrderIndex].tiedChar.rechargeSP(15);
            TurnOrderBoxList[turnOrderIndex].tiedChar.currentPlayerBox.UpdateVals();
            ///Broadcast Action
            ControlPanel_Right_TextBlock.Text += "\n" + TurnOrderBoxList[turnOrderIndex].Get_Name() + " waits and regains 15 SP.";
            ControlPanel_ScrollViewer.ScrollToEnd();

            // Remove control
            ControlPanel_ButtonPanel_MAIN.Visibility = Visibility.Hidden;
            ControlPanel_ButtonPanel_ATTACK.Visibility = Visibility.Hidden;
            // END TURN
            EndTurn();
        }
        private void CPBP_MAIN_ESCAPE_Click(object sender, RoutedEventArgs e)
        {
            // Remove control
            ControlPanel_ButtonPanel_ATTACK.Visibility = Visibility.Hidden;
            // END COMBAT
            PlayerEndCombat();
        }

        ///Attack Option Buttons (Enemy Targets)
        private void CPBP_ATTACK_ENEMYONE_Click(object sender, RoutedEventArgs e)
        {
            // Attack enemy
            PlayerAttack(0);
            // Remove control
            ControlPanel_ButtonPanel_ATTACK.Visibility = Visibility.Hidden;
        }
        private void CPBP_ATTACK_ENEMYTWO_Click(object sender, RoutedEventArgs e)
        {
            // Attack enemy
            PlayerAttack(1);
            // Remove control
            ControlPanel_ButtonPanel_ATTACK.Visibility = Visibility.Hidden;
        }
        private void CPBP_ATTACK_ENEMYTHREE_Click(object sender, RoutedEventArgs e)
        {
            // Attack enemy
            PlayerAttack(2);
            // Remove control
            ControlPanel_ButtonPanel_ATTACK.Visibility = Visibility.Hidden;
        }
        private void CPBP_ATTACK_ENEMYFOUR_Click(object sender, RoutedEventArgs e)
        {
            // Attack enemy
            PlayerAttack(3);
            // Remove control
            ControlPanel_ButtonPanel_ATTACK.Visibility = Visibility.Hidden;
        }
        private void CPBP_ATTACK_ENEMYFIVE_Click(object sender, RoutedEventArgs e)
        {
            // Attack enemy
            PlayerAttack(4);
            // Remove control
            ControlPanel_ButtonPanel_ATTACK.Visibility = Visibility.Hidden;
        }
        private void CPBP_ATTACK_ENEMYSIX_Click(object sender, RoutedEventArgs e)
        {
            // Attack enemy
            PlayerAttack(5);
            // Remove control
            ControlPanel_ButtonPanel_ATTACK.Visibility = Visibility.Hidden;
        }
        private void CPBP_ATTACK_Return_Click(object sender, RoutedEventArgs e)
        {
            // Hide Target Menu
            ControlPanel_ButtonPanel_ATTACK.Visibility = Visibility.Hidden;
            // Show Main Menu
            ControlPanel_ButtonPanel_MAIN.Visibility = Visibility.Visible;
        }

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
                    FontSizeVar.FontSize = 28;
                    break;
                case 1: //1600
                    FontSizeVar.FontSize = 34;
                    break;
                case 2: //1920
                    FontSizeVar.FontSize = 40;
                    break;
            }
            //ControlPanelFontSize.Margin = new Thickness(0, size + 8, 0, 0);
        }
        //Private method for making sure the combat menu does not crap itself on exit.
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < playerCount; i++)
            {
                ListPlayerTargets[i].Clear_PlayerBox();
                ListPlayerTargets[i].Clear_TurnOrder();
            }
        }
    }
}
