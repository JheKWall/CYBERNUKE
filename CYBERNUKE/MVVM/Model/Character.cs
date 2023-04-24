using CYBERNUKE.GameData.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYBERNUKE.MVVM.Model
{
    public class Character
    {
        //Vars
        private string name;
        private bool isUnconscious;

        //Stats
        private int maxHP;
        private int currentHP;
        private int maxSP;
        private int currentSP;
        private int defense;
        private int statStrength;
        private int statDexterity;
        private int statEndurance;
        private int statIntelligence;
        private MainWeapon equippedWeapon;
        private MainArmor equippedOutfit;

        //References
        public TurnOrderBox currentTurnOrder;
        public PlayerBox currentPlayerBox;

        public Character()
        {
            name = null;
            isUnconscious = false;
            maxHP = 0;
            currentHP = 0;
            maxSP = 0;
            currentSP = 0;
            defense = 0;
            statStrength = 0;
            statDexterity = 0;
            statEndurance = 0;
            statIntelligence = 0;
            equippedWeapon = null;
            equippedOutfit = null;
        }

        public Character(string name, int maxHP, int maxSP, int statStrength, int statDexterity, int statEndurance, int statIntelligence, MainWeapon w, MainArmor a)
        {
            this.name = name;
            this.maxHP = maxHP;
            currentHP = maxHP;
            this.maxSP = maxSP;
            currentSP = maxSP;
            this.statStrength = statStrength;
            this.statDexterity = statDexterity;
            this.statEndurance = statEndurance;
            this.statIntelligence = statIntelligence;
            equippedWeapon = w;
            Equip_Armor(a);
        }

        public void Equip_Armor(MainArmor armor)
        {
            equippedOutfit = armor;
            setDefense(armor.getDefense());
        }

        public void setName(string name)
        {
            this.name = name;
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
        public void healHP(int healAmount)
        {
            if (currentHP < maxHP)
            {
                currentHP += healAmount;
            }
            if (currentHP > maxHP)
            {
                currentHP = maxHP;
            }
        }
        public void setMaxSP(int maxSP)
        {
            this.maxSP = maxSP;
        }
        public void setCurrentSP(int currentSP)
        {
            this.currentSP = currentSP;
        }
        public void rechargeSP(int rechargeAmount)
        {
            if (currentSP < maxSP)
            {
                currentSP += rechargeAmount;
            }
            if (currentSP > maxSP)
            {
                currentSP = maxSP;
            }
        }
        public void setDefense(int defense)
        {
            this.defense = defense;
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

        public void setEquippedWeapon(MainWeapon w)
        {
            this.equippedWeapon = w;
        }
        public MainWeapon getEquippedWeapon()
        {
            return equippedWeapon;
        }
        public int getMainWeaponDamage()
        {
            return equippedWeapon.getDamage();
        }
        public void incurWeaponSPCost()
        {
            currentSP -= equippedWeapon.getSPCost();
        }
        public int getMainWeaponSPCost()
        {
            return equippedWeapon.getSPCost();
        }
        public void setEquippedOutfit(MainArmor a)
        {
            this.equippedOutfit = a;
        }
        public MainArmor getEquippedOutfit()
        {
            return this.equippedOutfit;
        }
        public int getMainArmorDefense()
        {
            return equippedOutfit.getDefense();
        }

        public string getName()
        {
            return name;
        }
        public bool getIsUnconscious()
        {
            return isUnconscious;
        }
        public void takeDamage(int damage)
        {
            // Check if unconscious
            if (isUnconscious)
            {
                throw new Exception("Player is unconscious.");
            }

            // Modify HP
            currentHP -= damage;

            // Check if Dead
            if (currentHP <= 0)
            {
                currentHP = 0;
                isUnconscious = true;
            }
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

        public void Set_TurnOrder(TurnOrderBox turnOrder)
        {
            currentTurnOrder = turnOrder;
        }
        public void Clear_TurnOrder()
        {
            currentTurnOrder = null;
        }

        public void Set_PlayerBox(PlayerBox playerBox)
        {
            currentPlayerBox = playerBox;
        }
        public void Clear_PlayerBox()
        {
            currentPlayerBox = null;
        }
    }
}
