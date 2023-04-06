using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYBERNUKE.MVVM.Model
{
    public abstract class Character
    {
        private string name;
        private int level;
        private bool isUnconscious;
        private int maxHP;
        private int currentHP;
        private int maxSP;
        private int currentSP;
        private int defense;
        private List<Skill> skillList;
        private int statStrength;
        private int statDexterity;
        private int statEndurance;
        private int statIntelligence;
        private double resSlash;
        private double resPierce;
        private double resBlunt;
        private double resFire;
        private double resWater;
        private double resIce;
        private double resElectric;
        private double resEarth;
        private double resWind;

        public void setName(string name)
        {
            this.name = name;
        }

        public void setLevel(int level) { 
            this.level = level;
        }

        public void setIsUnconscious(bool isUnconscious)
        {
            this.isUnconscious = isUnconscious;
        }

        public void setMaxHP(int maxHP)
        {
            this.maxHP = maxHP;
        }

        public void setCurrentHP(int currentHP)
        {
            this.currentHP = currentHP;
        }

        public void setMaxSP(int maxSP)
        {
            this.maxSP = maxSP;
        }

        public void setCurrentSP(int currentSP)
        {
            this.currentSP = currentSP;
        }

        public void setDefense(int defense)
        {
            this.defense = defense;
        }

        public void addSkill(Skill skill)
        {
            skillList.Add(skill);
        }

        public void setStatStrength(int statStrength)
        {
            this.statStrength = statStrength;
        }

        public void setStatDexterity(int statDexterity)
        {
            this.statDexterity = statDexterity;
        }

        public void setStatEndurance(int statEndurance)
        {
            this.statEndurance = statEndurance;
        }

        public void setStatIntelligence(int statIntelligence)
        {
            this.statIntelligence = statIntelligence;
        }

        public void setResSlash(double resSlash)
        {
            this.resSlash = resSlash;
        }

        public void setResPierce(double resPierce)
        {
            this.resPierce = resPierce;
        }

        public void setResBlunt(double resBlunt)
        {
            this.resBlunt = resBlunt;
        }

        public void setResFire(double resFire)
        {
            this.resFire = resFire;
        }

        public void setResWater(double resWater)
        {
            this.resWater = resWater;
        }

        public void setResIce(double resIce)
        {
            this.resIce = resIce;
        }

        public void setResElectric(double resElectric)
        {
            this.resElectric = resElectric;
        }

        public void setResEarth(double resEarth)
        {
            this.resEarth = resEarth;
        }

        public void setResWind(double resWind)
        {
            this.resWind = resWind;
        }

        public string getName()
        {
            return name;
        }

        public int getLevel()
        {
            return level;
        }

        public bool getIsUnconscious()
        {
            return isUnconscious;
        }

        public int getMaxHP()
        {
            return maxHP;
        }

        public int getCurrentHP()
        {
            return currentHP;
        }

        public int getMaxSP()
        {
            return maxSP;
        }

        public int getCurrentSP()
        {
            return currentSP;
        }

        public int getDefense()
        {
            return defense;
        }

        public List<Skill> getSkillList()
        {
            return skillList;
        }

        public int getStatStrength()
        {
            return statStrength;
        }

        public int getStatDexterity()
        {
            return statDexterity;
        }

        public int getStatEndurance()
        {
            return statEndurance;
        }

        public int getStatIntelligence()
        {
            return statIntelligence;
        }

        public double getResSlash()
        {
            return resSlash;
        }

        public double getResPierce()
        {
            return resPierce;
        }

        public double getResBlunt()
        {
            return resBlunt;
        }

        public double getResFire()
        {
            return resFire;
        }

        public double getResWater() {
            return resWater;
        }

        public double getResIce()
        {
            return resIce;
        }

        public double getResElectric()
        {
            return resElectric;
        }

        public double getResEarth()
        {
            return resEarth;
        }

        public double getResWind()
        {
            return resWind;
        }
    }
}
