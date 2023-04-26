using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYBERNUKE.MVVM.Model
{
    public class Item
    {
        private string itemName;
        private string itemDescription;

        public Item()
        {
            itemName = null;
            itemDescription = null;   
        }

        public Item(string itemName, string itemDescription)
        {
            this.itemName = itemName;
            this.itemDescription = itemDescription;
        }

        public void setName(string name)
        {
            this.itemName = name;
        }

        public string getName()
        {
            return this.itemName;
        }

        public void setDescription(string description)
        {
            this.itemDescription = description;
        }

        public string getDescription()
        {
            return this.itemDescription;
        }
    }
}
