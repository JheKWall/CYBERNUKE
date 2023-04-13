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
using System.Windows.Navigation;
using CYBERNUKE.MVVM.Model;

namespace CYBERNUKE.MVVM.View
{
    /// <summary>
    /// Interaction logic for CharacterView.xaml
    /// </summary>
    public partial class CharacterView : UserControl
    {
        List<Character> characterList = new List<Character>();
        public CharacterView()
        {
            InitializeComponent();

            List<Character> CharacterList = new List<Character>();
            characterList = ((MainWindow)Application.Current.MainWindow).CharacterList;

            Character0.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            if (characterList.Count == 1)
            {
                Character1.Visibility = Visibility.Hidden;
                Character2.Visibility = Visibility.Hidden;
                Character3.Visibility = Visibility.Hidden;
            }
            else if (characterList.Count == 2)
            {
                Character2.Visibility = Visibility.Hidden;
                Character3.Visibility = Visibility.Hidden;
            }
            else if (characterList.Count == 3)
            {
                Character3.Visibility = Visibility.Hidden;
            }
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

            OutfitTextBox.Text = "Outfit: " + characterList[0].getEquippedOutfit();
            MeleeWeaponTextBox.Text = "Melee Weapon: " + characterList[0].getEquippedMeleeWeapon();
            RangedWeaponTextBox.Text = "Ranged Weapon: " + characterList[0].getEquippedRangedWeapon();
        }

        private void Character1_Click(object sender, RoutedEventArgs e)
        {
            charName.Text = "Name: " + characterList[1].getName(); // Name: <Enter Name Here>

            HPBar.Maximum = characterList[1].getMaxHP();
            HPBar.Value = characterList[1].getCurrentHP();

            SPBar.Maximum = characterList[1].getMaxSP();
            SPBar.Value = characterList[1].getCurrentSP();

            StrengthTextBox.Text = "Strength: " + characterList[1].getStatStrength();
            DexterityTextBox.Text = "Dexterity: " + characterList[1].getStatDexterity();
            EnduranceTextBox.Text = "Endurance " + characterList[1].getStatEndurance();
            IntelligenceTextBox.Text = "Intelligence: " + characterList[1].getStatIntelligence();

            OutfitTextBox.Text = "Outfit: " + characterList[1].getEquippedOutfit();
            MeleeWeaponTextBox.Text = "Melee Weapon: " + characterList[1].getEquippedMeleeWeapon();
            RangedWeaponTextBox.Text = "Ranged Weapon: " + characterList[1].getEquippedRangedWeapon();
        }

        private void Character2_Click(object sender, RoutedEventArgs e)
        {
            charName.Text = "Name: " + characterList[2].getName(); // Name: <Enter Name Here>

            HPBar.Maximum = characterList[2].getMaxHP();
            HPBar.Value = characterList[2].getCurrentHP();

            SPBar.Maximum = characterList[2].getMaxSP();
            SPBar.Value = characterList[2].getCurrentSP();

            StrengthTextBox.Text = "Strength: " + characterList[2].getStatStrength();
            DexterityTextBox.Text = "Dexterity: " + characterList[2].getStatDexterity();
            EnduranceTextBox.Text = "Endurance " + characterList[2].getStatEndurance();
            IntelligenceTextBox.Text = "Intelligence: " + characterList[2].getStatIntelligence();

            OutfitTextBox.Text = "Outfit: " + characterList[2].getEquippedOutfit();
            MeleeWeaponTextBox.Text = "Melee Weapon: " + characterList[2].getEquippedMeleeWeapon();
            RangedWeaponTextBox.Text = "Ranged Weapon: " + characterList[2].getEquippedRangedWeapon();
        }

        private void Character3_Click(object sender, RoutedEventArgs e)
        {
            charName.Text = "Name: " + characterList[3].getName(); // Name: <Enter Name Here>

            HPBar.Maximum = characterList[3].getMaxHP();
            HPBar.Value = characterList[3].getCurrentHP();

            SPBar.Maximum = characterList[3].getMaxSP();
            SPBar.Value = characterList[3].getCurrentSP();

            StrengthTextBox.Text = "Strength: " + characterList[3].getStatStrength();
            DexterityTextBox.Text = "Dexterity: " + characterList[3].getStatDexterity();
            EnduranceTextBox.Text = "Endurance " + characterList[3].getStatEndurance();
            IntelligenceTextBox.Text = "Intelligence: " + characterList[3].getStatIntelligence();

            OutfitTextBox.Text = "Outfit: " + characterList[3].getEquippedOutfit();
            MeleeWeaponTextBox.Text = "Melee Weapon: " + characterList[3].getEquippedMeleeWeapon();
            RangedWeaponTextBox.Text = "Ranged Weapon: " + characterList[3].getEquippedRangedWeapon();
        }

        private void HPBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void SPBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void ExpBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
