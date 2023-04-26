using CYBERNUKE.MVVM.Model;
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
    /// Interaction logic for TurnOrderBox.xaml
    /// </summary>
    public partial class TurnOrderBox : UserControl
    {
        //Vars
        public bool isPlayer = false;
        public bool isEnemy = false;
        public int currentEnemyIndex = -1;

        //References
        public Character tiedChar;
        public EnemyBox tiedEnemy;

        //Player Constructor
        public TurnOrderBox(Character character)
        {
            InitializeComponent();
            ScaleText();
            Get_Info_Player(character);
        }
        //Enemy Constructor
        public TurnOrderBox(int index, EnemyBox enemy)
        {
            InitializeComponent();
            ScaleText();
            Get_Info_Enemy(index, enemy);
        }

        //Player Info
        private void Get_Info_Player(Character character)
        {
            isPlayer = true;

            CombatantName.Text = character.getName();

            tiedChar = character;

            Index.Visibility = Visibility.Hidden;
        }
        //Enemy Info
        private void Get_Info_Enemy(int index, EnemyBox enemy)
        {
            isEnemy = true;

            currentEnemyIndex = index;

            Index.Text = index.ToString();

            CombatantName.Text = enemy.GetName();

            tiedEnemy = enemy;
        }

        //Public method for retrieving dex of tied char/enemy
        public int Get_Dexterity()
        {
            if (isPlayer)
            {
                return tiedChar.getStatDexterity();
            }
            else
            {
                return tiedEnemy.statDexterity;
            }
        }

        //Public method for retrieving name of tied char/enemy
        public string Get_Name()
        {
            if (isPlayer)
            {
                return tiedChar.getName();
            }
            else
            {
                return tiedEnemy.GetName();
            }
        }

        //Public method to call when about to delete this box
        public void Prep_Delete()
        {
            tiedChar = null;
            tiedEnemy = null;
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
                    FontSizeVar.FontSize = 24;
                    break;
                case 1: //1600
                    FontSizeVar.FontSize = 28;
                    break;
                case 2: //1920
                    FontSizeVar.FontSize = 33;
                    break;
            }
        }
    }
}
