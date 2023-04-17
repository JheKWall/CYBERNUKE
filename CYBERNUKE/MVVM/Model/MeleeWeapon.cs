using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYBERNUKE.MVVM.Model
{
    public class MeleeWeapon : Item
    {
        private int damage;
        private string damageType;

        public MeleeWeapon() {
            setName(null);
            setDamagetype(null);
            setDamage(0);
            setItemValue(0);
            setDescription(null);
            setImage(null);
            setIsDiscardable(true);
        }

        public MeleeWeapon(string name, string damageType, int damage, int value, string description, Image image, bool i)
        {
            setName(name);
            setDamagetype(damageType);
            setDamage(damage);
            setItemValue(value);
            setDescription(description);
            setImage(image);
            setIsDiscardable(i);
        }

        public void setDamage(int damage)
        {
            this.damage = damage;
        }

        public int getDamage()
        {
            return this.damage;
        }

        public void setDamagetype(string type)
        {
            this.damageType = type;
        }

        public string getDamagetype()
        {
            return this.damageType;
        }
    }
}
