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
        bool isPlayer;
        bool isEnemy;

        public TurnOrderBox(int type, int index, string name)
        {
            InitializeComponent();
            ScaleText();
            Get_Info(type, index, name);
        }

        public void Get_Info(int type, int index, string name)
        {
            switch(type)
            {
                case 0: //player
                    isPlayer = true;
                    break;

                case 1: //enemy
                    isEnemy = true;
                    break;

                default:
                    break;
            }

            Index.Text = index.ToString();

            Name.Text = name;
        }

        //Private classes for scaling text with resolution
        private void ScaleText()
        {
            switch (Application.Current.MainWindow.Width)
            {
                case 1366:
                    break;
                case 1600:
                    ChangeFontSize(33);
                    break;
                case 1920:
                    ChangeFontSize(42);
                    break;
                default:
                    break;
            }
        }
        private void ChangeFontSize(double size)
        {
            Name.FontSize = size;
            Index.FontSize = size;
        }
    }
}
