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
        bool IsDead = false;

        //Stats
        int defense;
        int statStrength;
        int statDexterity;
        int statEndurance;
        int statIntelligence;
        int amountDamage;
        int costSP;
        double resSlash;
        double resPierce;
        double resBlunt;
        double resFire;
        double resWater;
        double resIce;
        double resElectric;
        double resEarth;
        double resWind;
        bool isSlash;
        bool isPierce;
        bool isBlunt;
        bool isFire;
        bool isWater;
        bool isIce;
        bool isElectric;
        bool isEarth;
        bool isWind;

        //TODO: Use file as input to initialize
        public EnemyBox(string enemyName)
        {
            InitializeComponent();
            ScaleText();
            ReadEnemyData(enemyName);
        }

        //Private class for reading enemy data
        private void ReadEnemyData(string enemyName)
        {
            // Initialize StreamReader toe EnemyData.txt
            input = new StreamReader("GameData/EnemyData/" + enemyName + ".txt");

            // Read in all enemy data (sorry for coding)
            this.Name.Text = input.ReadLine();
            defense = Int32.Parse(input.ReadLine());
            HP_Bar.Maximum = Double.Parse(input.ReadLine());
            HP_Bar.Value = HP_Bar.Maximum;
            SP_Bar.Maximum = Double.Parse(input.ReadLine());
            SP_Bar.Value = SP_Bar.Maximum;

            statStrength = Int32.Parse(input.ReadLine());
            statDexterity = Int32.Parse(input.ReadLine());
            statEndurance = Int32.Parse(input.ReadLine());
            statIntelligence = Int32.Parse(input.ReadLine());

            resSlash = Double.Parse(input.ReadLine());
            resPierce = Double.Parse(input.ReadLine());
            resBlunt = Double.Parse(input.ReadLine());
            resFire = Double.Parse(input.ReadLine());
            resWater = Double.Parse(input.ReadLine());
            resIce = Double.Parse(input.ReadLine());
            resElectric = Double.Parse(input.ReadLine());
            resEarth = Double.Parse(input.ReadLine());
            resWind = Double.Parse(input.ReadLine());

            isSlash = input.ReadLine() == "1";
            isPierce = input.ReadLine() == "1";
            isBlunt = input.ReadLine() == "1";
            isFire = input.ReadLine() == "1";
            isWater = input.ReadLine() == "1";
            isIce = input.ReadLine() == "1";
            isElectric = input.ReadLine() == "1";
            isEarth = input.ReadLine() == "1";
            isWind = input.ReadLine() == "1";

            // Initialize Portrait
            Portrait.Source = new BitmapImage(new Uri("pack://application:,,,/CYBERNUKE;component/GameData/Images/EnemyPortrait/" + enemyName + "_Portrait.png"));

            // Initialize Skills

            // Read in all skill data
            //TODO: Skills (maybe a list?)

            // End Read
            input.Close();
        }


        //Public class for attacking players
        //TODO: Attack player method

        //Public class for using skills
        //TODO: Use skill method

        //Public class for modifying enemy's HP
        public void ModifyHP(int AmountHP)
        {
            // Check if dead
            if (IsDead)
            {
                throw new Exception("Enemy is dead");
            }

            // Modify HP
            HP_Bar.Value += AmountHP;

            // Check if Dead
            if (HP_Bar.Value <= 0)
            {
                IsDead = true;
            }
        }

        //Public class for modifying enemy's SP
        public void ModifySP(int AmountSP)
        {
            // Check if dead
            if (IsDead)
            {
                throw new Exception("Enemy is dead");
            }

            // Modify SP
            HP_Bar.Value += AmountSP;
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



        //Private classes for scaling text with resolution
        private void ScaleText()
        {
            switch (Application.Current.MainWindow.Width)
            {
                case 1366:
                    break;
                case 1600:
                    ChangeFontSize(37);
                    break;
                case 1920:
                    ChangeFontSize(46);
                    break;
                default:
                    break;
            }
        }
        private void ChangeFontSize(double size)
        {
            Name.FontSize = size;
            HP_Text.FontSize = size;
            SP_Text.FontSize = size;
        }
    }
}
