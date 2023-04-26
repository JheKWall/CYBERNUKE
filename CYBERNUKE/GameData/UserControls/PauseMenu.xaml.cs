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
    /// Interaction logic for PauseMenu.xaml
    /// </summary>
    public partial class PauseMenu : UserControl
    {
        //References
        Grid pauseMenuContainer;
        int currentChar;

        public PauseMenu(Grid pauseMenuContainer)
        {
            InitializeComponent();
            ScaleText();
            Init_ComboBoxes();

            this.pauseMenuContainer = pauseMenuContainer;
        }

        #region Equipment Boxes
        private void Init_ComboBoxes()
        {
            //Armor Combobox
            List<string> armorNameList = new List<string>();
            for (int i = 0; i < ((MainWindow)Application.Current.MainWindow).ArmorList.Count; i++)
            {
                armorNameList.Add(((MainWindow)Application.Current.MainWindow).ArmorList[i].getName());
            }
            ArmorListBox.ItemsSource = armorNameList;
        }

        private void ArmorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Get index of armor
            int index = ArmorListBox.SelectedIndex;
            MainArmor selectedArmor = ((MainWindow)Application.Current.MainWindow).ArmorList[index];

            //Set armor on char
            ((MainWindow)Application.Current.MainWindow).CharacterList[currentChar].setEquippedOutfit(selectedArmor);
        }
        #endregion

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
            //Hide Other Selects
            Char_One_Select.Visibility = Visibility.Hidden;
            Char_Two_Select.Visibility = Visibility.Hidden;
            Char_Three_Select.Visibility = Visibility.Hidden;

            //Show Select
            Char_One_Select.Visibility = Visibility.Visible;
            currentChar = 0;

            //Hide Other Character Info

            //Show Character Info

        }
        private void Character_Two_Click(object sender, RoutedEventArgs e)
        {
            //Hide Other Selects
            Char_One_Select.Visibility = Visibility.Hidden;
            Char_Two_Select.Visibility = Visibility.Hidden;
            Char_Three_Select.Visibility = Visibility.Hidden;

            //Show Select
            Char_Two_Select.Visibility = Visibility.Visible;
            currentChar = 1;

            //Hide Other Character Info

            //Show Character Info

        }
        private void Character_Three_Click(object sender, RoutedEventArgs e)
        {
            //Hide Other Selects
            Char_One_Select.Visibility = Visibility.Hidden;
            Char_Two_Select.Visibility = Visibility.Hidden;
            Char_Three_Select.Visibility = Visibility.Hidden;

            //Show Select
            Char_Three_Select.Visibility = Visibility.Visible;
            currentChar = 2;

            //Hide Other Character Info

            //Show Character Info

        }
        private void Character_Four_Click(object sender, RoutedEventArgs e)
        {
            //didnt bother since we only have 3 for demo
        }
        #endregion

        #region Text Scaling
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
                    FontSizeVar.FontSize = 38;
                    InfoFontSizeVar.FontSize = 70;
                    HeaderFontSizeVar.FontSize = 50;
                    break;
                case 1: //1600
                    FontSizeVar.FontSize = 42;
                    InfoFontSizeVar.FontSize = 74;
                    HeaderFontSizeVar.FontSize = 54;
                    break;
                case 2: //1920
                    FontSizeVar.FontSize = 46;
                    InfoFontSizeVar.FontSize = 78;
                    HeaderFontSizeVar.FontSize = 58;
                    break;
            }
        }
        #endregion
    }
}
