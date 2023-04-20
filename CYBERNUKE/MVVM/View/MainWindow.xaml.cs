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
        public List<Item> EquipmentList = new List<Item>();
        public List<Item> ConsumableList = new List<Item>();

        //Player Variables
        public int numPartyMembers;
        public List<Character> CharacterList = new List<Character>();

        //Map Variables
        public string mapToLoad = "TestMap";
        public string currentMap = "";
        public List<Map> mapList;
        private List<string> loadedMaps; //contains all loaded map names

        //Combat Variables
        public string enemyPartyName = "LesserZombieHorde";

        //Town Variables
        public string townToLoad = "Tranquility";
        public string currentTown = "";

        public MainWindow()
        {
            InitializeComponent();

            // Initialize lists
            mapList = new List<Map>();
            loadedMaps = new List<string>();


            Character mainCharacter = new Character(); // test character for character view. Will remove later.
            mainCharacter.setName("PROTO");
            mainCharacter.setMaxHP(222);
            mainCharacter.setCurrentHP(222);
            mainCharacter.setMaxSP(111);
            mainCharacter.setCurrentSP(111);
            CharacterList.Add(mainCharacter);

            numPartyMembers = 1;

            /* The rest of this stuff is for testing
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
            EquipmentList.Add(testRangedWeapon1);*/
        }

        //Public method for creating and accessing maps in the mapList
        public int Get_Map()
        {
            int index;

            //Checks array for maps
            bool has = loadedMaps.Contains(mapToLoad);

            if (!has) //If not in list
            {
                //Add map to loaded list, create new map
                loadedMaps.Add(mapToLoad);
                mapList.Add(new Map());
                index = loadedMaps.IndexOf(mapToLoad);
            }
            else //If it is in the list
            {
                index = loadedMaps.IndexOf(mapToLoad);
            }

            return index;
        }
    }
}
