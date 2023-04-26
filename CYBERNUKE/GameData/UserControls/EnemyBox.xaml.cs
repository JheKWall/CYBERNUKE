using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace CYBERNUKE.GameData.UserControls
{
    /// <summary>
    /// EnemyBox Object that holds an instance of an ENEMY.
    /// This contains all of an enemy's stats, resistances, SP, HP, etc.
    /// 
    /// Incoming damage is filtered in ModifyHP()
    /// </summary>
    public partial class EnemyBox : UserControl
    {
        //File reader
        private StreamReader input;

        //Vars
        string name;
        public bool IsDead = false;

        //Stats
        public int defense;
        public int statStrength;
        public int statDexterity;
        public int statEndurance;
        public int statIntelligence;
        public int amountDamage;
        public int costSP;

        //References
        public TurnOrderBox currentTurnOrder;

        public EnemyBox(string enemyName, int index)
        {
            InitializeComponent();
            ScaleText();
            ReadEnemyData(enemyName, index);
        }

        //Private class for reading enemy data
        private void ReadEnemyData(string enemyName, int index)
        {
            // Initialize StreamReader toe EnemyData.txt
            input = new StreamReader("GameData/EnemyData/" + enemyName + ".txt");

            // Name & Index
            name = input.ReadLine();
            EnemyName.Text = index.ToString() + " // " + name;

            // Read in all enemy data (sorry for coding)
            defense = Int32.Parse(input.ReadLine());
            HP_Bar.Maximum = Double.Parse(input.ReadLine());
            HP_Bar.Value = HP_Bar.Maximum;
            SP_Bar.Maximum = Double.Parse(input.ReadLine());
            SP_Bar.Value = SP_Bar.Maximum;

            statStrength = Int32.Parse(input.ReadLine());
            statDexterity = Int32.Parse(input.ReadLine());
            statEndurance = Int32.Parse(input.ReadLine());
            statIntelligence = Int32.Parse(input.ReadLine());

            amountDamage = Int32.Parse(input.ReadLine());
            costSP = Int32.Parse(input.ReadLine());

            // Initialize Portrait
            Portrait.Source = new BitmapImage(new Uri("pack://application:,,,/CYBERNUKE;component/GameData/Images/EnemyPortrait/" + enemyName + "_Portrait.png"));

            // End Read
            input.Close();
        }

        //Public class for attacking players
        //TODO: Attack player method

        //Public class for modifying enemy's HP
        public void ModifyHP(int AmountHP)
        {
            // Check if dead
            if (IsDead)
            {
                throw new Exception("Enemy is dead");
            }

            // Modify HP
            HP_Bar.Value -= AmountHP;

            // Check if Dead
            if (HP_Bar.Value <= 0)
            {
                IsDead = true;
                DestroyEnemy();
            }
        }

        //Public class for modifying enemy's SP
        private void ModifySP(int AmountSP, int action) //2nd val is (0 == lose sp) (1 == gain sp)
        {
            // Check if dead
            if (IsDead)
            {
                throw new Exception("Enemy is dead");
            }

            switch (action)
            {
                case 0:
                    SP_Bar.Value -= AmountSP;
                    break;
                case 1:
                    SP_Bar.Value += AmountSP;
                    break;
            }
        }

        //Private class for destroying the enemy
        private void DestroyEnemy()
        {
            // Hide all elements
            Card.Visibility = Visibility.Hidden;
            DynamicElements.Visibility = Visibility.Hidden;

            // Show destroyed image
            Card_Destroyed.Visibility = Visibility.Visible;
        }

        //Public class for getting Current HP
        public int GetHP()
        {
            return (int)HP_Bar.Value;
        }
        //Public class for getting Current SP
        public int GetSP()
        {
            return (int)SP_Bar.Value;
        }
        public void RechargeSP(int rechargeAmount)
        {
            if (GetSP() < SP_Bar.Maximum)
            {
                ModifySP(rechargeAmount, 1);
            }
            if (GetSP() > (SP_Bar.Maximum - rechargeAmount))
            {
                SP_Bar.Value = SP_Bar.Maximum;
            }
        }
        public void AttackLoseSP()
        {
            ModifySP(costSP, 0);
        }

        //Public class for getting name
        public string GetName()
        {
            return name;
        }

        //Public class for turn order
        public void Set_TurnOrder(TurnOrderBox turnOrder)
        {
            currentTurnOrder = turnOrder;
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
                    FontSizeVar.FontSize = 32;
                    break;
                case 1: //1600
                    FontSizeVar.FontSize = 38;
                    break;
                case 2: //1920
                    FontSizeVar.FontSize = 44;
                    break;
            }
        }
    }
}
