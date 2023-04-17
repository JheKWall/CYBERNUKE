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
        //public int Id { get; set; }

        private string itemName;
        private int itemValue;
        private string itemDescription;
        private Image itemIcon;

        //private List<Tag> itemTags;

        private bool isItemDiscardable;

        public Item()
        {
            itemName = null;
            itemValue = 0;
            itemDescription = null;
            itemIcon = null;
            isItemDiscardable = true;            
        }

        public Item(string itemName, int itemValue, string itemDescription, Image itemIcon, bool isItemDiscardable)
        {
            this.itemName = itemName;
            this.itemValue = itemValue;
            this.itemDescription = itemDescription;
            this.itemIcon = itemIcon;
            this.isItemDiscardable = isItemDiscardable;
        }

        public void setName(string name)
        {
            this.itemName = name;
        }

        public string getName()
        {
            return this.itemName;
        }

        public void setItemValue(int value)
        {
            this.itemValue = value;
        }

        public int getItemValue()
        {
            return this.itemValue;
        }

        public void setDescription(string description)
        {
            this.itemDescription = description;
        }

        public string getDescription()
        {
            return this.itemDescription;
        }

        public void setImage(Image image)
        {
            this.itemIcon = image;
        }

        public Image getImage()
        {
            return this.itemIcon;
        }

        public void setIsDiscardable(bool i)
        {
            this.isItemDiscardable = i;
        }

        public bool getIsDiscardable()
        {
            return isItemDiscardable;
        }
    }
}
