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
    /// Interaction logic for OverworldBox.xaml
    /// </summary>
    public partial class OverworldBox : UserControl
    {
        public OverworldBox(string name, int currenthp, int maxhp, int currentsp, int maxsp)
        {
            InitializeComponent();
            ScaleText();

            // Get Name
            PlayerName.Text = name;

            // Get HP
            HP_Display.Text = "HP: " + currenthp.ToString() + "/" + maxhp.ToString();

            // Get SP
            SP_Display.Text = "SP: " + currentsp.ToString() + "/" + maxsp.ToString();
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
                    OverworldBoxNameFontSize.FontSize = 46;
                    OverworldBoxStatFontSize.FontSize = 36;
                    break;
                case 1: //1600
                    OverworldBoxNameFontSize.FontSize = 51;
                    OverworldBoxStatFontSize.FontSize = 41;
                    break;
                case 2: //1920
                    OverworldBoxNameFontSize.FontSize = 59;
                    OverworldBoxStatFontSize.FontSize = 49;
                    break;
            }
        }
    }
}
