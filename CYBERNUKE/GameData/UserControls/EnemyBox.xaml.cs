using System;
using System.Collections.Generic;
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
    /// </summary>
    public partial class EnemyBox : UserControl
    {
        //Vars
        bool IsDead = false;

        /*Stats
        int defense;
        int statStrength;
        int statDexterity;
        int statEndurance;
        int statIntelligence;
        double resSlash;
        double resPierce;
        double resBlunt;
        double resFire;
        double resWater;
        double resIce;
        double resElectric;
        double resEarth;
        double resWind;*/

        //TODO: Use file as input to initialize
        public EnemyBox(int MaxHP, int MaxSP, string Name)
        {
            InitializeComponent();
            ScaleText();

            // Initialize HP and SP variables
            HP_Bar.Value = (Double)MaxHP;
            SP_Bar.Value = (Double)MaxSP;

            // Initialize Name
            this.Name.Text = Name;

            // Initialize Portrait
            //TODO: Get identifier for portrait file

            // Initialize stats
            //TODO: Method or code to initialize stats
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
                    ChangeFontSize(39);
                    break;
                case 1920:
                    ChangeFontSize(49);
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
