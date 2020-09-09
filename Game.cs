﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Game
    {
        bool _gameOver = false;
        string _playerName = "Hero";
        int _playerHealth = 120;
        int _playerDamage = 20;
        int _playerDefense = 20;
        int levelScaleMax = 5;
        Random random;
        //Run the game
        public void Run()
        {
            Start();
            char input;
            Console.WriteLine("Are ya feeling lucky "+ _playerName +"?");
            GetInput(out input, "Flip Coin", "Not Really");
            if (input == '1')
            {
                random = new Random();
                int randomNumber = random.Next(0, 1000);
                if (randomNumber >= 500)
                {
                    Console.WriteLine("Heads, you live, for now, now scurry through that door");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Tails, ooooh. yeah, so begone!");
                    Console.ReadKey();
                    _gameOver = true;
                }
            }
            else if (input == '2')
            {
                Console.WriteLine("Coward");
                Console.ReadKey();
            }
            while (_gameOver == false)
            {                
                Update();
            }
            End();
        }
        //This function handles the battles for our ladder. roomNum is used to update the our opponent to be the enemy in the current room. 
        //turnCount is used to keep track of how many turns it took the player to beat the enemy
        bool StartBattle(int roomNum, ref int turnCount)
        {
            //initialize default enemy stats
            int enemyHealth = 0;
            int enemyAttack = 0;
            int enemyDefense = 0;
            string enemyName = "";
            //Changes the enemy's default stats based on our current room number. 
            //This is how we make it seem as if the player is fighting different enemies
            random = new Random();
            switch (roomNum)
            {
                case 0:
                    {
                        enemyHealth = random.Next(60, 80);
                        enemyAttack = random.Next(30,65);
                        enemyDefense = random.Next(3,15);
                        enemyName = "Wizard";
                        break;
                    }
                case 1:
                    {
                        enemyHealth = random.Next(130,200);
                        enemyAttack = random.Next(25,40);
                        enemyDefense = random.Next(10, 20);
                        enemyName = "Troll";
                        break;
                    }
                case 2:
                    {
                        
                        enemyHealth = random.Next(200,300);
                        enemyAttack = random.Next(40,90);
                        enemyDefense = 10;
                        enemyName = "Giant";
                        break;
                    }
            }

            //Loops until the player or the enemy is dead
            while(_playerHealth > 0 && enemyHealth > 0)
            {
                //Displays the stats for both charactersa to the screen before the player takes their turn
                PrintStats(_playerName, _playerHealth, _playerDamage, _playerDefense);
                PrintStats(enemyName, enemyHealth, enemyAttack, enemyDefense);

                //Get input from the player
                char input;
                GetInput(out input, "Attack", "Defend");
                //If input is 1, the player wants to attack. By default the enemy blocks any incoming attack
                if (input == '1')
                {
                    Attack(ref enemyHealth, enemyDefense);
                    Console.WriteLine("You dealt " + _playerDamage + " damage. But " + enemyName + " blocked " + enemyDefense + "!");
                    Console.Write("> ");
                    turnCount++;
                    Console.ReadKey();
                    //After the player attacks, the enemy takes its turn. Since the player decided not to defend, the block attack function is not called.
                    if (enemyHealth > 0)
                    {
                        _playerHealth -= enemyAttack;
                        Console.WriteLine(enemyName + " dealt " + enemyAttack + " damage.");
                        Console.Write("> ");
                        Console.ReadKey();

                    }
                    if (enemyHealth <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine(enemyName + " has fallen, their body burns away leaving a gold token with a dollar sign embedded onto it");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                    }
                }
                //If the player decides to defend the enemy just takes their turn. However this time the block attack function is
                //called instead of simply decrementing the health by the enemy's attack value.
                else if (input == '2')
                {
                    BlockAttack(_playerHealth, enemyAttack, _playerDefense);
                    Console.WriteLine(enemyName + " dealt " + enemyAttack + " damage.But you blocked " + _playerDefense);
                    Console.Write("> ");
                    Console.ReadKey();
                    turnCount++;
                    Console.Clear();
                }                
            } if (_gameOver = true || _playerHealth <= 0)
            {
                _gameOver = true;
                _playerHealth = 0;
            }
            //Return whether or not our player died
            return _playerHealth != 0;

        }
        //Decrements the health of a character. The attack value is subtracted by that character's defense
        void BlockAttack(int _opponentHealth, int attackVal, int _playerdefense)
        {
            int damage = attackVal - _playerDefense;
            if(damage < 0)
            {
                damage = 0;
            }
            _playerHealth -= damage;
        }
        void Attack(ref int _opponentHeatlh, int opponentDefense)
        {
            int damage = _playerDamage - opponentDefense;
            if(damage<0)
            {
                damage = 0;
            }
            _opponentHeatlh -= damage;

        }
        //Scales up the player's stats based on the amount of turns it took in the last battle
        void UpgradeStats(int turnCount)
        {
            //Subtract the amount of turns from our maximum level scale to get our current level scale
            int scale = levelScaleMax - turnCount;
            if(scale <= 0)
            {
                scale = 1;
            }
            _playerHealth += 10 * scale;
            _playerDamage += 10 * scale;
            _playerDefense *= scale;             
        }
        void UpgradeStats(string option1, int option1int, string option2, int option2int,string option3)
        {
            bool exit = false;
            while (exit == false)
            {
                Console.Clear();
                Console.WriteLine("ShopKeep: Welcome to my shop, traveler!");
                Console.WriteLine("Would you prefer " + option1 + " or " + option2 + " or " + option3);
                char input = ' ';
                Console.WriteLine("1. " + option1);
                Console.WriteLine("2. " + option2);
                Console.WriteLine("3. " + option3);
                input = Console.ReadKey().KeyChar;
                if (input == '1')
                {
                    _playerHealth += option1int;
                    Console.WriteLine("You just bought " + option1 + "!");
                    Console.WriteLine("Goodbye!!");
                    Console.ReadKey();
                    Console.Clear();
                    exit = true;
                }
                else if (input == '2')
                {
                    _playerDefense += option2int;
                    Console.WriteLine("You just bought " + option2 + "!");
                    Console.WriteLine("Goodbye!!");
                    Console.ReadKey();
                    Console.Clear();
                    exit = true;
                }
                else if (input == '3')
                {
                    int gambletiermultiplier = random.Next(1,10);
                    gambletiermultiplier /= 10;
                    int gambletierone = random.Next(0,9);
                    if (gambletierone == 0)
                    {
                        Console.WriteLine("Fate has decided to increase your health by " + gambletiermultiplier + "%");                        
                        Console.WriteLine("You previously " + _playerHealth + "which is now");
                        int multiplyingint = _playerHealth *= gambletiermultiplier;
                        _playerHealth += multiplyingint;
                        Console.Write(_playerHealth);
                        Console.ReadKey();
                        exit = true;
                    }
                    else if (gambletierone == 1)
                    {
                        Console.WriteLine("Fate has decided to increase your damage by " + gambletiermultiplier + "%");                        
                        Console.WriteLine("You previously had " + _playerDamage + " player damage which is now");
                        int multiplyingint = _playerDamage *= gambletiermultiplier;
                        _playerDamage += multiplyingint;
                        Console.Write(_playerDamage);
                        Console.ReadKey();
                        exit = true;
                    }
                    else if (gambletierone >= 2 && gambletierone <= 5)
                    {
                        Console.WriteLine("Unfortunate you weren't lucky enough to increase, but lucky enough to live");
                        Console.ReadKey();
                        exit = true;
                    }
                    else if (gambletierone >= 6 && gambletierone <=9)
                    {
                        Console.WriteLine("Unfortunately fate has decided this is where you die");
                        _gameOver = true;
                        _playerHealth = 0;
                        Console.ReadKey();
                    }
                    else
                        Console.WriteLine("THERE WAS ERROR");
                }
                else if (input != '1' && input != '2' && input != '3')
                {
                    Console.Clear();
                    Console.WriteLine("Invalid option");
                    Console.ReadKey();
                    exit = false;
                }
            }
        }
        //Gets input from the player
        //Out's the char variable given. This variables stores the player's input choice.
        //The parameters option1 and option 2 displays the players current chpices to the screen
        void GetInput(out char input,string option1, string option2)
        {
            //Initialize input
            input = ' ';
            //Loop until the player enters a valid input
            while (input != '1' && input != '2')
            {
                Console.WriteLine("1." + option1);
                Console.WriteLine("2." + option2);
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                Console.Clear();
            }
        }
        void GetInput(out char input, string option1, string option2, string query)
        {
            Console.WriteLine(query);
            //Initialize input
            input = ' ';
            //Loop until the player enters a valid input
            while (input != '1' && input != '2')
            {
                Console.WriteLine("1." + option1);
                Console.WriteLine("2." + option2);
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
            }
        }

        //Prints the stats given in the parameter list to the console
        void PrintStats(string name, int health, int damage, int defense)
        {
            Console.WriteLine("\n" + name);
            Console.WriteLine("Health: " + health);
            Console.WriteLine("Damage: " + damage);
            Console.WriteLine("Defense: " + defense);
        }

        //This is used to progress through our game. A recursive function meant to switch the rooms and start the battles inside them.
        void ClimbLadder(int roomNum)
        {
            //Displays context based on which room the player is in
            switch (roomNum)
            {
                case 0:
                    {
                        Console.WriteLine("A wizard blocks your path");       
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("A troll stands before you");          
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("A giant has appeared!");            
                        break;
                    }
                default:
                    {
                        _gameOver = true;
                        return;
                    }
            }
            int turnCount = 0;
            //Starts a battle. If the player survived the battle, level them up and then proceed to the next room.
           
                if (StartBattle(roomNum, ref turnCount))
                {
                    UpgradeStats(turnCount);
                    UpgradeStats("100 health", 100, "20 defense", 20, "tempt fate with the coin");
                    ClimbLadder(roomNum + 1);
                }
            
            _gameOver = true;

        }

        //Displays the character selection menu. 
        void SelectCharacter()
        {
            char input = ' ';
            //Loops until a valid option is choosen
            while(input != '1' && input != '2' && input != '3')
            {
                //Prints options
                Console.WriteLine("Welcome! Please select a character.");
                Console.WriteLine("1.Sir Kibble");
                Console.WriteLine("2.Gnojoel");
                Console.WriteLine("3.Joedazz");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                //Sets the players default stats based on which character was picked
                switch (input)
                {
                    case '1':
                        {
                            _playerName = "Sir Kibble";
                            _playerHealth = 120;
                            _playerDefense = 20;
                            _playerDamage = 40;
                            break;
                        }
                    case '2':
                        {
                            _playerName = "Gnojoel";
                            _playerHealth = 40;
                            _playerDefense = 10;
                            _playerDamage = 70;
                            break;
                        }
                    case '3':
                        {
                            _playerName = "Joedazz";
                            _playerHealth = 200;
                            _playerDefense = 30;
                            _playerDamage = 25;
                            break;
                        }
                        //If an invalid input is selected display and input message and input over again.
                    default:
                        {
                            Console.WriteLine("Invalid input. Press any key to continue.");
                            Console.Write("> ");
                            Console.ReadKey();
                            break;
                        }
                }
                Console.Clear();
            }
            //Prints the stats of the choosen character to the screen before the game begins to give the player visual feedback
            PrintStats(_playerName,_playerHealth,_playerDamage,_playerDefense);
            Console.WriteLine("Press any key to continue.");
            Console.Write("> ");
            Console.ReadKey();
            Console.Clear();
        }
        //Performed once when the game begins
        public void Start()
        {
            SelectCharacter();
        }

        //Repeated until the game ends
        public void Update()
        {
            if (_playerHealth > 0 || _gameOver == false)
            {
                ClimbLadder(0);
            }   
        }

        //Performed once when the game ends
        public void End()
        {
            //If the player died print death message
            if (_playerHealth <= 0)
            {
                Console.WriteLine("Failure");
                return;
            }
            else if (_gameOver == true)
            {
                Console.WriteLine("UNLUCKY");
                Console.ReadKey();
            }
            //Print game over message
            Console.WriteLine("Congratulations");
        } 
        struct Player
        {
            public int health;
            public float speed;
            public bool isAlive;
            public string name;
        }
    }
}
