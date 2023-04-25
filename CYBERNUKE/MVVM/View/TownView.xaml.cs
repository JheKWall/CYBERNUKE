using CYBERNUKE.Core;
using CYBERNUKE.MVVM.Model;
using CYBERNUKE.MVVM.ViewModel;
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
    /// Interaction logic for TownView.xaml
    /// </summary>
    public partial class TownView : UserControl
    {
        public TownView()
        {
            InitializeComponent();
        }

        private void ButtonLeave_Click(object sender, RoutedEventArgs e)
        {
            //Return to overworld view
            var viewModel = (TownViewModel)DataContext;
            if (viewModel.NavigateOverworldViewCommand.CanExecute(null))
            {
                viewModel.NavigateOverworldViewCommand.Execute(null);
            }
        }

        private void ButtonNPC1_Click(object sender, RoutedEventArgs e)
        {


        }

        private void ButtonNPC2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonRest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DialogueContinue_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
