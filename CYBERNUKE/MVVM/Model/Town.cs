using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CYBERNUKE.MVVM.Model
{
    public class Town
    {
        private string _name = null;
        private string _description = null;
        List<Button> townButtons = new List<Button>();

        public Town(string name, string description)
        {
            _name = name;
            _description = description;
            Button b = new Button();
            //b.Command =
            //townButtons.Add();
        }




    }
}
