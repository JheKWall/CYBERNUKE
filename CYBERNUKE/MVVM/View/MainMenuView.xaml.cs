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


        public MainMenuView()
        {
            InitializeComponent();

            //Play Main Menu Music
            MainMenuMusicPlayer();
        }

        // Sound Manager
        // Music
        MediaPlayer MainMenu_BGM = new MediaPlayer(); //Background Music
        private void MainMenuMusicPlayer()
        {
            //Open the sound files
            MainMenu_BGM.Open(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\GameData\Sounds\Beetlemuse_Psychotic_And_Robotic.wav", UriKind.Absolute));

            //Loop sound files if they end
            MainMenu_BGM.MediaEnded += new EventHandler(Media_Ended);

            //Play sound 
            MainMenu_BGM.Play();
        }

        // Replays sound once it ends
        private void Media_Ended(object sender, EventArgs e)
        {
            //Sender is the MediaPlayer but its sent as object so cast it to Mediaplayer
            MediaPlayer mediaPlayer = (MediaPlayer)sender;
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }


        // Main Menu Options
        private void MainMenu_StartGameButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainMenu_LoadGameButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainMenu_OptionsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainMenu_ExitGameButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }




        // DEBUG: Resolution Change Buttons
        private void Resolution_768x1366_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Width = 1366;
            Application.Current.MainWindow.Height = 768;
        }

        private void Resolution_900x1600_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Width = 1600;
            Application.Current.MainWindow.Height = 900;
        }

        private void Resolution_1080x1920_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Width = 1920;
            Application.Current.MainWindow.Height = 1080;
        }
    }
}
