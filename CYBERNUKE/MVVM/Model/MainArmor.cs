using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYBERNUKE.MVVM.Model
{
    public class MainArmor : Item
    {

        private int defense;
        private string armorType;

        public MainArmor() {
            setName(null);
            setArmorType(null);
            setDefense(0);
            setItemValue(0);
            setDescription(null);
            setImage(null);
            setIsDiscardable(true);
        }

        public MainArmor(string name, string armorType, int defense, int value, string description, Image image, bool i)
        {
            setName(name);
            setArmorType(armorType);
            setDefense(defense);
            setItemValue(value);
            setDescription(description);
            setImage(image);
            setIsDiscardable(i);
        }

        public void setDefense(int defense)
        {
            this.defense = defense;
        }

        public int getDefense()
        {
            return this.defense;
        }

        public void setArmorType(string type)
        {
            this.armorType = type;
        }

        public string getArmorType()
        {
            return this.armorType;
        }
    }
}
