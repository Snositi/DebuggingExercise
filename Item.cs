
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Item
    {
        private int _damage;
        private int _defense;
        private int _healthBoost;
        private string _name;        
        public Item()
        {
            _damage = 0;
            _defense = 0;
            _healthBoost = 0;
            _name = "Useless boot";
        }
        public Item (string nameVal, int damageVal, int defenseVal, int healthBoostVal)
        {
            _name = nameVal;
            _damage = damageVal;
            _defense = defenseVal;
            _healthBoost = healthBoostVal;
        }
       
    }
}
