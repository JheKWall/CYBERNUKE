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

        public MainWindow()
        {
            InitializeComponent();

            Character mainCharacter = new Character(); // test character for character view. Will remove later.
            mainCharacter.setName("Test Character");
            mainCharacter.setMaxHP(500);
            mainCharacter.setMaxSP(100);
            CharacterList.Add(mainCharacter);

            MeleeWeapon testWeapon = new MeleeWeapon();
            testWeapon.setName("Destroyer of Worlds");
            testWeapon.setDamage(700);

            MeleeWeapon testWeapon1 = new MeleeWeapon();
            testWeapon1.setName("Umbrella");
            testWeapon1.setDamage(3);

            EquipmentList.Add(testWeapon);
            EquipmentList.Add(testWeapon1);

        }
    }
}
