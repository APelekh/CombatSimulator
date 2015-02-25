using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSimulator
{
    class Program
    {
        //declaring global variables
        static Random rng = new Random();
        static bool playingMainGame = true;
        static int playerHP = 100;
        static int enemyHP = 200;
        static int swordLowestDmg = 20;
        static int swordHighestDmg = 35;
        static int magicLowestDmg = 10;
        static int magicHighestDmg = 15;
        static int healLowest = 10;
        static int healHighest = 20;
        static int enemyLowestDmg = 5;
        static int enemyHighestDmg = 15;
        static int outputPause = 500;

        static void Main(string[] args)
        {
            Game();

            Console.ReadKey();
        }

        /// <summary>
        /// Processing the game
        /// </summary>
        static void Game()
        {
            //asking for player's name
            Console.Write("Please enter your name: ");
            string name = Console.ReadLine();
            Console.Clear();
            //printing out greeting and instructions
            string greeting = "Greeting player " + name + "!";
            for (int i = 0; i < greeting.Length; i++)
            {
                Console.Write(greeting[i]);
                System.Threading.Thread.Sleep(50);
            }
            Console.WriteLine("\n");
            Console.WriteLine(@"How to play:
You are going to fight with a dragon who wants to kill you. You will have a choice of easy and hardcore version. 
In easy version, you will have three options to use at each move:
1 - Sword. Sword does a big damage between 20 and 35, but hits only 70% of the time.
2 - Magic. Magic does lower damage between 10 and 15, but hits all the time.
3 - Heal. You heal yourself for 10 to 20 HP.
Dragon will hit you back each move between 5 and 15 damage, and he hits only 80% of the time.
You start with 100HP and Dragon starts with 200HP. 

In hardcore version, dragon is stronger and few new features added. You will figure it out. Choices and HP amounts stays the same.");
            //asking for user's choice
            Console.Write("\nFor easy version enter 1 and for hardcore enter 2: ");
            string userChoice = Console.ReadLine();
            //creating a boolean for user choice loop
            bool userChoiceBool = true;
            //user choice loop
            while (userChoiceBool)
            {
                //checking if input is valid and if it isn't 3
                if (InputValidation(userChoice) && int.Parse(userChoice) <= 2)
                {
                    //executes if user input was 1
                    if (int.Parse(userChoice) == 1)
                    {
                        //clearing the console and printing out message
                        Console.Clear();
                        Console.WriteLine("You chose easy version. Remember: you must enter a number between 1 and 3 for your choice. Good luck!\n");
                        //stops user choice loop and calls function of easy version
                        userChoiceBool = false;
                        EasyVersion();
                    }
                    //executes if user input was 2
                    else if (int.Parse(userChoice) == 2)
                    {
                        //increasing console window size
                        Console.WindowHeight = 45;
                        Console.WindowWidth = 115;
                        //clearing the console and printing out message
                        Console.Clear();
                        Console.WriteLine("You chose hardcore version. Remember: you must enter a number between 1 and 3 for your choice. Good luck!");
                        //stops user choice loop and calls function of hardcore version
                        userChoiceBool = false;
                        HardcoreVersion();
                    }
                }
                else
                {
                    //executes if input was invalid
                    //printing out a message and asks for another input
                    Console.WriteLine("Your input is invalid. Please enter 1 or 2.");
                    Console.Write("\nFor easy version enter 1 and for hardcore enter 2: ");
                    userChoice = Console.ReadLine();
                }
            }
            Console.WriteLine("\n");
            
        }
        /// <summary>
        /// Processing a hardcore version
        /// </summary>
        static void EasyVersion()
        {
            //counter to check for first print out
            int counter = 0;
            //main game loop
            while (playingMainGame)
            {
                //calling a function for print out of current HP status
                CurrentHpStatus(counter);
                //prints out a picture
                Console.WriteLine(@"   
                                \||/
                                |  @___oo          
                    /\  /\   / (__,,,,|      
                    ) /^\) ^\/ _)                             /
                    )   /^\/   _)                      ,~~   /
                    )   _ /  / _)                  _  <=)  _/_ 
                /\  )/\/ ||  | )_)          VS    /I\.=""==.{>
                <  >      |(,,) )__)               \I/-\T/-'
                ||      /    \)___)\                  /_\
                | \____(      )___) )___             // \\_     
                \______(_______;;; __;;;           _I    /      ");
                //asking user for input
                Console.Write("\nPlease enter your choise: ");
                string userInput = Console.ReadLine();
                Console.WriteLine("\n");
                //checking if input was valid
                if (InputValidation(userInput))
                {
                    //executes input was valid
                    //calling functions for player and enemy moves
                    PlayerMove(userInput);
                    EnemyMove();
                }
                else
                {
                    //executes if input was invalid
                    //printing out message and process enemy move
                    Console.WriteLine("Your input is invalid, and dragon doesn't care!");
                    System.Threading.Thread.Sleep(outputPause);
                    EnemyMove();
                }
                //calling function to check for game end
                CheckForGameEnd();
                //clearing console and incrementing output counter
                Console.Clear();
                counter++;
            }
        }

        /// <summary>
        /// Processing a hardcore version
        /// </summary>
        static void HardcoreVersion()
        {
            //counter to check for first print out
            int counter = 0;
            //main game loop
            while (playingMainGame)
            {
                //calling a function for print out of current HP status  
                CurrentHpStatus(counter);
                //prints out a picture
                Console.WriteLine(@"
                                                                 ^                       ^
                                                                 |\   \        /        /|
                                                                /  \  |\__  __/|       /  \
                                                               / /\ \ \ _ \/ _ /      /    \
                                                              / / /\ \ {*}\/{*}      /  / \ \
                                                              | | | \ \( (00) )     /  // |\ \
                                                              | | | |\ \(V""V)\    /  / | || \| 
                                                              | | | | \ |^--^| \  /  / || || || 
                                                             / / /  | |( WWWW__ \/  /| || || ||
                                            _______         | | | | | |  \______\  / / || || || 
                                  |\     /|(  ____ \        | | | / | | )|______\ ) | / | || ||
                                  | )   ( || (    \/        / / /  / /  /______/   /| \ \ || ||
         ,;~;,  ))                | |   | || (_____        / / /  / /  /\_____/  |/ /__\ \ \ \ \
            /\_                   ( (   ) )(_____  )      | | | / /  /\______/    \   \__| \ \ \
           (  /                    \ \_/ /       ) |      | | | | | |\______ __    \_    \__|_| \
           (()      ;,;             \   /  /\____) |      | | ,___ /\______ _  _     \_       \  |
           | \\  ,,;;'(              \_/   \_______)      | |/    /\_____  /    \      \__     \ |    /\
       __ _(  )m=(((((((======-------                     |/ |   |\______ |      |        \___  \ |__/  \
     /'  '\'()/~' ' /'\.                                  v  |   |\______ |      |            \___/     |
  ,;(      )||     (                                         |   |\______ |      |                    __/
 ,;' \    /-(.;,   )                                          \   \________\_    _\               ____/
      ) /       ) /                                         __/   /\_____ __/   /   )\_,      _____/
     //         ||                                         /  ___/  \uuuu/  ___/___)    \______/
    (_\         (_\                                        VVV  V        VVV  V ");
                //asking user for input
                Console.Write("\nPlease enter your choise: ");
                string userInput = Console.ReadLine();
                Console.WriteLine("\n");
                //checking if input was valid
                if (InputValidation(userInput))
                {
                    //executes input was valid
                    //calling functions for player and enemy moves
                    PlayerMoveHardcore(userInput);
                    EnemyMoveHardcore();
                }
                else
                {
                    //executes if input was invalid
                    //printing out message and process enemy move
                    Console.WriteLine("Your input is invalid, and dragon doesn't care!");
                    System.Threading.Thread.Sleep(outputPause);
                    EnemyMoveHardcore();
                }
                //asking for any input before going to next move
                Console.Write("\nPress any key to keep going...");
                Console.ReadKey();
                //calling function to check for game end
                CheckForGameEnd();
                //clearing console and incrementing output counter
                Console.Clear();
                counter++;
            }
        }
        /// <summary>
        /// Checking if a game should end or not
        /// </summary>
        static void CheckForGameEnd()
        {
            //executes if player and enemy killed each other simultaneously
            if (playerHP <= 0 && enemyHP <= 0)
            {
                //prints out message, waiting for any input and stops main game loop
                Console.WriteLine("\nYou both killed each other...Error...Computer will blow up in 5..4..3..2..1...");
                Console.ReadKey();
                playingMainGame = false;
            }
            //executes if player lost
            else if (playerHP <= 0)
            {
                //prints out message, waiting for any input and stops main game loop
                Console.WriteLine("\nDragon has slain you! You're dead.");
                Console.ReadKey();
                playingMainGame = false;
            }
            //executes if player won
            else if (enemyHP <= 0)
            {
                //prints out message, waiting for any input and stops main game loop
                Console.WriteLine("\nCongratulations! You've killed the dragon!");
                Console.ReadKey();
                playingMainGame = false;
            }
        }
        /// <summary>
        /// Printing out the current HP status
        /// </summary>
        /// <param name="counter">Counter to check if it was a first print out or not</param>
        static void CurrentHpStatus(int counter)
        {
            //prints out a current HP status
            Console.WriteLine("Current HP status -> Your HP: {0}    Dragon HP: {1}", playerHP, enemyHP);
            //executes if it wasn't a first print out
            if (counter >= 1)
            {
                //prints out a reminder message
                Console.WriteLine("\nReminder about choises: ");
                Console.WriteLine("1 - Sword, 2 - Magic, 3 - Heal");
            }
        }
        /// <summary>
        /// Processing user's choice for easy version
        /// </summary>
        /// <param name="userInput">User's input</param>
        static void PlayerMove(string userInput)
        {
            //executes if choice was 1 (sword)
            if (int.Parse(userInput) == 1)
            {
                //initializing player's hit chance
                int playerHitChance = rng.Next(1, 11);
                //initializing random number for player's damage
                int playerHit = rng.Next(swordLowestDmg, swordHighestDmg + 1);
                //checking if player's hit chance of 70% has happened
                if (playerHitChance <= 7)
                {
                    //executes if hit was successful, then decreasing enemy's HP and printing message
                    enemyHP -= playerHit;
                    Console.WriteLine("You hit the dragon for {0} DMG with your sword!", playerHit);
                    System.Threading.Thread.Sleep(outputPause);
                }
                else
                {
                    //executes if hit was unsuccessful
                    Console.WriteLine("You missed!");
                    System.Threading.Thread.Sleep(outputPause);
                }
            }
            //executes if choice was 2 (magic)
            else if (int.Parse(userInput) == 2)
            {
                //initializing random number for player's damage, decreasing enemy's HP and printing message
                int playerHit = rng.Next(magicLowestDmg, magicHighestDmg + 1);
                enemyHP -= playerHit;
                Console.WriteLine("You hit the dragon for {0} DMG with your magic!", playerHit);
                System.Threading.Thread.Sleep(outputPause);
            }
            //executes if choice was 3 (heal)
            else
            {
                //initializing random number for player's heal
                //increasing player's HP and printing message
                int playerHeal = rng.Next(healLowest, healHighest);
                playerHP += playerHeal;
                Console.WriteLine("You healed yourself for {0} points!", playerHeal);
                System.Threading.Thread.Sleep(outputPause);
            }
        }
        /// <summary>
        /// Processing enemy move for easy version
        /// </summary>
        static void EnemyMove()
        {
            //initializing enemy's hit chance
            int enemyHitChance = rng.Next(1, 11);
            //initializing random number for enemy's damage
            int enemyHit = rng.Next(enemyLowestDmg, enemyHighestDmg + 1);
            //checking if enemy's hit chance of 80% has happened
            if (enemyHitChance <= 8)
            {
                //executes if hit was successful, then decreasing playe's HP, printing message and waiting
                playerHP -= enemyHit;
                Console.WriteLine("Dragon hit you for {0} DMG!", enemyHit);
                System.Threading.Thread.Sleep(outputPause);
            }
            else
            {
                //executes if hit was unsuccessful, printing message and waiting
                Console.WriteLine("Dragon missed his hit on you!");
                System.Threading.Thread.Sleep(outputPause);
            }
        }

        /// <summary>
        /// Processing user's choice for hardcore version
        /// </summary>
        /// <param name="userInput">User's input</param>
        static void PlayerMoveHardcore(string userInput)
        {
            //executes if choice was 1 (sword)
            if (int.Parse(userInput) == 1)
            {
                //initializing player's hit chance
                int playerHitChance = rng.Next(1, 11);
                //initializing random number for player's damage
                int playerHit = rng.Next(swordLowestDmg, swordHighestDmg + 1);
                //checking if player's hit chance of 70% has happened
                if (playerHitChance <= 7)
                {
                    //executes if hit was successful, then decreasing enemy's HP and printing message
                    enemyHP -= playerHit;
                    Console.WriteLine("You hit the dragon for {0} DMG with your sword!", playerHit);
                    //System.Threading.Thread.Sleep(outputPause);
                }
                else
                {
                    //executes if hit was unsuccessful
                    Console.WriteLine("You missed!");
                    //System.Threading.Thread.Sleep(outputPause);
                }
            }
            //executes if choice was 2 (magic)
            else if (int.Parse(userInput) == 2)
            {
                //initializing random number for player's damage, decreasing enemy's HP and printing message
                int playerHit = rng.Next(magicLowestDmg, magicHighestDmg + 1);
                enemyHP -= playerHit;
                Console.WriteLine("You hit the dragon for {0} DMG with your magic!", playerHit);
                //System.Threading.Thread.Sleep(outputPause);
            }
            //executes if choice was 3 (heal)
            else
            {
                //initializing random number for player's heal. In hardcore version it's doubled
                //increasing player's HP and printing message
                int playerHeal = rng.Next(healLowest, healHighest) * 2;
                playerHP += playerHeal ;
                Console.WriteLine("You healed yourself for {0} points! Now your heal is stronger!", playerHeal);
                //System.Threading.Thread.Sleep(outputPause);
            }
        }
        /// <summary>
        /// Process enemy move in hardcore version
        /// </summary>
        static void EnemyMoveHardcore()
        {
            //initializing a random number for crit chance
            int enemyCritChance = rng.Next(1, 11);
            //calculating enemy damage. It's doubled in hardcore version
            int enemyHit = rng.Next(enemyLowestDmg, enemyHighestDmg + 1) * 2;
            //checking if a 20% crit chance happened
            if (enemyCritChance <= 2)
            {
                //if crit chance happened, then damage is doubled. Decreasing player HP
                playerHP -= enemyHit * 2;
                //printing out message and executes sound
                Console.WriteLine("Dragon did a critical hit on you - {0}! Damage is doubled!".ToUpper(), enemyHit * 2);
                Console.Beep(700, 2000);
            }
            else
            {
                //decreasing player HP by the amount of enemy hit and printing the message
                playerHP -= enemyHit;
                Console.WriteLine("Dragon hit you for {0} DMG! Now he never misses and his damage is higher!", enemyHit);
                //System.Threading.Thread.Sleep(outputPause);
            }
        }
        /// <summary>
        /// Checking input for valid values
        /// </summary>
        /// <param name="userInput">Input to be checked</param>
        /// <returns>Returns true if input is valid and false if not</returns>
        static bool InputValidation(string userInput)
        {
            int result = 0;
            //checking if input is a number
            if (int.TryParse(userInput, out result))
            {
                //checking if input is between 1 and 3
                if (int.Parse(userInput) >= 1 && int.Parse(userInput) <= 3)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
