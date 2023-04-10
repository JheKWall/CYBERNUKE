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
using System.Windows.Shapes;
using CYBERNUKE.MVVM.View;
using CYBERNUKE.MVVM.Model;

namespace CYBERNUKE.MVVM.View
{
    /// <summary>
    /// Interaction logic for CharacterView.xaml
    /// </summary>
    public partial class CharacterView : Window
    {
        List<Character> characterList = new List<Character>();
        public CharacterView()
        {
            InitializeComponent();
            Loaded += (s, e) =>
            {
                Character0.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            };

            List<Character> CharacterList = new List<Character>();
            CharacterList = ((MainWindow)Application.Current.MainWindow).CharacterList;
        }

        private void Character0_Click(object sender, RoutedEventArgs e)
        {
            charName.Text = "Name: " + characterList[0].getName(); // Name: <Enter Name Here>

            HPBar.Maximum = characterList[0].getMaxHP();
            HPBar.Value = characterList[0].getCurrentHP();

            SPBar.Maximum = characterList[0].getMaxSP();
            SPBar.Value = characterList[0].getCurrentSP();

            StrengthTextBox.Text = "Strength: " + characterList[0].getStatStrength();
            DexterityTextBox.Text = "Dexterity: " + characterList[0].getStatDexterity();
            EnduranceTextBox.Text = "Endurance " + characterList[0].getStatEndurance();
            IntelligenceTextBox.Text = "Intelligence: " + characterList[0].getStatIntelligence();

            //OutfitTextBox.Text = "Outfit: " + characterList[0].getOutfit();
            //MeleeWeaponTextBox.Text = "Melee Weapon: " + characterList[0].getEquippedMeleeWeapon();
            //RangedWeaponTextBox.Text = "Ranged Weapon: " + characterList[0].getEquippedRangedWeapon();
        }

        private void Character1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Character2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Character3_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
