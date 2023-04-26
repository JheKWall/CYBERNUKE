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
        //Player Variables
        public int numPartyMembers;
        public List<Character> CharacterList = new List<Character>();

        //Player Status
        public bool TownToMap = false; //Used for identifying which spawn index to look for

        //Map Variables
        public string mapToLoad = "TranquilityCheckpoint";
        public string currentMap = "";
        public List<Map> mapList;
        private List<string> loadedMaps; //contains all loaded map names
        public int currentPosX = 0;
        public int currentPosY = 0;
        public bool returnToSavedPos = false;

        //Combat Variables
        public string enemyPartyName = "ZombieGroup";
        public bool isEncounter;

        //Town Variables
        public string townToLoad = "TranquilityTown";
        public string currentTown = "";

        //Item Variables
        public List<MainWeapon> WeaponList = new List<MainWeapon>();
        public List<MainArmor> ArmorList = new List<MainArmor>();

        //Enemy Variables
        public List<string> enemyPartyList = new List<string>(); //Enemy parties encountered randomly while walking

        //Cutscene Variables
        public string cutsceneToLoad = "Intro";
        public int menuToLoad = 0; //0 == main menu, 1 == overworld, 2 == town, 3 == combat

        public MainWindow()
        {
            InitializeComponent();

            // Initialize lists
            mapList = new List<Map>();
            loadedMaps = new List<string>();

            #region Enemy Party List Init
            enemyPartyList.Add("MutantGroup");
            enemyPartyList.Add("ZombieGroup");
            enemyPartyList.Add("ZombieHorde");
            enemyPartyList.Add("ZombieMutant");
            #endregion

            #region Main Weapon Init
            MainWeapon weaponOne = new MainWeapon("HOLOSWORD", 42, 15, "A hilt that projects a large 4 foot blade. It almost feels weightless.");
            MainWeapon weaponTwo = new MainWeapon("DAEMONBRAND", 60, 35, "A hefty blade infused with the souls of daemons.");
            MainWeapon weaponThree = new MainWeapon("CUTE PLUSHIE", 1, 1, "A stuffed animal that has seen better days.");
            MainWeapon weaponFour = new MainWeapon("NUKESTAFF", 999, 100, "A large metal rod with a mini-nuke at the end. Surely you're not thinking of using it, right?");
            MainWeapon weaponFive = new MainWeapon("PHOTONSABER", 40, 15, "Replace Photon with Light and you've got yourself a lawsuit.");
            MainWeapon weaponSix = new MainWeapon("LASGUN", 50, 25, "A big hefty metal gun that shoots lasers, battery pack not included.");
            MainWeapon weaponSeven = new MainWeapon("G.B.E.", 70, 50, "Gravitational Beam Emitter, otherwise known as the gun that will tear your hands off if you're not careful.");
            WeaponList.Add(weaponOne);
            WeaponList.Add(weaponTwo);
            WeaponList.Add(weaponThree);
            WeaponList.Add(weaponFour);
            WeaponList.Add(weaponFive);
            WeaponList.Add(weaponSix);
            WeaponList.Add(weaponSeven);
            #endregion

            #region Main Armor Init
            MainArmor armorOne = new MainArmor("ANDROID SUIT", 20, "The suit of those who are forgotten at birth and experimented on relentlessly.");
            MainArmor armorTwo = new MainArmor("MEDIEVAL ARMOR", 10, "In this day and age, metal plates won't get you far when you're up against energy weapons.");
            MainArmor armorThree = new MainArmor("CORPORATE ASSASSIN", 20, "Offers surprising protection, probably because nobody likes bureaucrats.");
            MainArmor armorFour = new MainArmor("SUN ARMOR", 15, "A suit of metal armor with a sun painted on the chestpiece. Praise the Sun!");
            MainArmor armorFive = new MainArmor("DEMONICA", 50, "Demountable Next Integrated Capability Armor, a high-tech demon summoning suit.");
            MainArmor armorSix = new MainArmor("POWER ARMOR", 30, "Surprisingly manueverable and highly effective, fusion cores not included.");
            MainArmor armorSeven = new MainArmor("NANOSUIT", 40, "A suit comprised of reactive nanobots capable of increasing armor thickness at points of impact.");
            ArmorList.Add(armorOne);
            ArmorList.Add(armorTwo);
            ArmorList.Add(armorThree);
            ArmorList.Add(armorFour);
            ArmorList.Add(armorFive);
            ArmorList.Add(armorSix);
            ArmorList.Add(armorSeven);
            #endregion

            #region Character Init
            Character charOne = new Character("PROTO", 175, 175, 5, 5, 5, 5, WeaponList[0], ArmorList[0]);
            Character charTwo = new Character("MAST", 200, 150, 7, 3, 6, 6, WeaponList[1], ArmorList[1]);
            Character charThree = new Character("KELPY", 140, 300, 4, 7, 4, 2, WeaponList[6], ArmorList[2]);
            CharacterList.Add(charOne);
            CharacterList.Add(charTwo);
            CharacterList.Add(charThree);
            #endregion

            numPartyMembers = 3;
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
