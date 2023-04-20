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
    /// Interaction logic for PlayerBox.xaml
    /// </summary>
    public partial class PlayerBox : UserControl
    {
        int currenthp;
        int currentsp;
        bool isActive = false;

        public PlayerBox(string name, int currenthp, int maxhp, int currentsp, int maxsp)
        {
            InitializeComponent();

            // Name
            MainBG_Name.Text = name;
            ActiveBG_Name.Text = name;
            // HP
            MainBG_HP_Bar.Maximum = maxhp;
            MainBG_HP_Bar.Value = currenthp;
            ActiveBG_HP_Bar.Maximum = maxhp;
            ActiveBG_HP_Bar.Value = currenthp;

            this.currenthp = currenthp;
            // SP
            MainBG_SP_Bar.Maximum = maxsp;
            MainBG_SP_Bar.Value = currentsp;
            ActiveBG_SP_Bar.Maximum = maxsp;
            ActiveBG_SP_Bar.Value = currentsp;

            this.currentsp = currentsp;
        }

        public void ToggleActive()
        {
            if (isActive)
            {
                isActive = false;
                // Set MainBG Visible
                PlayerBox_MainBG.Visibility = Visibility.Visible;
                PlayerBoxInfo_MainBG.Visibility = Visibility.Visible;
                // Set ActiveBG Hidden
                PlayerBoxInfo_ActiveBG.Visibility = Visibility.Hidden;
                PlayerBox_ActiveBG.Visibility = Visibility.Hidden;
            }
            else
            {
                isActive = true;
                // Set MainBG Hidden
                PlayerBox_MainBG.Visibility = Visibility.Hidden;
                PlayerBoxInfo_MainBG.Visibility = Visibility.Hidden;
                // Set ActiveBG Visible
                PlayerBoxInfo_ActiveBG.Visibility = Visibility.Visible;
                PlayerBox_ActiveBG.Visibility = Visibility.Visible;
            }
        }
    }
}
