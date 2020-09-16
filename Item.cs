
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Item
    {
        public int damage;
        public int defense;
        public int healthBoost;
        private string itemName;        
        public Item()
        {
            damage = 0;
            defense = 0;
            healthBoost = 0;
            itemName = "Useless boot";
        }
        public Item (string nameVal, int damageVal, int defenseVal, int healthBoostVal)
        {
            itemName = nameVal;
            damage = damageVal;
            defense = defenseVal;
            healthBoost = healthBoostVal;
        }
    }
}
