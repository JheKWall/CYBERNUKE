using CYBERNUKE.Core;
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

namespace CYBERNUKE.MVVM.View
{
    /// <summary>
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : UserControl
    {
        // Resolution Variable List
        readonly double[] ListResolutionWidth = new double[3] { 1366, 1600, 1920 };
        readonly double[] ListResolutionHeight = new double[3] { 768, 900, 1080 };
        int ResolutionIndex = 0;

        // Constructor
        public MainMenuView()
        {
            InitializeComponent();
        }

        // Start Button
        private void MainMenu_StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            //Hide Main Menu Options and Highlights
            MainMenu_OptionsPanel.Visibility = Visibility.Hidden;
            MainMenu_OptionsHighlight.Visibility = Visibility.Hidden;

            //Show Start Prompt
            MainMenu_StartPrompt.Visibility = Visibility.Visible;
        }
        private void StartPrompt_YesButton_Click(object sender, RoutedEventArgs e)
        {
            //Start New Game

        }
        private void StartPrompt_NoButton_Click(object sender, RoutedEventArgs e)
        {
            //Hide Start Prompt
            MainMenu_StartPrompt.Visibility = Visibility.Hidden;

            //Show Main Menu Options and Highlights
            MainMenu_OptionsPanel.Visibility = Visibility.Visible;
            MainMenu_OptionsHighlight.Visibility = Visibility.Visible;
        }


        // Load Game Button
        private void MainMenu_LoadGameButton_Click(object sender, RoutedEventArgs e)
        {
            //Hide Main Menu Options and Highlights
            MainMenu_OptionsPanel.Visibility = Visibility.Hidden;
            MainMenu_OptionsHighlight.Visibility = Visibility.Hidden;

            //Show Load Menu

        }


        // Options Menu Button
        private void MainMenu_OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            //Hide Main Menu Options and Highlights
            MainMenu_OptionsPanel.Visibility = Visibility.Hidden;
            MainMenu_OptionsHighlight.Visibility = Visibility.Hidden;

            //Show Options Menu
            MainMenu_OptionsMenu.Visibility = Visibility.Visible;
        }
        // Options Menu: Resolution Change Buttons
        private void OptionsMenu_Resolution_ButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            if (ResolutionIndex > 0)
            {
                ResolutionIndex--;
                UpdateScreenResolution(ListResolutionWidth[ResolutionIndex], ListResolutionHeight[ResolutionIndex]);
            }
        }
        private void OptionsMenu_Resolution_ButtonRight_Click(object sender, RoutedEventArgs e)
        {
            if (ResolutionIndex < 2)
            {
                ResolutionIndex++;
                UpdateScreenResolution(ListResolutionWidth[ResolutionIndex], ListResolutionHeight[ResolutionIndex]);
            }
        }

        // Options Menu: Fullscreen/Windowed Button
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //Fullscreens the game
            Application.Current.MainWindow.WindowStyle = WindowStyle.None;
            Application.Current.MainWindow.WindowState = WindowState.Maximized;

            //Updates resolution (updates to your screen's max resolution since its fullscreen)
            UpdateScreenResolution(System.Windows.SystemParameters.PrimaryScreenWidth, System.Windows.SystemParameters.PrimaryScreenHeight);
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            //Windows the game
            Application.Current.MainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            Application.Current.MainWindow.WindowState = WindowState.Normal;

            //Updates resolution (updates to current resolution after it windows)
            UpdateScreenResolution(Application.Current.MainWindow.Width, Application.Current.MainWindow.Height);
        }

        // Options Menu: Back Button
        private void OptionsMenu_BackButton_Click(object sender, RoutedEventArgs e)
        {
            //Hide Options Menu
            MainMenu_OptionsMenu.Visibility = Visibility.Hidden;

            //Show Main Menu Options and Highlights
            MainMenu_OptionsPanel.Visibility = Visibility.Visible;
            MainMenu_OptionsHighlight.Visibility = Visibility.Visible;
        }


        // Exit Button
        private void MainMenu_ExitGameButton_Click(object sender, RoutedEventArgs e)
        {
            //Hide Main Menu Options and Highlights
            MainMenu_OptionsPanel.Visibility = Visibility.Hidden;
            MainMenu_OptionsHighlight.Visibility = Visibility.Hidden;

            //Show Quit Prompt
            MainMenu_ExitPrompt.Visibility = Visibility.Visible;
        }
        private void ExitPrompt_YesButton_Click(object sender, RoutedEventArgs e)
        {
            //Quit Game
            System.Windows.Application.Current.Shutdown();
        }
        private void ExitPrompt_NoButton_Click(object sender, RoutedEventArgs e)
        {
            //Hide Exit Prompt
            MainMenu_ExitPrompt.Visibility = Visibility.Hidden;

            //Show Main Menu Options and Highlights
            MainMenu_OptionsPanel.Visibility = Visibility.Visible;
            MainMenu_OptionsHighlight.Visibility = Visibility.Visible;
        }


        // Call to update screen resolution
        private void UpdateScreenResolution(double ResolutionWidth, double ResolutionHeight)
        {
            //Changes resolution
            Application.Current.MainWindow.Width = ResolutionWidth;
            Application.Current.MainWindow.Height = ResolutionHeight;

            //Updates all resolution displays (text saying what resolution you are using)
            OptionsMenu_Resolution_DisplayText.Text = ResolutionHeight.ToString() + "x" + ResolutionWidth.ToString();
        }
    }
}
