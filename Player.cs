using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Player
    {
        private string name;
        private int health;
        private int damage;
        private int defense;
        public Player()
        {
            name = "NewPlayer";
            health = 100;
            damage = 5;
            defense = 3;
        }
        public Player(string nameVal, int healthVal, int damageVal, int defenseVal)
        {
            name = nameVal;
            health = healthVal;
            damage = damageVal;
            defense = defenseVal;
        }
        public bool GetIsAlive()
        {
            return health > 0;
        }
        public void EquipItem(Item weapon)
        {
            damage += weapon.damage;
            defense += weapon.defense;
        }
        public string GetName()
        {
            return name;
        }
        public void Attack(Player enemy)
        {
            enemy.TakeDamage(damage);
        }
        public void Printstats()
        {
            Console.WriteLine("Name: " + name + "\nHealth: " + health + "\n Damage: " + damage);
        }
        public void TakeDamage(int damageVal)
        {
            if (GetIsAlive())
            {
                health -= damageVal;
                Console.WriteLine(name + " took" + damageVal + " damage!");
            }
        }        
    }
}
