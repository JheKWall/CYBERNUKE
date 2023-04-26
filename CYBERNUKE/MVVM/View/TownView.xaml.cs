using CYBERNUKE.Core;
using CYBERNUKE.GameData.UserControls;
using CYBERNUKE.MVVM.Model;
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
    /// Interaction logic for TownView.xaml
    /// </summary>
    public partial class TownView : UserControl
    {
        //Streamreader
        private StreamReader input;

        //Prompt Vars
        int numPromptRuns; //How many times the prompt needs to run
        int promptIndex; //Current run index

        //Dialogue Vars
        string[] dialogueArray;

        //Player Vars
        bool hasControl;

        public TownView()
        {
            InitializeComponent();
            ScaleText();

            //Set new current town
            ((MainWindow)Application.Current.MainWindow).currentTown = "TranquilityTown";

            hasControl = true;
        }

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
                    Dialogue_Text.FontSize = 32;
                    DialogueContinue.FontSize = 22;
                    break;
                case 1: //1600
                    Dialogue_Text.FontSize = 35;
                    DialogueContinue.FontSize = 25;
                    break;
                case 2: //1920
                    Dialogue_Text.FontSize = 45;
                    DialogueContinue.FontSize = 35;
                    break;
            }
        }

        private void ButtonLeave_Click(object sender, RoutedEventArgs e)
        {
            //Set TownToMap true
            ((MainWindow)Application.Current.MainWindow).TownToMap = true;

            //Return to overworld view
            var viewModel = (TownViewModel)DataContext;
            if (viewModel.NavigateOverworldViewCommand.CanExecute(null))
            {
                viewModel.NavigateOverworldViewCommand.Execute(null);
            }
        }

        private void ButtonNPC1_Click(object sender, RoutedEventArgs e)
        {
            Init_Dialogue("OldMan_Tranquility");
        }

        private void ButtonNPC2_Click(object sender, RoutedEventArgs e)
        {
            Init_Dialogue("Guard_Tranquility");
        }

        private void DialogueContinue_Click(object sender, RoutedEventArgs e)
        {
            // Increment Prompt Index
            promptIndex++;

            // Hide button
            DialogueContinue.Visibility = Visibility.Hidden;

            // Call Display Dialogue again
            Display_Dialogue();
        }

        #region DialoguePromopt
        //Public & private method for displaying popups
        public void Init_Dialogue(string dialogueName) //Call when starting prompt
        {
            //Weird bug sometimes where spamming 'Interact' at a teleport spot opens combat and teleport prompt which messes up index and crashes
            PopUpContainer.Visibility = Visibility.Hidden;

            #region Get Dialogue
            input = new StreamReader("GameData/Dialogue/" + dialogueName + ".txt");
            numPromptRuns = Int32.Parse(input.ReadLine());
            dialogueArray = new string[numPromptRuns];
            for (int i = 0; i < numPromptRuns; i++)
            {
                dialogueArray[i] = input.ReadLine();
            }
            #endregion

            // Close Streamreader
            input.Close();

            // Display Dialogue
            promptIndex = 0;
            Display_Dialogue();
        }
        private void Display_Dialogue()
        {
            if (promptIndex < numPromptRuns) //If within runs
            {
                //0. Remove Player Control
                hasControl = false;

                //1. Show dialogue box
                PopUpContainer.Visibility = Visibility.Visible;
                DialogueContainer.Visibility = Visibility.Visible;

                //2. Show dialogue in dialogueArray at promptIndex
                Dialogue_Text.Text = dialogueArray[promptIndex];

                //3. Show Continue Button
                DialogueContinue.Visibility = Visibility.Visible;
            }
            else //If exiting prompt run
            {
                //4. Re-hide dialogue box
                PopUpContainer.Visibility = Visibility.Hidden;
                DialogueContainer.Visibility = Visibility.Hidden;

                //6. Give player control back :)
                hasControl = true;
            }
        }
        #endregion
    }
}
