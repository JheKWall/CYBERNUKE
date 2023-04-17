using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents.DocumentStructures;

namespace CYBERNUKE.MVVM.Model
{
    public class Skill
    {
        private string name;
        private int SPCost;
        private int damage;
        private string typeDamage;
        private string description;

        public Skill(string fileName)
        {
            // read in all info from a designated text file
            string relativePath = "Resources/"; // relative path for resources (where Skill Text Files will be)

            // if the file exists, copies text line-by-line to string array and then starts splitting into appropriate variables
            if (File.Exists(relativePath + fileName)) {
                int numLines = File.ReadLines(relativePath + fileName).Count();
                string[] lines = File.ReadAllLines(relativePath + fileName);
                for(int i = 0; i < numLines; i++)
                {
                    // hacky way to filter strings for each variable. Super terrible
                    // if file structure for skill changes, this needs to be edited
                    switch (i)
                    {
                        // Name of Skill
                        case 0:
                            name = lines[i].Substring(6);
                            break;

                        // SP Cost of Skill
                        case 1:
                            SPCost = Int32.Parse(lines[i].Substring(8));
                            break;

                        // Damage Value of Skill
                        case 2:
                            damage = Int32.Parse(lines[i].Substring(8));
                            break;

                        // Type of Damage of the Skill
                        case 3:
                            typeDamage = lines[i].Substring(12);
                            break;

                        // Description of the Skill
                        case 4:
                            description = lines[i].Substring(13);
                            break;
                    }
                }

            }
            
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setSPCost(int SPCost)
        {
            this.SPCost = SPCost;
        }

        public void setDamage(int Damage)
        {
            this.damage = Damage;
        }

        public void setTypeDamage(string TypeDamage)
        {
            this.typeDamage = TypeDamage;
        }

        public void setDescription(string Description)
        {
            this.description = Description;
        }

        public string getName()
        {
            return this.name;
        }

        public int getSPCost()
        {
            return this.SPCost;
        }

        public int getDamage()
        {
            return this.damage;
        }

        public string getTypeDamage()
        {
            return this.typeDamage;
        }

        public string getDescription()
        {
            return this.description;
        }
    }
}
