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
    /// Interaction logic for PauseMenu.xaml
    /// </summary>
    public partial class PauseMenu : UserControl
    {
        //References
        Grid pauseMenuContainer;

        public PauseMenu(Grid pauseMenuContainer)
        {
            InitializeComponent();

            this.pauseMenuContainer = pauseMenuContainer;
        }

        #region Side Panel ToggleButtons
        private void Character_Button_Checked(object sender, RoutedEventArgs e)
        {
            //Uncheck all other buttons
            Map_Button.IsChecked = false;
            Close_Menu_Button.IsChecked = false;
            Exit_Game_Button.IsChecked = false;

            //Show Character Menu
            Character_Menu_Display.Visibility = Visibility.Visible;
        }
        private void Map_Button_Checked(object sender, RoutedEventArgs e)
        {
            //Uncheck all other buttons
            Character_Button.IsChecked = false;
            Close_Menu_Button.IsChecked = false;
            Exit_Game_Button.IsChecked = false;

            //Show Map Display

        }
        private void Close_Menu_Button_Checked(object sender, RoutedEventArgs e)
        {
            //Uncheck all other buttons
            Character_Button.IsChecked = false;
            Map_Button.IsChecked = false;
            Exit_Game_Button.IsChecked = false;

            //Uncheck Self
            Close_Menu_Button.IsChecked = false;

            //Close Menu
            pauseMenuContainer.Visibility = Visibility.Hidden;
        }
        private void Exit_Game_Button_Checked(object sender, RoutedEventArgs e)
        {
            //Uncheck all other buttons
            Character_Button.IsChecked = false;
            Map_Button.IsChecked = false;
            Close_Menu_Button.IsChecked = false;

            //Show Exit Game Prompt

        }
        #endregion
        #region Side Panel ToggleButtons Unchecked
        private void Character_Button_Unchecked(object sender, RoutedEventArgs e)
        {
            //Hide Character Menu
            Character_Menu_Display.Visibility = Visibility.Hidden;
        }
        private void Map_Button_Unchecked(object sender, RoutedEventArgs e)
        {
            //Hide Map Display

        }
        private void Exit_Game_Button_Unchecked(object sender, RoutedEventArgs e)
        {
            //Hide Exit Game Prompt
        }
        #endregion
        #region Character Buttons
        private void Character_One_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Character_Two_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Character_Three_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Character_Four_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
