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

        public MainArmor() {
            setName(null);
            setDefense(0);
            setDescription(null);
        }

        public MainArmor(string name, int defense, string description)
        {
            setName(name);
            setDefense(defense);
            setDescription(description);
        }

        public void setDefense(int defense)
        {
            this.defense = defense;
        }

        public int getDefense()
        {
            return this.defense;
        }
    }
}
