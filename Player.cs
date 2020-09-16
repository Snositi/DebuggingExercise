using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Player
    {
        private string _name;
        private int _health;
        private int _damage;
        private int _defense;
        public Player()
        {
            _name = "NewPlayer";
            _health = 100;
            _damage = 5;
            _defense = 3;
        }
        public Player(string nameVal, int healthVal, int damageVal, int defenseVal)
        {
            _name = nameVal;
            _health = healthVal;
            _damage = damageVal;
            _defense = defenseVal;
        }
        public bool GetIsAlive()
        {
            return _health > 0;
        }
        public void EquipItem(Item weapon)
        {
            _damage += weapon.GetDamage();
            _defense += weapon.GetDefense();
            _health += weapon.GetHealthBoost();            
        }
        public string GetName()
        {
            return _name;
        }
        public void Attack(Player enemy)
        {
            enemy.TakeDamage(_damage);
        }
        public void Printstats()
        {
            Console.WriteLine("Name: " + _name + "\nHealth: " + _health + "\n Damage: " + _damage);
        }
        public void TakeDamage(int damageVal)
        {
            if (GetIsAlive())
            {
                _health -= damageVal;
                Console.WriteLine(_name + " took" + damageVal + " damage!");
            }
        }        
    }
}
