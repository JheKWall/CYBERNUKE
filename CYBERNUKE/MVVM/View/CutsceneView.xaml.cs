using CYBERNUKE.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for CutsceneView.xaml
    /// </summary>
    public partial class CutsceneView : UserControl
    {
        //Streamreader
        private StreamReader input;

        //Vars
        string cutsceneName;
        string cutsceneText;

        public CutsceneView()
        {
            InitializeComponent();

            cutsceneName = ((MainWindow)Application.Current.MainWindow).cutsceneToLoad;

            input = new StreamReader("GameData/Dialogue/Cutscene_" + cutsceneName + ".txt");

            Cutscene_Text.Text = input.ReadLine();
        }

        private void Cutscene_FullScreenClick_Click(object sender, RoutedEventArgs e)
        {
            int nextMenu = ((MainWindow)Application.Current.MainWindow).menuToLoad;

            //0 == main menu, 1 == overworld, 2 == town, 3 == combat
            var viewModel = (CutsceneViewModel)DataContext;
            switch (nextMenu)
            {
                case 0:
                    if (viewModel.NavigateMainMenuViewCommand.CanExecute(null))
                    {
                        viewModel.NavigateMainMenuViewCommand.Execute(null);
                    }
                    break;

                case 1:
                    if (viewModel.NavigateOverworldViewCommand.CanExecute(null))
                    {
                        viewModel.NavigateOverworldViewCommand.Execute(null);
                    }
                    break;

                case 2:
                    if (viewModel.NavigateTownViewCommand.CanExecute(null))
                    {
                        viewModel.NavigateTownViewCommand.Execute(null);
                    }
                    break;

                case 3:
                    if (viewModel.NavigateCombatViewCommand.CanExecute(null))
                    {
                        viewModel.NavigateCombatViewCommand.Execute(null);
                    }
                    break;
            }
        }
    }
}
