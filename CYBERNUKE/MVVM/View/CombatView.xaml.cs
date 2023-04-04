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

namespace CYBERNUKE.MVVM.View
{
    /// <summary>
    /// Interaction logic for CombatView.xaml
    /// </summary>
    public partial class CombatView : UserControl
    {

        public CombatView()
        {
            InitializeComponent();

            // Combat Start Text
            ControlPanel_Left_TextBlock.Text = "!! COMBAT START !!";

            // Display Options
            ControlPanel_DisplayOptions();

            
        }

        //
        public void ControlPanel_DisplayOptions()
        {
            /* Final Options
            ControlPanel_Left_TextBlock.Text +=
                "\nINPUT COMMAND:\n" +
                "\n1. ATTACK\t2. SKILL" +
                "\n3. ITEM\t4. ANALYZE" +
                "\n5. GUARD\t6. WAIT" +
                "\n7. ESCAPE\t8. AUTO";
            */

            /* Debug Options for Test
            ControlPanel_Left_TextBlock.Text +=
                "\nINPUT COMMAND:\n" +
                "\n1. Add Enemy\t2. Add Player";
            */
        }

        //
        //TODO: Take file input for a specific enemy
        public void AddEnemy()
        {
            // Create new enemy box
            var enemy = new Image();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("CombatMenu_EnemyBox_1.PNG", UriKind.Relative);
            bitmapImage.EndInit();
            enemy.Stretch = Stretch.Fill;
            enemy.Source = bitmapImage;

            CombatMenu_EnemyBoxPanel.Children.Add(enemy);

            // Create new turn order box
        }

        //
        //TODO: Take file input for a character
        public void AddPlayer()
        {

        }

        //
        public void InputReader()
        {

        }
    }
}
