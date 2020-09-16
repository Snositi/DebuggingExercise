
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Item
    {
        private int damage;
        private int defense;
        private int healthBoost;
        private string itemName;
        Item()
        {
            damage = 0;
            defense = 0;
            healthBoost = 0;
            itemName = "Useless boot";
        }
        Item (string nameVal, int damageVal, int defenseVal, int healthBoostVal)
        {
            itemName = nameVal;
            damage = damageVal;
            defense = defenseVal;
            healthBoost = healthBoostVal;
        }
    }
}
