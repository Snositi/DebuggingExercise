using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    //Create a turn based PvP game. It should have a battle loop where both players
    //must fight until one is dead. The game should allow players to upgrade their stats
    //using items. Both players and items should be defined as structs. 
    class Game
    {
        bool singlePlayer;
        bool _gameOver = false;                
        int levelScaleMax = 5;
        Random random;
        private Player player1;

        private Player player2;
        private Item gameOverToken;
        private Item winnersToken;
        private Item armor;
        private Item healthpotion;
        private Item cheapshot;
        private Item longsword;
        private Item dagger;


        


        //Run the game
        public void Run()
        {   
            InitializeItems();
            isMultiplayer();
            if (singlePlayer == false)
                {
                    player1 = CreateCharacter();
                    player2 = CreateCharacter();
                }            
            else if (singlePlayer == true)
            {
                Start();
            
                while (_gameOver == false && singlePlayer == true)
                {                   
                    Update();
                }
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
            while(player1._health > 0 && enemyHealth > 0)
            {
                //Displays the stats for both charactersa to the screen before the player takes their turn
                PrintStats(player1._name, player1._health, player1._damage, player1._defense);
                PrintStats(enemyName, enemyHealth, enemyAttack, enemyDefense);

                //Get input from the player
                char input;
                GetInput(out input, "Attack", "Defend");
                //If input is 1, the player wants to attack. By default the enemy blocks any incoming attack
                if (input == '1')
                {
                    Attack(ref enemyHealth, enemyDefense);
                    Console.WriteLine("You dealt " + player1._damage + " damage. But " + enemyName + " blocked " + enemyDefense + "!");
                    Console.Write("> ");
                    turnCount++;
                    Console.ReadKey();
                    //After the player attacks, the enemy takes its turn. Since the player decided not to defend, the block attack function is not called.
                    if (enemyHealth > 0)
                    {
                        player1._health -= enemyAttack;
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
                    BlockAttack(player1._health, enemyAttack, player1._defense);
                    Console.WriteLine(enemyName + " dealt " + enemyAttack + " damage.But you blocked " + player1._defense);
                    Console.Write("> ");
                    Console.ReadKey();
                    turnCount++;
                    Console.Clear();
                }                
            } if (_gameOver = true || player1._health <= 0)
            {
                _gameOver = true;
                player1._health = 0;
            }
            //Return whether or not our player died
            return player1._health != 0;

        }
        //Decrements the health of a character. The attack value is subtracted by that character's defense
        void BlockAttack(int _opponentHealth, int attackVal, int playerdefense)
        {
            int damage = attackVal - playerdefense;
            if (damage < 0)
            {
                damage = 0;
            }
            player1._health -= damage;
        }
        void Attack(ref int _opponentHeatlh, int opponentDefense)
        {
            int damage = player1._damage - opponentDefense;
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
            player1._health += 10 * scale;
            player1._damage += 10 * scale;
            player1._defense *= scale;             
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
                    player1._health += option1int;
                    Console.WriteLine("You just bought " + option1 + "!");
                    Console.WriteLine("Goodbye!!");
                    Console.ReadKey();
                    Console.Clear();
                    exit = true;
                }
                else if (input == '2')
                {
                    player1._defense += option2int;
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
                        Console.WriteLine("You previously " + player1._health + "which is now");
                        int multiplyingint = player1._health *= gambletiermultiplier;
                        player1._health += multiplyingint;
                        Console.Write(player1._health);
                        Console.ReadKey();
                        exit = true;
                    }
                    else if (gambletierone == 1)
                    {
                        Console.WriteLine("Fate has decided to increase your damage by " + gambletiermultiplier + "%");                        
                        Console.WriteLine("You previously had " + player1._damage + " player damage which is now");
                        int multiplyingint = player1._damage *= gambletiermultiplier;
                        player1._damage += multiplyingint;
                        Console.Write(player1._damage);
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
                        player1._health = 0;
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

        void pvpintro()
        {
            Console.Clear();
            Console.WriteLine("Welcome to player versus player!");
            Console.WriteLine("Rules are simple, you have 4 abilities,");
            Console.WriteLine("This game continues till the winner buys an exit coin");
            Console.WriteLine("Got it?");
            Console.WriteLine("1. Yea");
            Console.WriteLine("2. No");
            Console.Write("> ");
            Console.ReadKey();
            Console.WriteLine("of course you do");
            Console.WriteLine("Press any key to begin");
            Console.ReadKey();
            Console.Clear();
        }
        void PlayerVersusPlayer()
        {
            pvpintro();
            
            while (player1.hasgameendingcoin == false && player2.hasgameendingcoin == false)
            {

            }
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
                            player1._name = "Sir Kibble";
                            player1._health = 120;
                            player1._defense = 20;
                            player1._damage = 40;                                                   
                            //  0-9 so 0 is 10% call random number if hitchance or lower then attack if else then miss
                            break;
                        }
                    case '2':
                        {
                            player1._name = "Gnojoel";
                            player1._health = 40;
                            player1._defense = 10;
                            player1._damage = 70;                           
                            break;
                        }
                    case '3':
                        {
                            player1._name = "Joedazz";
                            player1._health = 200;
                            player1._defense = 30;
                            player1._damage = 25;
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
            }
            //Prints the stats of the choosen character to the screen before the game begins to give the player visual feedback            
        }
        //Performed once when the game begins
        public void Start()
        {         
            SelectCharacter();
            Gamble();
        }

        //Repeated until the game ends
        public void Update()
        {
            bool singleplayer = true;
            Console.WriteLine("Would you like to participate in the multiplayer feature or singleplayer?");
            Console.WriteLine("*Player 2 will not participate at all in singleplayer*");
            Console.WriteLine("1. SinglePlayer");
            Console.WriteLine("2. Multiplayer");
            Console.Write("> ");
            char input = '6';
            while (input != '1' && input != '2')
            {
                input = Console.ReadKey().KeyChar;
                switch (input)
                {
                    case '1':
                        singleplayer = true;
                        break;
                    case '2':
                        singleplayer = false;
                        break;
                    default:
                        Console.WriteLine("Please select a valid option ");
                        break;
                }
                if (player1._health > 0 && singleplayer == true)
                {
                    ClimbLadder(0);
                }
                else if (player1._health > 0 && player2._health > 0 && singleplayer == false)
                {
                    player1.hasgameendingcoin = false;
                    player2.hasgameendingcoin = false;
                    PlayerVersusPlayer();
                }
            }
        }

        //Performed once when the game ends
        public void End()
        {
            //If the player died print death message
            if (player1._health <= 0)
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
        
        void Gamble()
        {
            char input;
            Console.WriteLine("Are ya feeling lucky " + player1._name + "?");
            GetInput(out input, "Flip Coin", "Not Really");
            if (input == '1')
            {
                random = new Random();
                int randomNumber = random.Next(0, 1000);
                if (randomNumber >= 500)
                {
                    Console.WriteLine("Heads, huh, that doesn't happen often, ok well here's bonus damage I suppose.");
                    player1._damage += 15;
                    Console.WriteLine("You gained 15 damage points for a total of " + player1._damage + "!");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Tails, ooooh. big bummer bro good luck but im taking your shoes");
                    Console.WriteLine("*Removed shoes");
                    Console.WriteLine("-5 defense");
                    Console.ReadKey();
                }
            }
            else if (input == '2')
            {
                Console.WriteLine("Coward");
                Console.ReadKey();
            }
        }
        
        public void SelectItems(Player player)
        {
            char input;
            GetInput(out input, "LongSword", "Dagger", "Welcome! Please choose a weapon.");
            if (input == '1')
            {
                player1.EquipItem(longsword);
            }
            else if (input == '2')
            {
                player1.EquipItem(dagger);
            }
            Console.WriteLine("Player one stats");
            player1.PrintStats();
            GetInput(out input, "LongSword", "Dagger", "Welcome! Please choose a weapon.");
            if (input == '1')
            {
                player2.EquipItem(longsword);
            }
            else if (input == '2')
            {
                player2.EquipItem(dagger);
            }
            Console.WriteLine("Player two stats");
            player1.PrintStats();
        }
        public Player CreateCharacter() 
        {
            Console.WriteLine("Hello, please type your name.");
            string name = Console.ReadLine();
            Player player = new Player(name, 100, 5, 3);
            SelectItems(player);
            return player;
        }
            void isMultiplayer()
            {
                Console.Clear();
                char input;
                GetInput(out input, "SinglePlayer", "Multiplayer", "What gamemode do you wish to play?");
                if (input == '1')
                {
                    singlePlayer = true;
                }
                if (input == '2')
                {
                    singlePlayer = false; 
                }
            }
        void InitializeItems()
        {
            longsword.damage = 5;
            longsword.health = 10;
            longsword.defense = 8;
            longsword.name = "Knights Long Sword";
            dagger.damage = 4;
            dagger.health = 20;
            dagger.defense = 0;
            dagger.name = "Short blade of the damned";
        }

        
    }
}


