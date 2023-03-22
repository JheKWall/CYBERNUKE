using CYBERNUKE.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CYBERNUKE.Core;

namespace CYBERNUKE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        // Public method for changing screen resolution, used by options
        public void ChangeResolution(double screenWidth, double screenHeight)
        {
            Application.Current.MainWindow.Width = screenWidth;
            Application.Current.MainWindow.Height = screenHeight;
        }
    }
}
