using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYBERNUKE.MVVM.Model
{
    public class RangedWeapon : Item
    {
        public RangedWeapon() {
            setName(null);
            setItemValue(0);
            setDescription(null);
            setImage(null);
            setIsDiscardable(true);
        }

        public RangedWeapon(string name, int value, string description, Image image, bool i)
        {
            setName(name);
            setItemValue(value);
            setDescription(description);
            setImage(image);
            setIsDiscardable(i);
        }
    }
}
