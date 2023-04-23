using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYBERNUKE.MVVM.Model
{
    public class MainWeapon : Item
    {
        private int damage;
        private int spCost;

        public MainWeapon()
        {
            setName(null);
            setDamage(0);
            setDescription(null);
        }

        public MainWeapon(string name, int damage, int spCost, string description)
        {
            setName(name);
            setDamage(damage);
            setSPCost(spCost);
            setDescription(description);
        }

        public void setDamage(int damage)
        {
            this.damage = damage;
        }

        public int getDamage()
        {
            return this.damage;
        }

        public void setSPCost(int spCost)
        {
            this.spCost = spCost;
        }

        public int getSPCost()
        {
            return this.spCost;
        }
    }
}
