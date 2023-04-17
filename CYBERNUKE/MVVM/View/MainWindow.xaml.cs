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
using CYBERNUKE.MVVM.Model;

namespace CYBERNUKE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Character> CharacterList = new List<Character>();
        public List<Item> EquipmentList = new List<Item>();
        public List<Item> ConsumableList = new List<Item>();

        public string enemyPartyName = "LesserZombieHorde";

        public MainWindow()
        {
            InitializeComponent();

            Character mainCharacter = new Character(); // test character for character view. Will remove later.
            mainCharacter.setName("Test Character");
            mainCharacter.setMaxHP(500);
            mainCharacter.setMaxSP(100);
            CharacterList.Add(mainCharacter);

            // The rest of this stuff is for testing

            MainArmor testArmor = new MainArmor();
            testArmor.setName("Test Armor");
            testArmor.setDefense(100);

            MainArmor testArmor1 = new MainArmor();
            testArmor1.setName("Test Armor 2");
            testArmor1.setDefense(200);

            MeleeWeapon testWeapon = new MeleeWeapon();
            testWeapon.setName("Destroyer of Worlds");
            testWeapon.setDamage(700);

            MeleeWeapon testWeapon1 = new MeleeWeapon();
            testWeapon1.setName("Umbrella");
            testWeapon1.setDamage(3);

            RangedWeapon testRangedWeapon = new RangedWeapon();
            testRangedWeapon.setName("Test Ranged Weapon");
            testRangedWeapon.setDamage(100);

            RangedWeapon testRangedWeapon1 = new RangedWeapon();
            testRangedWeapon1.setName("Test Ranged Weapon 2");
            testRangedWeapon1.setDamage(200);

            EquipmentList.Add(testWeapon);
            EquipmentList.Add(testWeapon1);
            EquipmentList.Add(testArmor);
            EquipmentList.Add(testArmor1);
            EquipmentList.Add(testRangedWeapon);
            EquipmentList.Add(testRangedWeapon1);

        }
    }
}
