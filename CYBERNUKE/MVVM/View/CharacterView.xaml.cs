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
using CYBERNUKE.Core;

namespace CYBERNUKE.MVVM.View
{
    /// <summary>
    /// Interaction logic for CharacterView.xaml
    /// </summary>
    public partial class CharacterView : UserControl
    {
        List<Character> characterList = new List<Character>();
        List<Item> equipmentList = new List<Item>();
        List<Item> consumableList = new List<Item>();

        Character currentCharacter = null;

        // On load, set the character list to the list of characters in the main window
        // and set the equipment and consumable lists to the lists in the main window
        // Then, hide the buttons for characters that don't exist to prevent errors and crashes.
        public CharacterView()
        {
            InitializeComponent();

            characterList = ((MainWindow)Application.Current.MainWindow).CharacterList;

            equipmentList = ((MainWindow)Application.Current.MainWindow).EquipmentList;

            consumableList = ((MainWindow)Application.Current.MainWindow).ConsumableList;

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

        // On click, inserts data from first character into appropriate textboxes.
        private void Character0_Click(object sender, RoutedEventArgs e)
        {
            EquipmentStackPanel.Children.Clear(); // Clear equipment change children
            EquipmentStackPanel.Visibility = Visibility.Collapsed; // Collapses stack panel

            charName.Text = "Name: " + characterList[0].getName(); // Name: <Enter Name Here>

            HPBar.Maximum = characterList[0].getMaxHP();
            HPBar.Value = characterList[0].getCurrentHP();

            SPBar.Maximum = characterList[0].getMaxSP();
            SPBar.Value = characterList[0].getCurrentSP();

            StrengthTextBox.Text = "Strength: " + characterList[0].getStatStrength();
            DexterityTextBox.Text = "Dexterity: " + characterList[0].getStatDexterity();
            EnduranceTextBox.Text = "Endurance " + characterList[0].getStatEndurance();
            IntelligenceTextBox.Text = "Intelligence: " + characterList[0].getStatIntelligence();

            OutfitTextBox.Text = "Outfit: ";
            if (characterList[0].getEquippedOutfit() != null)
            {
                OutfitTextBox.Text += characterList[0].getEquippedOutfit().getName();
            }
            else
            {
                OutfitTextBox.Text += "None";
            }

            MeleeWeaponTextBox.Text = "Melee Weapon: ";
            if (characterList[0].getEquippedMeleeWeapon() != null)
            {
                MeleeWeaponTextBox.Text += characterList[0].getEquippedMeleeWeapon().getName();
            }
            else
            {
                MeleeWeaponTextBox.Text += "None";
            }

            RangedWeaponTextBox.Text = "Ranged Weapon: ";
            if (characterList[0].getEquippedRangedWeapon() != null)
            {
                RangedWeaponTextBox.Text += characterList[0].getEquippedRangedWeapon().getName();
            }
            else
            {
                RangedWeaponTextBox.Text += "None";
            }
            

            currentCharacter = characterList[0];
        }

        // On click, inserts data from second character into appropriate textboxes.
        private void Character1_Click(object sender, RoutedEventArgs e)
        {
            EquipmentStackPanel.Children.Clear(); // Clear equipment change children
            EquipmentStackPanel.Visibility = Visibility.Collapsed; // Collapses stack panel

            charName.Text = "Name: " + characterList[1].getName(); // Name: <Enter Name Here>

            HPBar.Maximum = characterList[1].getMaxHP();
            HPBar.Value = characterList[1].getCurrentHP();

            SPBar.Maximum = characterList[1].getMaxSP();
            SPBar.Value = characterList[1].getCurrentSP();

            StrengthTextBox.Text = "Strength: " + characterList[1].getStatStrength();
            DexterityTextBox.Text = "Dexterity: " + characterList[1].getStatDexterity();
            EnduranceTextBox.Text = "Endurance " + characterList[1].getStatEndurance();
            IntelligenceTextBox.Text = "Intelligence: " + characterList[1].getStatIntelligence();

            OutfitTextBox.Text = "Outfit: ";
            if (characterList[1].getEquippedOutfit() != null)
            {
                OutfitTextBox.Text += characterList[1].getEquippedOutfit().getName();
            }
            else
            {
                OutfitTextBox.Text += "None";
            }

            MeleeWeaponTextBox.Text = "Melee Weapon: ";
            if (characterList[1].getEquippedMeleeWeapon() != null)
            {
                MeleeWeaponTextBox.Text += characterList[1].getEquippedMeleeWeapon().getName();
            }
            else
            {
                MeleeWeaponTextBox.Text += "None";
            }

            RangedWeaponTextBox.Text = "Ranged Weapon: ";
            if (characterList[1].getEquippedRangedWeapon() != null)
            {
                RangedWeaponTextBox.Text += characterList[1].getEquippedRangedWeapon().getName();
            }
            else
            {
                RangedWeaponTextBox.Text += "None";
            }

            currentCharacter = characterList[1];
        }

        // On click, inserts data from third character into appropriate textboxes
        private void Character2_Click(object sender, RoutedEventArgs e)
        {
            EquipmentStackPanel.Children.Clear(); // Clear equipment change children
            EquipmentStackPanel.Visibility = Visibility.Collapsed; // Collapses stack panel

            charName.Text = "Name: " + characterList[2].getName(); // Name: <Enter Name Here>

            HPBar.Maximum = characterList[2].getMaxHP();
            HPBar.Value = characterList[2].getCurrentHP();

            SPBar.Maximum = characterList[2].getMaxSP();
            SPBar.Value = characterList[2].getCurrentSP();

            StrengthTextBox.Text = "Strength: " + characterList[2].getStatStrength();
            DexterityTextBox.Text = "Dexterity: " + characterList[2].getStatDexterity();
            EnduranceTextBox.Text = "Endurance " + characterList[2].getStatEndurance();
            IntelligenceTextBox.Text = "Intelligence: " + characterList[2].getStatIntelligence();

            OutfitTextBox.Text = "Outfit: ";
            if (characterList[2].getEquippedOutfit() != null)
            {
                OutfitTextBox.Text += characterList[2].getEquippedOutfit().getName();
            }
            else
            {
                OutfitTextBox.Text += "None";
            }

            MeleeWeaponTextBox.Text = "Melee Weapon: ";
            if (characterList[2].getEquippedMeleeWeapon() != null)
            {
                MeleeWeaponTextBox.Text += characterList[2].getEquippedMeleeWeapon().getName();
            }
            else
            {
                MeleeWeaponTextBox.Text += "None";
            }

            RangedWeaponTextBox.Text = "Ranged Weapon: ";
            if (characterList[2].getEquippedRangedWeapon() != null)
            {
                RangedWeaponTextBox.Text += characterList[2].getEquippedRangedWeapon().getName();
            }
            else
            {
                RangedWeaponTextBox.Text += "None";
            }

            currentCharacter = characterList[2];
        }

        // On click, inserts data from fourth character into appropriate textboxes.
        private void Character3_Click(object sender, RoutedEventArgs e)
        {
            EquipmentStackPanel.Children.Clear(); // Clear equipment change children
            EquipmentStackPanel.Visibility = Visibility.Collapsed; // Collapses stack panel

            charName.Text = "Name: " + characterList[3].getName(); // Name: <Enter Name Here>

            HPBar.Maximum = characterList[3].getMaxHP();
            HPBar.Value = characterList[3].getCurrentHP();

            SPBar.Maximum = characterList[3].getMaxSP();
            SPBar.Value = characterList[3].getCurrentSP();

            StrengthTextBox.Text = "Strength: " + characterList[3].getStatStrength();
            DexterityTextBox.Text = "Dexterity: " + characterList[3].getStatDexterity();
            EnduranceTextBox.Text = "Endurance " + characterList[3].getStatEndurance();
            IntelligenceTextBox.Text = "Intelligence: " + characterList[3].getStatIntelligence();

            OutfitTextBox.Text = "Outfit: ";
            if (characterList[3].getEquippedOutfit() != null)
            {
                OutfitTextBox.Text += characterList[0].getEquippedOutfit().getName();
            }
            else
            {
                OutfitTextBox.Text += "None";
            }

            MeleeWeaponTextBox.Text = "Melee Weapon: ";
            if (characterList[3].getEquippedMeleeWeapon() != null)
            {
                MeleeWeaponTextBox.Text += characterList[3].getEquippedMeleeWeapon().getName();
            }
            else
            {
                MeleeWeaponTextBox.Text += "None";
            }

            RangedWeaponTextBox.Text = "Ranged Weapon: ";
            if (characterList[3].getEquippedRangedWeapon() != null)
            {
                RangedWeaponTextBox.Text += characterList[3].getEquippedRangedWeapon().getName();
            }
            else
            {
                RangedWeaponTextBox.Text += "None";
            }

            currentCharacter = characterList[3];
        }

        // Have to have this or else build fails. Because the HP bar won't change while this View is open, don't need to code it.
        private void HPBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        // Have to have this or else build fails. Because the SP bar won't change while this View is open, don't need to code it.
        private void SPBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        // Have to have this or else build fails. Because the EXP bar won't change while this View is open, don't need to code it.
        private void ExpBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        // On click, opens a side "panel" where available equipment options are available in the form of buttons for the player to click and equip.
        private void OutfitChangeButton_Click(object sender, RoutedEventArgs e)
        {
            EquipmentStackPanel.Children.Clear(); // clears any previous buttons. Prevents infinite buttons from being generated and clears the space
            EquipmentStackPanel.Visibility = Visibility.Visible;
            
            // Loop through each appropriate item and dynamically add equipment-switch buttons to EquipmentStackPanel
            foreach (Item i in equipmentList) 
            {
                if(i is MainArmor)
                {
                    CreateButton((MainArmor)i, ChangeSelectedOutfit);
                }
            }
        }

        // On click, opens a side "panel" where available equipment options are available in the form of buttons for the player to click and equip.
        private void MeleeWeaponChangeButton_Click(object sender, RoutedEventArgs e)
        {
            EquipmentStackPanel.Children.Clear(); // clears any previous buttons. Prevents infinite buttons from being generated and clears the space
            EquipmentStackPanel.Visibility = Visibility.Visible;

            // Loop through each appropriate item and dynamically add equipment-switch buttons to EquipmentStackPanel
            foreach (Item i in equipmentList)
            {
                if (i is MeleeWeapon)
                {
                    CreateButton((MeleeWeapon)i, ChangeSelectedMeleeWeapon);
                }
            }
        }

        // On click, opens a side "panel" where available equipment options are available in the form of buttons for the player to click and equip.
        private void RangedWeaponChangeButton_Click(object sender, RoutedEventArgs e)
        {
            EquipmentStackPanel.Children.Clear(); // clears any previous buttons. Prevents infinite buttons from being generated and clears the space
            EquipmentStackPanel.Visibility = Visibility.Visible;

            // Loop through each appropriate item and dynamically add equipment-switch buttons to EquipmentStackPanel
            foreach (Item i in equipmentList)
            {
                if (i is RangedWeapon)
                {
                    CreateButton((RangedWeapon)i, ChangeSelectedRangedWeapon);
                }
            }
        }

        // Used to populate EquipmentStackPanel with dynamically-generated buttons, 
        // and the button commands depend on the argued function.
        // Each xChangeButton_Click button will do a different argued function
        private void CreateButton(Item item, Action<Item> buttonAction)
        {
            Button button = new Button();
            button.Content = item.getName();
            button.Command = new RelayCommand(param => buttonAction(item), o => true);
            EquipmentStackPanel.Children.Add(button);
        }

        // Sets current character's equipped outfit to the passed item
        private void ChangeSelectedOutfit(Item item)
        {
            currentCharacter.setEquippedOutfit((MainArmor)item);
        }

        // Sets current character's equipped Melee weapon to the passed item
        private void ChangeSelectedMeleeWeapon(Item item)
        {
            currentCharacter.setEquippedMeleeWeapon((MeleeWeapon)item);

        }

        // Sets current character's equipped ranged weapon to the passed item
        private void ChangeSelectedRangedWeapon(Item item)
        {
            currentCharacter.setEquippedRangedWeapon((RangedWeapon)item);
        }
    }
}
