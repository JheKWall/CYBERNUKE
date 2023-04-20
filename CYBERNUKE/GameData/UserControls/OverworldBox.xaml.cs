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

            // Get Name
            Name.Text = name;

            // Get HP
            HP_Display.Text = "HP: " + currenthp.ToString() + "/" + maxhp.ToString();

            // Get SP
            SP_Display.Text = "SP: " + currentsp.ToString() + "/" + maxsp.ToString();
        }
    }
}
