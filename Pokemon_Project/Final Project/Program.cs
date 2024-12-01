using System;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace Final_Project
{
    internal class Program
    {
        static string[] pokemonArray = { "Pansear", "Panpour", "Pansage", "Deerling", "Mincino", "Pidove", "Patrat", "Oshawott", "Snivy", "Tepig", "Simisear", "Simisage", "Simipour", "Snivy", "Oshawott", "Tepig" };
        static bool burn = false;
        static bool playerburn = false;
        static bool starterBattle = false;
        static bool pokemonCaught = false;
        static bool initialDialogue = false;
        static int battleCounter = 0;
        static int[] healthArray = { 36, 38, 37, 35, 35, 36, 36, 45, 45, 45, 45, 45, 45, 40, 40, 40 };
        static int[] defenseArray = { 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 }; //In pokemon defense is used to see how much resistance you have to attacks which I implemented whenever taking damage.
        static int[] staticHealth = { 36, 38, 37, 35, 35, 36, 36, 45, 45, 45, 45, 45, 45, 40, 40, 40 };
        static int counterMap = 0;
        static int counterMovement = 0;
        static int randomPositionX;
        static int randomPositionY;
        static int potions = 4;
        static int starter = 0;
        static int totalPoints = 0;
        static int totalDamage = 0;
        //15 Enemy Tepig
        //14 Enemy Oshawott
        //13 Enemy Snivy
        //12 Simipour
        //11 Simisage
        //10 Simisear
        //9 Tepig
        //8 Snivy
        //7 Oshawott
        //6 Patrat
        //5 Pidove
        //4 Minccino 
        //3 Deerling
        //2 Pansage
        //1 Panpour
        //0 Pansear
        static void Main(string[] args)
        {
            Start();
        }
        static int Parse()
        {
            const int MIN = 1;
            const int MAX = 3;
            int input;
            bool ifTrue;
            do
            {
                Console.WriteLine();
                ifTrue = int.TryParse(Console.ReadLine(), out input);
                if (!ifTrue || input < MIN || input > MAX)
                {
                    Console.WriteLine("Invalid Input");
                }
            }
            while (!ifTrue || input < MIN || input > MAX);
            return input;
        }
        static int Selection()
        {

            int selection = 0;
            Console.WriteLine(@"
1.Fight
2.Bag
");
            selection = Parse();

            return selection;
        }
        static int Bag() //The bag functions lets you use your pokeballs and your potions. 
        {
            int userInput;
            Console.WriteLine(@$"
Your bag contains:
1. Potions:{potions}
2. Pokeballs
What would you like to do?

Go Back?[3]
");
            userInput = Parse();
            return userInput;
        }
        static void PotionUse() //If the player decides to use a potion this function runs.
        {
            int currentHealth = 0;
            string input = "Place Holder";
            do
            {
                Console.WriteLine("Would you like to use a potion?[Y/N]");
                input = Console.ReadLine();
                if (input.ToLower() != "y" && input.ToLower() != "n")
                {
                    Console.WriteLine("Invalid Input");
                }
            }
            while (input.ToLower() != "y" && input.ToLower() != "n");
            if (input.ToLower() == "y")
            {
                if (starter == 1)
                {
                    if (potions > 0)
                    {
                        if (healthArray[9] == 45) //Makes sure you can't heal over 45 HP
                        {
                            Console.WriteLine("Tepig's health can't go any higher!");
                            Thread.Sleep(2000);
                            Console.WriteLine(); //Adding these help with cleanliness on the console. 
                        }
                        else
                        {
                            potions -= 1;
                            Console.WriteLine("You used a potion! Tepig was healed by 20HP!");
                            Thread.Sleep(2000);
                            Console.WriteLine();
                            healthArray[9] += 20;
                            currentHealth = healthArray[9];
                        }
                        if (healthArray[9] > 45) //I'm using this system to make sure the health healed will never exceed 45, let's say I used it at 45HP, it would then be 65HP. However if you subtract that by 45, 65-45=20
                        {
                            currentHealth -= 45;
                            healthArray[9] -= currentHealth;
                        }
                    }
                    else
                    {

                    }
                }
                else if (starter == 2)
                {
                    if (potions > 0)
                    {
                        if (healthArray[7] == 45)
                        {
                            Console.WriteLine("Oshawott's health can't go any higher!");
                            Thread.Sleep(2000);
                            Console.WriteLine();
                        }
                        else
                        {
                            potions -= 1;
                            Console.WriteLine("You used a potion! Oshawott was healed by 20HP!");
                            Thread.Sleep(2000);
                            Console.WriteLine();
                            healthArray[9] += 20;
                            currentHealth = healthArray[9];
                        }
                        if (healthArray[7] > 45)
                        {
                            currentHealth -= 45;
                            healthArray[7] -= currentHealth;
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    if (potions > 0)
                    {
                        if (healthArray[8] == 45)
                        {
                            Console.WriteLine("Snivy's health can't go any higher!");
                            Thread.Sleep(2000);
                            Console.WriteLine();
                        }
                        else
                        {
                            potions -= 1;
                            Console.WriteLine("You used a potion! Snivy was healed by 20HP!");
                            Thread.Sleep(2000);
                            Console.WriteLine();
                            healthArray[9] += 20;
                            currentHealth = healthArray[9];
                        }
                        if (healthArray[8] > 45)
                        {
                            currentHealth -= 45;
                            healthArray[8] -= currentHealth;
                        }
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                Bag();
            }
        }
        static void PokemonCatch(int enemy) //This functions is used to catch the pokemon.
        {
            if (healthArray[enemy] > 30) //By using the health left of the pokemon I can make it so that the chances are lowered if your health is higher.
            {
                Console.WriteLine("You threw a pokeball at the opposing Pokemon!");
                Console.WriteLine("...");
                const int MIN = 0; //Here the odds are relatively low to catch a pokemon.
                const int MAX = 15;
                Random randomizer = new Random();
                int caught = randomizer.Next(MIN, MAX);
                if (caught == 4 || caught == 7 || caught == 5 || caught == 2) //I randomly chose these numbers to make the odds relatively high but not impossible to catch a pokemon.
                {
                    Console.WriteLine($"Congrats! You caught the trainer's {pokemonArray[enemy]}!");
                    Console.WriteLine("The pokemon was sent off to the boss and you received 1500 points!"); //If caught you get points.
                    totalPoints += 1500;
                    pokemonCaught = true;
                    Thread.Sleep(3000);
                }
                else
                {
                    Console.WriteLine("It broke out of the ball! Try weakening it some more.");
                    Thread.Sleep(3000);
                }
            }
            else if (healthArray[enemy] < 30 && healthArray[enemy] > 20)
            {
                const int MIN = 0;//Here odds are better but still not great.
                const int MAX = 10;
                Random randomizer = new Random();
                int caught = randomizer.Next(MIN, MAX);
                if (caught == 4 || caught == 7 || caught == 5 || caught == 2)
                {
                    Console.WriteLine($"Congrats! You caught the trainer's {pokemonArray[enemy]}!");
                    Console.WriteLine("The pokemon was sent off to the boss and you received 1500 points!");
                    totalPoints += 1500;
                    pokemonCaught = true;
                    Thread.Sleep(3000);
                }
                else
                {
                    Console.WriteLine("It broke out of the ball! Try weakening it some more.");
                    Thread.Sleep(3000);
                }
            }
            else if (healthArray[enemy] < 20)
            {
                const int MIN = 1;//Here your odds are extremely high to get a catch.
                const int MAX = 5;
                Random randomizer = new Random();
                int caught = randomizer.Next(MIN, MAX);
                if (caught == 4 || caught == 3)
                {
                    Console.WriteLine($"Congrats! You caught the trainer's {pokemonArray[enemy]}!");
                    Console.WriteLine("The pokemon was sent off to the boss and you received 1500 points!");
                    totalPoints += 1500;
                    pokemonCaught = true;
                    Thread.Sleep(4000);
                }
                else
                {
                    Console.WriteLine("It broke out of the ball! Keep on trying, it's weak!");
                    Thread.Sleep(4000);
                }
            }
        }
        static void Tepig()
        {

            Console.Clear();
            const int MIN = 0;
            const int MAX = 10;
            Random randomizer = new Random();
            int enemy = randomizer.Next(MIN, MAX); //This randomizer is used to pick what enemy you will be facing in battle, "enemy" is also used for various other things within the other functions.        

            if (!starterBattle) //This is used so that the pokemon your fighting in the starter battle is the one that is "super effective" against you.
            {
                enemy = 14;
            }
            if (enemy == 7 || enemy == 8 || enemy == 9) //This is used to not mix up the health of the enemy's pokemon with the player's.
            {
                if (enemy == 7)
                {
                    enemy = 14;
                }
                else if (enemy == 8)
                {
                    enemy = 13;
                }
                else
                {
                    enemy = 15;
                }
            }
            if (battleCounter == 5) //This is used for the boss fight which appears ever 5 battles, including the start battle.
            {
                const int MIN2 = 10; //This is used to select between 1 of the 3 possible boss fights. 
                const int MAX2 = 12;
                Random random = new Random();
                enemy = random.Next(MIN2, MAX2);
                battleCounter = 0; //This simply resets the counter that activates the boss fight to 0.
            }
            else
            {
                if (!starterBattle) //This is simply just so the trainer doesn't repeat what it's sending out as in the starter battle dialogue they already say what they're sending out.
                {
                    starterBattle = true;
                }
                else
                {
                    Console.WriteLine($"Trainer: Go! {pokemonArray[enemy]}!"); //Chooses enemy from the array to make the trainer say what pokemon is coming out.
                    Thread.Sleep(4000);
                    Console.Clear();
                }
            }
            while (healthArray[enemy] > 0 && healthArray[9] > 0 && !pokemonCaught) //This make sure that when the enemy pokemon faints or the player's pokemon faints or the pokemon is caught the loop ends.
            {

                string sprite = Sprites(enemy);
                Console.WriteLine($@"
{pokemonArray[enemy]} 
HP:{healthArray[enemy]}/{staticHealth[enemy]}
{sprite}                                                                         

                       {pokemonArray[9]} 
                       HP:{healthArray[9]}/45
                               ,▄▌                                    
                               ▄██╓       ╓███▄                                   
                             ""█████▄,   █████                                   
                               ██████▌ ▓████▀                                   
                                ╙▀██████████                                    
                        ggg╖        ▓███████▄,                                  
                       ▐▌╢▒▓      ╓▓▀▒▒▒▒▒▀▀▀██,                                
                        ▀█▀▀     ╔▓╢╢╢╢╢╢╢╢╢╣▒]█r                               
                        █▀▄▀▀▄  ╓█▓╣╣╣╢╢╢╢╢╢╢╣ ]▌                               
                        ╙██╖╓████████▄▒╙╫╢╢╢╢╢╣░╓▓                              
                           ████████████▄╢╢╢╢╢╢╢▒▓                               
                          ▄█████████████▓╣╢╢╢╣▓▌                                
                          ██████████▒╢▓██Ñ▓▓█▀                                  
                          █▒███████▒╢╢▒█▓█▓█▀                                   
                           █▒▓▓▀▀▀█▒╢▒▀ ╙▓█▀                                    
                            ``     ▀▀▀                                            
");
                if (playerburn) //If the player is burnt this function will activate causing damage to be had on the enemy.
                {
                    int burnDamage = 3;
                    healthArray[enemy] -= burnDamage;
                    Console.WriteLine($"{pokemonArray[enemy]} took damage from the burn!");
                }
                int choice = Selection();
                if (choice == 1)
                {
                    Console.WriteLine(@"
1.Ember
2.Tail Whip
");
                    int selection = Parse();

                    switch (selection)
                    {
                        case 1:
                            int damage = Ember(enemy);
                            totalDamage+= damage;
                            healthArray[enemy] -= damage; //Makes enemy take the damage from the move. 
                            Console.WriteLine($"{pokemonArray[enemy]} took {damage} damage!");
                            Thread.Sleep(2000);
                            Console.WriteLine();

                            break;

                        case 2:
                            TailWhipPlayer(enemy);
                            break;

                    }
                }
                else if (choice == 2)
                {
                    int userInput = Bag();
                    if (userInput == 1)
                    {
                        PotionUse();
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        PokemonCatch(enemy);
                    }
                }
                enemyChoice(enemy);
            }

            if (healthArray[9] <= 0) //Causes a game over as health is <=0.
            {
                Console.WriteLine($"{pokemonArray[9]} Fainted!");
                Console.WriteLine("You flee the scene! You have failed the mission");
                Console.WriteLine("Game Over");
            }
            else if (healthArray[enemy] <= 0) //Victory dialogue if the enemy has fainted.
            {

                Console.WriteLine($"{pokemonArray[enemy]} fainted! You won the battle!");
                Console.WriteLine("");
                Thread.Sleep(2000);
                Console.WriteLine("But you didn't catch the Pokemon...though an evil deed is an evil deed, you received 200 points");
                Thread.Sleep(3000);
                totalPoints += 200;
                if (!initialDialogue) //Used for after beating the starter battle,dialogue will only ever play once due to the bools.
                {
                    Console.Clear();
                    InitialDialogue();
                    initialDialogue = true;
                }
                Reset(enemy); //Resets health for both the player and the enemy so if that pokemon is ever randomized again it won't be at 0HP.
                Console.Clear();
                Movement();
            }
            else if (pokemonCaught) //Similar to when a pokemon faints but is for if a pokemon is caught.
            {
                if (!initialDialogue)
                {
                    Console.Clear();
                    InitialDialogue();
                    initialDialogue = true;
                }
                Reset(enemy);
                pokemonCaught = false;
                playerburn = false;
                Console.Clear();
                Movement();
            }
        }
        static void Oshawott()
        {

            const int MIN = 0;
            const int MAX = 10;
            Random randomizer = new Random();
            int enemy = randomizer.Next(MIN, MAX);
            if (!starterBattle)
            {
                enemy = 8;
            }
            if (enemy == 7 || enemy == 8 || enemy == 9)
            {
                if (enemy == 7)
                {
                    enemy = 14;
                }
                else if (enemy == 8)
                {
                    enemy = 13;
                }
                else
                {
                    enemy = 15;
                }
            }
            if (battleCounter == 5)
            {
                const int MIN2 = 10;
                const int MAX2 = 12;
                Random random = new Random();
                enemy = random.Next(MIN2, MAX2);
                battleCounter = 0;
            }
            else
            {
                if (!starterBattle)
                {
                    starterBattle = true;
                }
                else
                {
                    Console.WriteLine($"Trainer: Go!{pokemonArray[enemy]}!");
                    Thread.Sleep(4000);
                    Console.Clear();
                }
            }
            while (healthArray[enemy] > 0 && healthArray[7] > 0 || !pokemonCaught)
            {
                string sprite = Sprites(enemy);
                Console.WriteLine($@"
{pokemonArray[enemy]} 
HP:{healthArray[enemy]}/{staticHealth[enemy]}
{sprite}
                                                                         

Oshawott 
HP:{healthArray[7]}/45
                                   ,,╓H@@@,,
                                ▓▓╝╜    ▒▒▒░░╨▓╖
                               ]▓▒,,,╓╥▒▒▒▒▒▒▒╟█▓▓
                               ▄╜▒▒▒▒▒▒▒▒▒▒▒▒▒▒╟██
                               ▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░█
                               ]▌▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▌
                                ▐▓▒▓▓▓▒▒▓▓▓▒▒░g▓""
                               ╣╟▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓,
                                ╚▓▓▓▓▓▓▓▓▓▓▓▓╬▒  ║▓
                                 ╓▓▓▓▓▓▓▓▓▓▓▓▓█▀▀▀
                                 ▐▓╣▓▓▓▓▓▓▓▓▓▓█
                                ,╓▓████▓▓▓▓▓▓█
                             g██▓▓▓█▓██▓████▓▓▓▄
                             `▀▀████▀`    ▀▀▀▀``               
");
                int choice = Selection();
                if (choice == 1)
                {

                    Console.WriteLine(@"
1.Water gun
2.Tail Whip
");

                    int selection = Parse();
                    switch (selection)
                    {
                        case 1:
                            int damage = Watergun(enemy);
                            healthArray[enemy] -= damage;
                            totalDamage += damage;
                            Console.WriteLine($"{pokemonArray[enemy]} took {damage} damage!");
                            Thread.Sleep(1000);
                            Console.WriteLine();
                            break;

                        case 2:
                            TailWhipPlayer(enemy);
                            break;

                    }
                }
                else
                {
                    int userInput = Bag();
                    if (userInput == 1)
                    {
                        PotionUse();
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        PokemonCatch(enemy);
                    }
                }
                enemyChoice(enemy);
            }
            if (healthArray[7] <= 0)
            {
                Console.WriteLine($"{pokemonArray[7]} Fainted!");
                Console.WriteLine("You flee the scene! You have failed the mission");
                Console.WriteLine("Game Over");
            }
            else if (healthArray[enemy] <= 0)
            {

                Console.WriteLine($"{pokemonArray[enemy]} fainted! You won the battle!");
                Console.WriteLine("");
                Thread.Sleep(2000);
                Console.WriteLine("But you didn't catch the Pokemon...though an evil deed is an evil deed, you received 200 points");
                Thread.Sleep(3000);
                totalPoints += 200;
                if (!initialDialogue)
                {
                    Console.Clear();
                    InitialDialogue();
                    initialDialogue = true;
                }
                Reset(enemy);
                Console.Clear();
                Movement();
            }
            else if (pokemonCaught)
            {

                if (!initialDialogue)
                {
                    Console.Clear();
                    InitialDialogue();
                    initialDialogue = true;
                }
                Reset(enemy);
                pokemonCaught = false;
                playerburn = false;
                Console.Clear();
                Movement();
            }

        }

        static void Snivy()
        {
            const int MIN = 0;
            const int MAX = 10;
            Random randomizer = new Random();
            int enemy = randomizer.Next(MIN, MAX);
            if (!starterBattle)
            {
                enemy = 9;
            }
            if (enemy == 7 || enemy == 8 || enemy == 9)
            {
                if (enemy == 7)
                {
                    enemy = 14;
                }
                else if (enemy == 8)
                {
                    enemy = 13;
                }
                else
                {
                    enemy = 15;
                }
            }
            if (battleCounter == 5)
            {
                const int MIN2 = 10;
                const int MAX2 = 12;
                Random random = new Random();
                enemy = random.Next(MIN2, MAX2);
                battleCounter = 0;
            }
            else
            {
                if (!starterBattle)
                {
                    starterBattle = true;
                }
                else
                {
                    Console.WriteLine($"Trainer: Go! {pokemonArray[enemy]}!");
                    Thread.Sleep(4000);
                    Console.Clear();
                }
            }
            while (healthArray[enemy] > 0 && healthArray[8] > 0 || !pokemonCaught)
            {
                string sprite = Sprites(enemy);
                Console.WriteLine($@"
{pokemonArray[enemy]} 
HP:{healthArray[enemy]}/{staticHealth[enemy]}
{sprite}
                                                                         

Snivy 
HP:{healthArray[8]}/45
              
                                        ,▄▓▓▓▓▓▄▄,
                                      ╓▓▓▓▓▓▓▓▓▓▓▓▓▄╖
                                     ]█▓▓▓▓▓▓@▒▄▄▒▓▓▓▓▀▓▀
                            ,g,      ▐█████▓▓▓]█▌▓▓╓@╢▓▀
                           ╓▓▓▓▄      ▓█████▓▓╣╫▀▓▒▓▓▀
                       ,╓  ▐▓▓▓▓▌  ╓▄,,▓█▓▓██▓▓▀▓▀""
                       ▐▓▓▄▓▓▓▓▓█▌g▓▓█▓▓▓▀▀▓▓▓█
                       ╙█▓▓▓▓▓▓▓▓█▓▓▓███▓▓▓▓▓▓]▌
                        ▀▓▓▓▓▓▓▓▓▓▓████▌╙▓▓▓█▓╖╟[
                         ▀▓▓▓▓▓▓███████`▄▓▓▓█▓▓▌╟L
                           `▀▀▀█████▀▓▄▓▓▓█████▓▓`
                                ▀█▓███▓██████▓▓▓`
                                  ▀▀█████▓▓██▀
                                      ▀▓▓╙▓▒▓▄
                                           ╙▀                    
");

                int choice = Selection();
                if (choice == 1)
                {
                    Console.WriteLine(@"
1.Vine Whip
2.Tail Whip
");
                    int selection = Parse();

                    switch (selection)
                    {
                        case 1:
                            int damage = VineWhip(enemy);

                            healthArray[enemy] -= damage;
                            totalDamage += damage;
                            Console.WriteLine($"{pokemonArray[enemy]} took {damage} damage!");
                            Thread.Sleep(1000);
                            Console.WriteLine();
                            break;

                        case 2:
                            TailWhipPlayer(enemy);
                            break;
                    }
                }
                else
                {
                    int userInput = Bag();
                    if (userInput == 1)
                    {
                        PotionUse();
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        PokemonCatch(enemy);
                    }
                }
                enemyChoice(enemy);
            }

            if (healthArray[8] <= 0)
            {
                Console.WriteLine($"{pokemonArray[8]} Fainted!");
                Console.WriteLine("You flee the scene! You have failed the mission");
                Console.WriteLine("Game Over");
            }
            else if (healthArray[enemy] <= 0)
            {

                Console.WriteLine($"{pokemonArray[enemy]} fainted! You won the battle!");
                Console.WriteLine("");
                Thread.Sleep(2000);
                Console.WriteLine("But you didn't catch the Pokemon...though an evil deed is an evil deed, you received 200 points");
                Thread.Sleep(3000);
                totalPoints += 200;
                if (!initialDialogue)
                {
                    Console.Clear();
                    InitialDialogue();
                    initialDialogue = true;
                }
                Reset(enemy);
                Console.Clear();
                Movement();
            }
            else if (pokemonCaught)
            {

                if (!initialDialogue)
                {
                    Console.Clear();
                    InitialDialogue();
                    initialDialogue = true;
                }
                Reset(enemy);
                pokemonCaught = false;
                playerburn = false;
                Console.Clear();
                Movement();
            }
        }
        //ENEMY POKEMON:
        static void Patrat(int enemy)
        {
            const int MIN = 1;
            const int MAX = 2;
            Random randomizer = new Random();
            int enemyMove = randomizer.Next(MIN, MAX); //Chooses the move the pokemon will use.
            if (healthArray[6] > 0) //This is a percaution so that the enemy doesn't attack even when fainted.
            {
                if (enemyMove == 1)
                {

                    int damage = EnemyTackle(enemy);
                    if (starter == 1)
                    {
                        healthArray[9] -= damage;
                        Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else if (starter == 2)
                    {
                        healthArray[7] -= damage;
                        Console.WriteLine($"Oshawott took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else
                    {
                        healthArray[8] -= damage;
                        Console.WriteLine($"Snivy took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                }
                else
                {
                    EnemyTailWhip(enemy);
                    Thread.Sleep(3000);
                }
            }
            Console.Clear(); //This clears all the battle dialogue as the enemy always attacks last.
        }
        static void Pidove(int enemy)
        {
            const int MIN = 1;
            const int MAX = 2;
            Random randomizer = new Random();
            int enemyMove = randomizer.Next(MIN, MAX);
            if (healthArray[5] > 0)
            {
                if (enemyMove == 1)
                {
                    int damage = EnemyTackle(enemy);
                    if (starter == 1)
                    {
                        healthArray[9] -= damage;
                        Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else if (starter == 2)
                    {
                        healthArray[7] -= damage;
                        Console.WriteLine($"Oshawott took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else
                    {
                        healthArray[8] -= damage;
                        Console.WriteLine($"Snivy took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                }
                else if (enemyMove == 2)
                {
                    if (healthArray[5] > 0)
                    {
                        int damage = EnemyGust(enemy);
                        if (starter == 1)
                        {
                            healthArray[9] -= damage;
                            Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                            Thread.Sleep(2000);
                            Console.WriteLine();
                        }
                        else if (starter == 2)
                        {
                            healthArray[7] -= damage;
                            Console.WriteLine($"Oshawott took {damage} damage!");
                            Thread.Sleep(2000);
                            Console.WriteLine();
                        }
                        else
                        {
                            healthArray[8] -= damage;
                            Console.WriteLine($"Snivy took {damage} damage!");
                            Thread.Sleep(2000);
                            Console.WriteLine();
                        }
                    }
                }
                Console.Clear();
            }

        }
        static void Mincino(int enemy)
        {
            const int MIN = 1;
            const int MAX = 3;
            Random randomizer = new Random();
            int enemyMove = randomizer.Next(MIN, MAX);
            if (healthArray[4] > 0)
            {
                if (enemyMove == 1)
                {

                    int damage = EnemyTailSlap(enemy);
                    if (starter == 1)
                    {
                        healthArray[9] -= damage;
                        Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else if (starter == 2)
                    {
                        healthArray[7] -= damage;
                        Console.WriteLine($"Oshawott took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else
                    {
                        healthArray[8] -= damage;
                        Console.WriteLine($"Snivy took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                }
                else if (enemyMove == 2)
                {
                    if (healthArray[4] > 0)
                    {
                        int damage = EnemyTackle(enemy);
                        if (starter == 1)
                        {
                            healthArray[9] -= damage;
                            Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                            Thread.Sleep(2000);
                            Console.WriteLine();
                        }
                        else if (starter == 2)
                        {
                            healthArray[7] -= damage;
                            Console.WriteLine($"Oshawott took {damage} damage!");
                            Thread.Sleep(2000);
                            Console.WriteLine();
                        }
                        else
                        {
                            healthArray[8] -= damage;
                            Console.WriteLine($"Snivy took {damage} damage!");
                            Thread.Sleep(2000);
                            Console.WriteLine();
                        }
                    }
                }
                else
                {
                    EnemyTailWhip(enemy);
                    Thread.Sleep(3000);
                }
                Console.Clear();
            }
        }
        static void Deerling(int enemy)
        {
            const int MIN = 1;
            const int MAX = 4;
            Random randomizer = new Random();
            int enemyMove = randomizer.Next(MIN, MAX);
            if (healthArray[3] > 0)
            {
                if (enemyMove == 1)
                {

                    int damage = EnemyRazorLeaf(enemy);
                    if (starter == 1)
                    {
                        healthArray[9] -= damage;
                        Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else if (starter == 2)
                    {
                        healthArray[7] -= damage;
                        Console.WriteLine($"Oshawott took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else
                    {
                        healthArray[8] -= damage;
                        Console.WriteLine($"Snivy took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                }
                else if (enemyMove == 2)
                {
                    if (healthArray[3] > 0)
                    {
                        int damage = EnemyTackle(enemy);
                        if (starter == 1)
                        {
                            healthArray[9] -= damage;
                            Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                            Thread.Sleep(2000);
                            Console.WriteLine();
                        }
                        else if (starter == 2)
                        {
                            healthArray[7] -= damage;
                            Console.WriteLine($"Oshawott took {damage} damage!");
                            Thread.Sleep(2000);
                            Console.WriteLine();
                        }
                        else
                        {
                            healthArray[8] -= damage;
                            Console.WriteLine($"Snivy took {damage} damage!");
                            Thread.Sleep(2000);
                            Console.WriteLine();
                        }
                    }
                }
                else
                {
                    EnemyTailWhip(enemy);
                    Thread.Sleep(3000);
                }
            }
            Console.Clear();
        }
        static void Pansage(int enemy)
        {
            const int MIN = 1;
            const int MAX = 3;
            Random randomizer = new Random();
            int enemyMove = randomizer.Next(MIN, MAX);
            if (healthArray[2] > 0)
            {
                if (enemyMove == 1)
                {
                    int damage = EnemyBulletSeed(enemy);
                    if (starter == 1)
                    {
                        healthArray[9] -= damage;
                        Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else if (starter == 2)
                    {
                        healthArray[7] -= damage;
                        Console.WriteLine($"Oshawott took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else
                    {
                        healthArray[8] -= damage;
                        Console.WriteLine($"Snivy took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                }
                else
                {
                    EnemyTailWhip(enemy);
                    Thread.Sleep(3000);
                }
            }
            Console.Clear();
        }
        static void Panpour(int enemy)
        {
            const int MIN = 1;
            const int MAX = 3;
            Random randomizer = new Random();
            int enemyMove = randomizer.Next(MIN, MAX);
            if (healthArray[1] > 0)
            {
                if (enemyMove == 1)
                {
                    int damage = EnemyWaterGun(enemy);
                    if (starter == 1)
                    {
                        healthArray[9] -= damage;
                        Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else if (starter == 2)
                    {
                        healthArray[7] -= damage;
                        Console.WriteLine($"Oshawott took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else
                    {
                        healthArray[8] -= damage;
                        Console.WriteLine($"Snivy took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                }
                else if (enemyMove == 2)
                {
                    EnemyTailWhip(enemy);
                    Thread.Sleep(3000);
                }
            }
            Console.Clear();
        }
        static void Pansear(int enemy)
        {
            const int MIN = 1;
            const int MAX = 3;
            Random randomizer = new Random();
            int enemyMove = randomizer.Next(MIN, MAX);
            if (burn)
            {
                if (starter == 1)
                {
                    int burnDamage = 3;
                    healthArray[9] -= burnDamage;
                    Console.WriteLine($"{pokemonArray[9]} took damage from the burn!");
                }
                else if (starter == 2)
                {
                    int burnDamage = 3;
                    healthArray[7] -= burnDamage;
                    Console.WriteLine($"{pokemonArray[7]} took damage from the burn!");
                }
                else
                {
                    int burnDamage = 3;
                    healthArray[8] -= burnDamage;
                    Console.WriteLine($"{pokemonArray[8]} took damage from the burn!");
                }
            }
            if (healthArray[0] > 0)
            {
                if (enemyMove == 1)
                {



                    int damage = EnemyEmber(enemy);

                    if (starter == 1)
                    {
                        healthArray[9] -= damage;
                        Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                    else if (starter == 2)
                    {
                        healthArray[7] -= damage;
                        Console.WriteLine($"Oshawott took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();

                    }
                    else
                    {
                        healthArray[8] -= damage;
                        Console.WriteLine($"Snivy took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                }
                else if (enemyMove == 2)
                {
                    if (healthArray[0] > 0)
                    {

                    }
                    else
                    {
                        EnemyTailWhip(enemy);
                        Thread.Sleep(3000);
                    }
                }
            }
            Console.Clear();
        }
        static void Simisear(int enemy)
        {
            int bossDamage = 2;
            const int MIN = 1;
            const int MAX = 4;
            Random randomizer = new Random();
            int enemyMove = randomizer.Next(MIN, MAX);
            if (healthArray[10] > 0)
            {
                if (enemyMove == 1)
                {

                    int damage = EnemyEmber(enemy);
                    if (starter == 1)
                    {
                        damage += bossDamage;
                        healthArray[9] -= damage;
                        Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else if (starter == 2)
                    {
                        damage += bossDamage;
                        healthArray[7] -= damage;
                        Console.WriteLine($"Oshawott took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else
                    {
                        damage += bossDamage;
                        healthArray[8] -= damage;
                        Console.WriteLine($"Snivy took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                }
                else if (enemyMove == 2)
                {
                    EnemyTailWhip(enemy);
                    Thread.Sleep(3000);
                }
                else if (enemyMove == 3)
                {

                    int damage = EnemyTackle(enemy);
                    if (starter == 1)
                    {
                        damage += bossDamage;
                        healthArray[9] -= damage;

                        Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else if (starter == 2)
                    {
                        damage += bossDamage;
                        healthArray[7] -= damage;
                        Console.WriteLine($"Oshawott took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else
                    {
                        damage += bossDamage;
                        healthArray[8] -= damage;
                        Console.WriteLine($"Snivy took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                }
            }
            Console.Clear();
        }
        static void Simisage(int enemy)
        {
            int bossDamage = 2;
            const int MIN = 1;
            const int MAX = 4;
            Random randomizer = new Random();
            int enemyMove = randomizer.Next(MIN, MAX);
            if (healthArray[11] > 0)
            {
                if (enemyMove == 1)
                {

                    int damage = EnemyRazorLeaf(enemy);
                    if (starter == 1)
                    {
                        damage += bossDamage;
                        healthArray[9] -= damage;
                        Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else if (starter == 2)
                    {
                        damage += bossDamage;
                        healthArray[7] -= damage;
                        Console.WriteLine($"Oshawott took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else
                    {
                        damage += bossDamage;
                        healthArray[8] -= damage;
                        Console.WriteLine($"Snivy took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                }
                else if (enemyMove == 2)
                {
                    EnemyTailWhip(enemy);
                    Thread.Sleep(3000);
                }
                else if (enemyMove == 3)
                {

                    int damage = EnemyBulletSeed(enemy);
                    if (starter == 1)
                    {
                        damage += bossDamage;
                        healthArray[9] -= damage;

                        Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else if (starter == 2)
                    {
                        damage += bossDamage;
                        healthArray[7] -= damage;
                        Console.WriteLine($"Oshawott took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else
                    {
                        damage += bossDamage;
                        healthArray[8] -= damage;
                        Console.WriteLine($"Snivy took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                }
            }
            Console.Clear();
        }
        static void SimiPour(int enemy)
        {
            int bossDamage = 2;
            const int MIN = 1;
            const int MAX = 4;
            Random randomizer = new Random();
            int enemyMove = randomizer.Next(MIN, MAX);
            if (healthArray[12] > 0)
            {
                if (enemyMove == 1)
                {

                    int damage = EnemyWaterGun(enemy);
                    if (starter == 1)
                    {
                        damage += bossDamage;
                        healthArray[9] -= damage;
                        Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else if (starter == 2)
                    {
                        damage += bossDamage;
                        healthArray[7] -= damage;
                        Console.WriteLine($"Oshawott took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else
                    {
                        damage += bossDamage;
                        healthArray[8] -= damage;
                        Console.WriteLine($"Snivy took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }


                }
                else if (enemyMove == 2)
                {
                    EnemyTailWhip(enemy);
                    Thread.Sleep(3000);
                }
                else if (enemyMove == 3)
                {
                    int damage = EnemyTackle(enemy);
                    if (starter == 1)
                    {
                        damage += bossDamage;
                        healthArray[9] -= damage;

                        Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else if (starter == 2)
                    {
                        damage += bossDamage;
                        healthArray[7] -= damage;
                        Console.WriteLine($"Oshawott took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else
                    {
                        damage += bossDamage;
                        healthArray[8] -= damage;
                        Console.WriteLine($"Snivy took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                }
            }
            Console.Clear();
        }
        static void enemyTepig(int enemy)
        {
            const int MIN = 1;
            const int MAX = 3;
            Random randomizer = new Random();
            int enemyMove = randomizer.Next(MIN, MAX);
            if (healthArray[15] > 0)
            {
                if (enemyMove == 1)
                {
                    int damage = EnemyEmber(enemy);
                    if (starter == 1)
                    {
                        healthArray[9] -= damage;
                        Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                        Thread.Sleep(3500);
                        Console.Clear();
                    }
                    else if (starter == 2)
                    {
                        healthArray[7] -= damage;
                        Console.WriteLine($"Oshawott took {damage} damage!");
                        Thread.Sleep(3500);
                        Console.WriteLine();
                    }
                    else
                    {
                        healthArray[8] -= damage;
                        Console.WriteLine($"Snivy took {damage} damage!");
                        Thread.Sleep(3500);
                        Console.WriteLine();
                    }
                }
                else if (enemyMove == 2)
                {
                    EnemyTailWhip(enemy);
                    Thread.Sleep(3000);
                }
            }
            Console.Clear();
        }
        static void enemyOshawott(int enemy)
        {
            const int MIN = 1;
            const int MAX = 3;
            Random randomizer = new Random();
            int enemyMove = randomizer.Next(MIN, MAX);
            if (healthArray[14] > 0)
            {
                if (enemyMove == 1)
                {
                    int damage = EnemyWaterGun(enemy);
                    if (starter == 1)
                    {
                        healthArray[9] -= damage;
                        Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                        Thread.Sleep(3000);
                        Console.WriteLine();
                    }
                    else if (starter == 2)
                    {
                        healthArray[7] -= damage;
                        Console.WriteLine($"Oshawott took {damage} damage!");
                        Thread.Sleep(3000);
                        Console.WriteLine();
                    }
                    else
                    {
                        healthArray[8] -= damage;
                        Console.WriteLine($"Snivy took {damage} damage!");
                        Thread.Sleep(3000);
                        Console.WriteLine();
                    }
                }
                else if (enemyMove == 2)
                {
                    EnemyTailWhip(enemy);
                    Thread.Sleep(2000);
                }
            }

            Console.Clear();
        }
        static void enemySnivy(int enemy)
        {
            const int MIN = 1;
            const int MAX = 3;
            Random randomizer = new Random();
            int enemyMove = randomizer.Next(MIN, MAX);
            if (healthArray[13] > 0)
            {
                if (enemyMove == 1)
                {
                    int damage = EnemyVineWhip(enemy);
                    if (starter == 1)
                    {
                        healthArray[9] -= damage;
                        Console.WriteLine($"{pokemonArray[9]} took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else if (starter == 2)
                    {
                        healthArray[7] -= damage;
                        Console.WriteLine($"Oshawott took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                    else
                    {
                        healthArray[8] -= damage;
                        Console.WriteLine($"Snivy took {damage} damage!");
                        Thread.Sleep(2000);
                        Console.WriteLine();
                    }
                }
                else
                {
                    EnemyTailWhip(enemy);
                    Thread.Sleep(3000);
                }
            }
            Console.Clear();
        }
        //PLAYER MOVES:
        static int Ember(int enemy) //Fire attack
        {
            Console.WriteLine($"{pokemonArray[9]} used Ember!");
            int resistance = 3;
            int grassWeakness = 4;
            const int MIN = 11;
            const int MAX = 26;
            Random randomizer = new Random();
            int damage = randomizer.Next(MIN, MAX);
            if (damage > 20)
            {
                Console.WriteLine("Critical Hit!");
                Thread.Sleep(2000);
            }
            if (enemy == 14 || enemy == 1 || enemy == 0 || enemy == 10 || enemy == 12)
            {
                damage -= defenseArray[enemy];
                damage -= resistance;
                Console.WriteLine("It's not very effective...");
                Thread.Sleep(2000);
            }
            else if (enemy == 2 || enemy == 13 || enemy == 3 || enemy == 11)
            {
                damage -= defenseArray[enemy];
                damage += grassWeakness;
                Console.WriteLine("It's super effective!");
                Thread.Sleep(2000);
            }
            else
            {
                damage -= defenseArray[enemy];
            }
            if (damage == 10 || damage == 18 || damage == 12)
            {
                Console.WriteLine($"{pokemonArray[enemy]} was afflicted with burn!");
                Thread.Sleep(2000);
                playerburn = true;
            }
            return damage;
        }
        static void TailWhipPlayer(int enemy) //Lowers the defense of the enemy pokemon.
        {
            if (defenseArray[enemy] == 0)
            {
                Console.WriteLine($"{pokemonArray[enemy]}'s defense can't go any lower!");
            }
            else if (starter == 1)
            {
                Console.WriteLine($"{pokemonArray[9]} used Tail Whip!");
                defenseArray[enemy] -= 2;
                Thread.Sleep(2000);
                Console.WriteLine($"{pokemonArray[enemy]}'s defense fell!");
            }
            else if (starter == 2)
            {
                Console.WriteLine($"{pokemonArray[8]} used Tail Whip!");
                defenseArray[enemy] -= 2;
                Thread.Sleep(2000);
                Console.WriteLine($"{pokemonArray[enemy]}'s defense fell!");
            }
            else
            {
                Console.WriteLine($"{pokemonArray[7]} used Tail Whip!");
                defenseArray[enemy] -= 2;
                Thread.Sleep(2000);
                Console.WriteLine($"{pokemonArray[enemy]}'s defense fell!");
            }

        }
        static int Watergun(int enemy) //Water move
        {
            Console.WriteLine("Oshawott used Water Gun!");
            Thread.Sleep(2000);
            int superEffective = 4;
            int waterResistance = 3;
            const int MIN = 14;
            const int MAX = 26;
            Random randomizer = new Random();
            int damage = randomizer.Next(MIN, MAX);
            if (damage > 22)
            {
                Console.WriteLine("Critical Hit!");
            }
            if (enemy == 15 || enemy == 0 || enemy == 10)
            {
                damage -= defenseArray[enemy];
                damage -= superEffective;
                Console.WriteLine("It's super effective!");
            }
            else if (enemy == 2 || enemy == 3 || enemy == 13 || enemy == 1 || enemy == 14 || enemy == 11 || enemy == 12)
            {
                damage -= defenseArray[enemy];
                damage -= waterResistance;
                Console.WriteLine("It's not very effective...");
            }
            else
            {
                damage -= defenseArray[enemy];
            }
            return damage;
        }
        static int VineWhip(int enemy) //Grass move
        {
            Console.WriteLine("Snivy used Vine Whip!");
            Thread.Sleep(2000);
            int superEffective = 4;
            int grassResistance = 3;
            const int MIN = 14;
            const int MAX = 26;
            Random randomizer = new Random();
            int damage = randomizer.Next(MIN, MAX);
            if (damage > 17)
            {
                Console.WriteLine("Critical Hit!");
            }
            if (enemy == 1 || enemy == 14 || enemy == 12)
            {
                damage -= defenseArray[enemy];
                damage -= superEffective;
                Console.WriteLine("It's super effective!");
            }
            else if (enemy == 15 || enemy == 0 || enemy == 3 || enemy == 2 || enemy == 5 || enemy == 11 || enemy == 10)
            {
                damage -= defenseArray[enemy];
                damage -= grassResistance;
                Console.WriteLine("It's not very effective...");
            }
            else
            {
                damage -= defenseArray[enemy];
            }
            return damage;
        }
        //ENEMY MOVES:
        static int EnemyEmber(int enemy)
        {
            Console.WriteLine($"{pokemonArray[enemy]} used Ember!");
            int resistance = 3;
            int grassWeakness = 4;
            const int MIN = 12;
            const int MAX = 25;
            Random randomizer = new Random();
            int damage = randomizer.Next(MIN, MAX);
            if (damage > 17)
            {
                Console.WriteLine("Critical Hit!");
                Thread.Sleep(2000);
            }
            if (starter == 1 || starter == 2)
            {
                if (starter == 1)
                {
                    damage -= defenseArray[9];
                }
                else
                {
                    damage -= defenseArray[7];
                }
                damage -= resistance;
                Console.WriteLine("It's not very effective...");
                Thread.Sleep(2000);
            }
            else if (starter == 3)
            {
                damage -= defenseArray[8];
                damage += grassWeakness;
                Console.WriteLine("It's super effective!");
                Thread.Sleep(2000);
            }
            if (damage == 10 || damage == 18 || damage == 12)
            {
                if (!burn && starter == 1)
                {
                    Console.WriteLine($"{pokemonArray[9]} was afflicted with burn!");
                    burn = true;
                    Thread.Sleep(2000);
                }
                else if (!burn && starter == 2)
                {
                    Console.WriteLine($"{pokemonArray[7]} was afflicted with burn!");
                    Thread.Sleep(2000);
                    burn = true;
                }
                else
                {
                    Console.WriteLine($"{pokemonArray[8]} was afflicted with burn!");
                    Thread.Sleep(2000);
                    burn = true;
                }
            }
            return damage;
        }
        static int EnemyWaterGun(int enemy)
        {
            Console.WriteLine($"{pokemonArray[enemy]} used Water Gun!");
            int superEffective = 4;
            int waterResistance = 3;
            const int MIN = 14;
            const int MAX = 25;
            Random randomizer = new Random();
            int damage = randomizer.Next(MIN, MAX);
            if (damage > 17)
            {
                Console.WriteLine("Critical Hit!");
            }
            if (starter == 1)
            {
                damage -= defenseArray[9];
                damage -= superEffective;
                Console.WriteLine("It's super effective!");
            }
            else
            {
                if (starter == 2)
                {
                    damage -= defenseArray[7];
                    damage -= waterResistance;
                }
                else
                {
                    damage -= defenseArray[8];
                    damage -= waterResistance;
                }
                Console.WriteLine("It's not very effective...");
            }
            return damage;
        }
        static void EnemyTailWhip(int enemy)
        {
            Console.WriteLine($"{pokemonArray[enemy]} used Tail Whip!");
            if (starter == 1)
            {
                if (defenseArray[9] == 0)
                {
                    Console.WriteLine($"{pokemonArray[9]}'s defense can't go any lower!");
                }
                else
                {
                    Console.WriteLine("Tepig's defense fell!");
                    defenseArray[9] -= 2;
                }
            }
            else if (starter == 2)
            {
                if (defenseArray[7] == 0)
                {
                    Console.WriteLine("Oshawott's defense can't go any lower!");
                }
                else
                {
                    Console.WriteLine("Oshawott's defense fell!");
                    defenseArray[7] -= 2;
                }
            }
            else
            {
                if (defenseArray[8] == 0)
                {
                    Console.WriteLine("Snivy's defense can't go any lower!");
                }
                else
                {
                    Console.WriteLine("Snivy's defense fell!");
                    defenseArray[8] -= 2;
                }
            }
        }
        static int EnemyBulletSeed(int enemy)
        {
            Console.WriteLine($"{pokemonArray[enemy]} used Bullet Seed!");
            int resistance = 3;
            int waterWeakness = 4;
            const int MIN = 1;
            const int MAX = 5;
            Random randomizer = new Random();
            int seeds = randomizer.Next(MIN, MAX);
            int damage = seeds * 7;
            Console.WriteLine($"It hit {seeds} times!");
            if (starter == 1 || starter == 3)
            {
                if (starter == 1)
                {
                    damage -= defenseArray[9];
                }
                else
                {
                    damage -= defenseArray[8];
                }
                damage -= resistance;
                Console.WriteLine("It's not very effective...");
                Thread.Sleep(2000);
            }
            else if (starter == 2)
            {
                damage -= defenseArray[7];
                damage += waterWeakness;
                Console.WriteLine("It's super effective!");
                Thread.Sleep(2000);
            }
            return damage;
        }
        static int EnemyVineWhip(int enemy)
        {
            Console.WriteLine($"{pokemonArray[enemy]} used Vine Whip!");
            Thread.Sleep(2000);
            int superEffective = 4;
            int grassResistance = 3;
            const int MIN = 14;
            const int MAX = 26;
            Random randomizer = new Random();
            int damage = randomizer.Next(MIN, MAX);
            if (damage > 17)
            {
                Console.WriteLine("Critical Hit!");
            }
            if (starter == 2)
            {
                damage -= defenseArray[7];
                damage -= superEffective;
                Console.WriteLine("It's super effective!");
            }
            else if (starter == 1 || starter == 3)
            {
                if (starter == 1)
                {
                    damage -= defenseArray[9];
                    damage -= grassResistance;
                    Console.WriteLine("It's not very effective...");
                }
                else
                {
                    damage -= defenseArray[8];
                    damage -= grassResistance;
                    Console.WriteLine("It's not very effective...");
                }
            }
            return damage;
        }
        static int EnemyGust(int enemy) //Flying type move
        {
            Console.WriteLine($"{pokemonArray[enemy]} used Gust!");
            Thread.Sleep(2000);
            int superEffective = 4;
            const int MIN = 14;
            const int MAX = 24;
            Random randomizer = new Random();
            int damage = randomizer.Next(MIN, MAX);
            if (damage > 16)
            {
                Console.WriteLine("Critical Hit!");
            }
            if (starter == 3)
            {
                damage -= defenseArray[8];
                damage -= superEffective;
                Console.WriteLine("It's super effective!");
            }
            else if (starter == 1 || starter == 2)
            {
                if (starter == 1)
                {
                    damage -= defenseArray[9];
                }
                else
                {
                    damage -= defenseArray[8];
                }
            }
            return damage;
        }
        static int EnemyRazorLeaf(int enemy)
        {
            Console.WriteLine($"{pokemonArray[enemy]} used Razor Leaf!");
            Thread.Sleep(2000);
            int superEffective = 4;
            int grassResistance = 3;
            const int MIN = 14;
            const int MAX = 26;
            Random randomizer = new Random();
            int damage = randomizer.Next(MIN, MAX);
            if (damage > 17)
            {
                Console.WriteLine("Critical Hit!");
            }
            if (starter == 2)
            {
                damage -= defenseArray[7];
                damage -= superEffective;
                Console.WriteLine("It's super effective!");
            }
            else if (starter == 1 || starter == 3)
            {
                if (starter == 1)
                {
                    damage -= defenseArray[9];
                    damage -= grassResistance;
                    Console.WriteLine("It's not very effective...");
                }
                else
                {
                    damage -= defenseArray[8];
                    damage -= grassResistance;
                    Console.WriteLine("It's not very effective...");
                }
            }
            return damage;
        }
        static int EnemyTackle(int enemy) //Normal type move
        {
            Console.WriteLine($"{pokemonArray[enemy]} used Tackle!");
            Thread.Sleep(2000);
            const int MIN = 14;
            const int MAX = 26;
            Random randomizer = new Random();
            int damage = randomizer.Next(MIN, MAX);
            if (damage > 17)
            {
                Console.WriteLine("Critical Hit!");
            }
            switch (starter)
            {
                case 1:
                    damage -= defenseArray[9];
                    break;

                case 2:
                    damage -= defenseArray[7];
                    break;

                case 3:
                    damage -= defenseArray[8];
                    break;
            }
            return damage;
        }
        static int EnemyTailSlap(int enemy) //Normal type move.
        {
            Console.WriteLine($"{pokemonArray[enemy]} used Tail Slap!");
            const int MIN = 1;
            const int MAX = 5;
            Random randomizer = new Random();
            int hits = randomizer.Next(MIN, MAX);
            int damage = hits * 5;
            Console.WriteLine($"It hit {hits} times!");
            switch (starter)
            {
                case 1:
                    damage -= defenseArray[9];
                    break;

                case 2:
                    damage -= defenseArray[7];
                    break;

                case 3:
                    damage -= defenseArray[8];
                    break;
            }
            return damage;
        }
        static void enemyChoice(int enemy) //Chooses which enemy function to run.
        {
            if (enemy == 0)
            {
                Pansear(enemy);
            }
            else if (enemy == 1)
            {
                Panpour(enemy);
            }
            else if (enemy == 2)
            {
                Pansage(enemy);
            }
            else if (enemy == 3)
            {
                Deerling(enemy);
            }
            else if (enemy == 4)
            {
                Mincino(enemy);
            }
            else if (enemy == 5)
            {
                Pidove(enemy);
            }
            else if (enemy == 6)
            {
                Patrat(enemy);
            }
            else if (enemy == 10)
            {
                Simisear(enemy);
            }
            else if (enemy == 11)
            {
                Simisage(enemy);
            }
            else if (enemy == 12)
            {
                SimiPour(enemy);
            }
            else if (enemy == 13)
            {
                enemySnivy(enemy);
            }
            else if (enemy == 14)
            {
                enemyOshawott(enemy);
            }
            else if (enemy == 15)
            {
                enemyTepig(enemy);
            }
        }
        static string Sprites(int enemy) //Finds the sprites for all enemy pokemon and prints them into the Starter Pokemons functions. They're all correlated to their array number. 
        {
            //All sprites were from https://pokemondb.net/pokedex.
            //Used https://asciiart.club/ to turn it into Ascii.
            string sprite = "";
            if (enemy == 0)
            {
                sprite = @"                                             
                                        ,,                                      
                                     ╓▓▀▒█`                                     
                                 ╓╥╥╥▌▒▒░░▓,                                    
                              ,@░░▒▒▒▒▓▒▒▒▒█                                    
                        ╔╣▓▓▓▄▓▒▒▒▒░▒░▓▒▒▒▒░█ ,¿▄æg╗                            
                       |╣▒╖ ╙█▌▒▒▒░▓▀╝░▒▒▒▒µ██▒▒╜╜╜╫▌                           
                       ▐▌╢`  ╙█▄▒▒▒▒▒▒▒▒▒▄▄█▌╨`  ª▒▒█▌                          
                       `█▒m ,▓▒▒▓╢╣▒▒▒╜▀▀░╟▌╢,    ╢▐▌`                          
                         █▒@▓▌ ██▒▒▒╜▐█▀▓▒▒▒▒╢  ▄▓▀▌                            
                          ""╙▀▌─` ,   └▀ ╙▒▒░▒@█╙╬  ]▌                           
                             ""%m╓▄╖   ╓  ╓█▀ ▀▀@   ▓`                           
                                ,║▀▀███Ñ▀▀╙¢ÑÑ╜  ╓▀  ,,@@▓L                     
                             g#"",Æ▀""╙[ , ▐▀""▀NNÑ""   ]▌▒▒▐▌                      
                           ][   ▀▌   ▓@▒Ñ░▓[         ]▒█▀                       
                          j▌   ]█▀ ,▓▒▒░░▒╖░▓w,,,,@@╬▄▀`                        
                            ╟NÑ""   ]▓▓▌""""▀▀▓▓▓█▓██▀""`                           
                                   ╙▀▀`   '▓▓█▌                           
                                     ";
            }
            else if (enemy == 1)
            {
                sprite = @"                                                  
                              ╓╓,Æ▓█▓╓╓╓                                        
                          ,╬╙▓▒,║░ ░░▄,║█▄                                      
                          ▓╖░╙░  ╟▓╜  ╙▓░░▓Γ                                    
                           ╙▀▓▄▄▓╣╣▓▓▓▓█▀▀                                      
                    ,æææ,     ,▓╢╢╢╢▓█▌      ,,,,                               
                   $▒▒╜║▒▓╓,φ▓╜╜▓▓▓▓▓Ñ▓▓W ╓φ╬▒▒▒░▓w                             
                  ▐▌╢   ╟▒▓╝` `▒   ░▒╨╢░░▓▓╨`   ╟▒█                             
                  ▐▌░╢~  ▓ h╜▀N╣╙`╙▒╓▄╖╟░▓╠U   ╓╣▒█                             
                   ▀▓▌, ╓█ ╖, * ╖      ╟╖░▒    ▒▄▀                              
                     ╙█Ñ▄▄█▄ ª,╓KK,,╓H ""▓▓▒╫@@▄@▀                               
                           '╙Vm,,,,,,▄▄╜▀ '▀▀▀                                  
                            ,╖▓▓█▒▒▒▒▀▄                                         
                      ╓m╜""""""▄▓╜▓╙╜╨j█╙Ñ▓╙K#╓,     ╔▓▒@▀▓w                         
                     █   ╓]▌  ╓╙    ▌  ▀▓     ▄ ▐▓░ ╓█▀                         
                     ╙▀▀▀`` ,ƒ  ,   ╘▌,,║▀▀╖▄▄▀▓▄█▀▄▄█                          
                            `%╗Ñ▌""""Ñ,,██Ñ     ""`                                
                            ╙╜▀╜   ╙╥▄▓                                         
                                                   ";
            }
            else if (enemy == 2)
            {
                sprite = @"                                                                                                                                     
                               g███,g██▄                                        
                            ╔█╢▓▓▓▓▓▀▓▓▓█▀▓L                                    
                           ▐█╣▓▓▌╜▓▄▓▀█▓▓▓██                                    
                       ,,   ▀██████▓█▓▓▓▓▓███                                   
                     ▄╢▓▓╢▄╒██▓▓▓▓▓▓▓▓███▓▓█▄▄▄                                 
                    █╫█▀ ██\▄█▓▓▓▓▀▄/▀▓▒█▀╫▓▀▓╢▓▄                               
                    █▓▌ ▄█ █▀`▓▀╜`██▀]▓█▓▓▌   █▓█                               
                    ▀█▓██▀`   ▄   »»  ╙█▓▌   █╢█                                
                      ▀███⌐  ║╙ ▀▀    ╓██▄ █▓▓█`                                
                          ▀∞φ▄`¢▀▒╥╥o▄Å`▀▓▓█▀`  ,                               
                         ╦╕   ▀█▀ ▀▀▀N▄╦       ]▓█▄█▌                           
                      √ ▀▄.└ⁿⁿ ▄▀▌ ╙▀█ⁿ▄  ⁿy   ██║▓▓▓▌                          
                      ╘τ,,,¿∞▀▀ ▐▌   █,,▀   ▀,  █▓█▀`                           
                              ▄█╣▓▓▓█▓▀▄▄▌   █ ███                              
                              ▀█▓█  ▀╢▓╫▀▀█████▀                                
                              ███▀   █▓█∞          ";
            }
            else if (enemy == 3)
            {
                sprite = @"                                                    
                                  ,æ▄                                           
                                 |╣▒▓╖                                          
                               ╓▒ ╙╢╙▒                                          
                              ,▓,,  ╜▀▓,g@""▒▒Hm╖                                
                            ,ƒ    ╟,,▒╜ ▄█▀╜ ,@╜                                
                           ┌╣,*╜,,▄▒``ª╣▄▄▓▀╜`                                  
                         ,▄▓`  ]██ ▓ ]µ ▓                                       
                         ▀█     ▀`╙  ║`█        ,ª╖                             
                           ╙Mµ,╓╛ ,,@╟▌        H ,▀                             
                               ▀▀▀▒▒▐▌    ,╓æ▄▓╖@▓▄                             
                                ▓▒╜╜ ╙▌╖ß▓▒▒╜ `▐█`                              
                               @`           ;  ─▀▄                              
                               ▓       ` ~~`    ╓█                              
                               j▌   ╓     ███▄  ▐                               
                                ╘▓ ]█▌   █▀▓╫█,,╙█                              
                                 ▐U╓▌└ÿ ╓▌  ╟▓█▓╕ █                             
                                ,╟▒▄▀ ▐▌▐█ ▐██▀▐▌▄▀                             
                                ███▀ ╞▌,▓  ""▀""j███                              
                                     ├██▀                                                                                                                       
                                               ";
            }
            else if (enemy == 4)
            {

                sprite = @"             
                                             ,,     
                        ,,                ¿╨` `t                                
                     ,r`  ╙╖            ┬`      ╚U                              
                    @       ▐         ]▒▒╓p▒╖,  ,▓                              
                  ,╟ ░≡╓  ≡▒ ▀µ@`▓   ╟▌    ╓@▓▓H  ▓                             
                  ▐▌  ╓╢▓▄  ,▓  ╓▓  J▌   ]▓▓▒▓╣`  ╙╗                            
                 ╞▌   ╓╜╜▒█ ╟▌ ╓╣`  █   g▀▀ █ ]▌  j▌      ╓                     
                 ╞▌   ╟▓▌▀█@å█▄▐▓@╜▀▀  █▀  $▌ ]▌  j▌     ▌ `@                   
                  ▐▌   ▀█  █▀▓ ▀    ,  ▀ ╓▓▀▀▄▓   █      ╞[  ▌                  
                   ╟╖ ,▓▌▀▄▀       @ ▄   `╓▄▓`   █       ╞▌  ]▌                 
                    ▀▓╢▒▒▓█▒╠      ▓██▌   `  ▄▄Ñ▀        ╟▌   ▐L                
                      ╙▀▓█▐██▌æ  ,  ▀▀  ,▄▄▓▀           j▌    ▐▌                
                          ▓  ╨Ñ▄╖╜    ╓@╣▒▓`           ╓▀    ▐▌                 
                           ▀█N▄▄▄,g@@╢▓▓█▓▀▀█         g╜ ,▄@@@▀▀█               
                             ╓▄█m▀▀╜╜    ,╓▓       ,¢╜  m╜    ╓▓                
                           ▓▒▀▓▄▓t╓H    Ñ▄      ,,@` ª``     ▄▀                 
                            ""▀▓ ╙▀`    ,@▓▄▄▄▄▓▓`  ,,,  ,áN▀""                   
                             ▓▓▓@╖╖╖g▓▓╣╣▓█▒▒╢▓▓▓▄,   ╙▀▄                       
                             $██▓▓█▀█▓▓██▓▌``▀▀▀▀``╙▀▀▀▀▀                       
                              ""▀▀`    ╙▀▀""                      ";
            }
            else if (enemy == 5)
            {
                sprite = @"   
                                       .""ⁿⁿ²╖╓,╫W╖
                                                *▄*
                                   ╓╓⌐    ▒,`   ║
                                 ╓▓▓╢▓▄╖╜ ▐▓ ""  █
                                   `╟▓▓`  ╙*    á█
                                             ,▄▓▀▀▄╖      ,
                                           ╓▓▒╢╢╢▌  ╙W╓▓▓▓▀
                                           ▐╣╢╢╢╣╜   ╟╢▒▓╜
                                 ╘,       ▐▓╣╢╢╢╜   ]▒▒▓`
                                   ╙╓      █▓▓▓▓   ]▓▀
                                      """"%@▄▄▓▀█▄▄Ñ╝""
                                         ]▒▀ █╫▀
                                    ███▄██▀▓▄, ╟▓w
                                      `""▀""▀▓▓▓╝""""""
     ";
            }
            else if (enemy == 6)
            {
                sprite = @" 
                              ╥╖       ╥╖
                            ]╣░▒M▒  `╢╣  ]
                        ,#@@╖▐U.,,;m░,╖▓Ñ▓▓╖         ,
                      ][ ,▒▒█▀▀@pp▄╣╙▄    ▒░▒╫╖   ▓ ╥ ª  ]
                      ]╣ ]▓▀▒ ▀▒``╙╨╖╙`  [  ╙▒╠[ ²`     ]▌,
                       └╣j█  ╚ ╙M╨,          ]╫╛ ╙  ,g@, ª╜
                        `╫╢▓╖  `   ╙`     ,g▄▓╜ÑÑWæg▓▌▓▓▌
                           ▀▒▒▒▓▓▓▄▄▄╖╨ ╙▒▒▒▒▒    ▒▒╢▒▓
                             `╩▄░╜        ╙▒▀▀```▓▀▌``
                                ▌          ] ▒╕,φ▒╝
                                ]┐         ] ╓▐█▒╜
                                ]╣╖       ╓@▒▒▒▓▌
                               ▓▒▒▒@@╖╖╖╓@╣▒▒▒▒▒▀▄
                               ▀▄▒▒▒▄╣╨╨╨╨▄▄▒▒▒▒▒█
                              ≡▀▓▓▀""        `""▓▓█,
                                              ╙`'╙

  ";
            }
            else if (enemy == 10)
            {
                sprite = @"       
                                       ▄▄m
                                     ▐▓╣█
                              ▀▓▓▄   ▌╣╣╢▒▓▄
                               █╢▒▄█▓╣╣▀█╢╢█▄▄▄▓█
                    ,▓▓▓▓▓&, ▄▒╢╢█▒╢╫▓▓▓▓╢▐█▒╢╫▓█ ▄▄▄▓▓▓&,
                   ▄▓╢▓▓▒▒▒▓▓▓▀▓▓╣▒█▓╣╣╢╢╢▒▓╣╢╢▓█▀╣▓▓▓▒▒▓▓▓▄
                   ╙█╫▓▒▒▒▒▒▒█╣╢╢▓▓╢╢╣╣╢▓▓╢╢╢╢▓╣▓▓▒▒▒▒╫╢▓▓▒█
                     ▀▒▓▓▒▒▒▓▒╢▌▐▓▓╢╢╢╢▓ ▓▌▐▓╢╫▓▒▒▒▒▒▒▒▓╢▒▀
                       ▀██╢╢██░▒(`▒░░░░░╦""J╬░╣╢╣▓▓▓╣╢╢██▀`
                             ▀█░Ñ░░░▄╖░░░░▄Å▒▄█▓▓▀▀▀         ▄▓▓▀
                           ,▓╩ ▀▓▓▓█▄▄███▓▓█▓▒▓ ⁿ▀▄        8╢╫▓▌
                  ╔g▓▓▓▓▄  ▐Çτ  `╙▓▀▓▒▒▒╢▓▀""`     █   gg▓▓▀▀█▓█▓
               ▀▄▓▓▓╢╢╢╫▓╣▓⌐╙▄▄▌ ]▄▀░░░░░▒▌,    ╓█  ▄▓▓▀▓▓▓▀█░▓█▓▄
               █▄ƒ▀▄▓▀▓▌▀█╣▒▀╢▓█▀▓░░░░░░░░░█N∞███╢▀▓╢▓███░▀▌░██▒╢█
                 '▀▀▄▌▀'▀▀▀▀██▀ m░░░░░░░░░░░█  ▐████▀▒▒╢▒█▄█▀▒╢╢█▀
                              ╓▓▓▓▓░▄▓▓W░▄▓╖▓╢█└▓╣╢╢╢╢╢╢╢╢╢▓▓▓▄▀-
                              █▓╢╢╢▓╢╢╢╢▓╢▓▓▓▓█▓▓▓▓▓▓╢╢╢▓▓▓▓▓█,
                              ▄█▓▓▓▓▓▓▓▓▓▓▓▓▓▓╢█▌▓▓▓▓▓▓▓█▓▓▓▓▓▌
                             $▓▓▓▓▓`-▐▓╢╢╢▓█╣▓▓▓█▌▓▓▓▌▓▓▓█▓▓▓█▀
                             Å╣╢╢╢█,  ""▀▓▓██▒▓╢██╢▓▓▓▓█▓╢╢╢▓█▀
                              ▄█▓╢╢▓█▄  █▒╢╢▓▓▓▓█╣▓▓▓▓█▀
                           ▐╬▄M░░░▓▓█▌   ▀▀▓░░▓░▓▀█
                            ▀▀Å███▀▀        ▀▀▀▀▀▀

 
";
            }
            else if (enemy == 11)
            {
                sprite = @"     
                                ,
                             ▄█╢▓▄
                            █▓█▓▓▓█,
                           ▐▓▓▓▓▓▓▓█▄▄
                           ▄▓▓▓▓▓▓▓▓██▄
                           █▓▓▓▓▓▓▓▓▓▓▓█
                          ¬█▓▓▓▓▓▓▓▓▓▓▓█▄
                           █▓▓▓▓▓▓▓▓▓▓▓██▄
                           ▐▌▓▓▌░▓▓▓▓▓███▌
                           ▐██▓▓▓▓▓▓▓█████▌     ╔▄▄███▄▄
                 ▄▄▄▄▄▄▄▄ ¬▌▀██████████▀▄██▓██▓▓▓▓▒▒▒▓▓▓█
                █▓▓▒▒▒▓▓▓▓▓█H""████▓█ ` ,▀██▓▓▓▒▒▒▒▒▒▒▓▓█
                ▐█▓▓▄▒▒▒▒▀█▓█▀▄▓███▄▄█▌█░▓▓▓▒▒▒▒██▓▓▓█`  ▄█▄
                  ▀▀███▓▓▒██▓H,▀░▄▄░K,#▒░▄▓▓▓███▀▀▀█▓▓█▓█▓▓▓█
                      '▀▀▀▀██░░▄φ╣▓▄▄▄▓▓░░▓█▀     ▄██▓█▓▓▓▓█▓▌
                     ▌░█  ▀▄⌐╙▓▓▓▄▄▄▄▄▓▀▀ ╓▄█▄▄⌐   ███▓████▀
                ████▀░¢█▄ ▄▄g `╙╫▓▒╢▒▌     µ▄██▓████▓▓█▓▓██▄,,AÇ
               ▄▓▓█░░░@▄██▓█▌▄█▐█▀╙╙░▐▄╟ █▄▐`▀▀▀███▓▓▓▓▓██▒░░░▄▓▄
               `▄▓█▓░╨▓██▀   ▄ ▄▀░░░░░⌠░█         ███▌▓█▀▀╟█▓▓█▄▄▀
                █▀█╢█████  ▐███▌░▄▓█░░▄█▄████      █▓██ ▀▌░█▀▓█▀
                  ▀▀ `▀   ██▓▓█▓██▓▓▓█▓█╢█▓▓█▀    ▄█▓▀   '▀`
                           █▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓█▄▄   ╓█▓█
                         ,▄▓▓▓▓╢▓████████▓▓▓▓▓█▄▄███`
                     ██▄▄█▓▓▓█▀▀   ▀█▓▓██▄██▓▓▓▓██▀
                     ▄█▓▓▓▓█▀         ▀▀████▓▓▓▓╢█▄,
                   ╔▀▀▀▓▓█▀██              ▐▌▀░@▀▀@▄▓r
                   ▀▄█▒▐░░╓▌`               ▀▀▄▄█▄▄▀▀
                      ▀▀▀▀'";
            }
            else if (enemy == 12)
            {
                sprite = @"
                                ╓▄▄▓▓▓▄▄▓▓▌▄
                            ,Æ▓▒▒▒▒▒▒▓▒▓▓▒▒▒▒▒█▄
                           ▄▓▓▓▒▒▒▒▓▓╣▓▓▓▀▀▀▓▓▒▒█
                          ██▒▒╢╢╢╢╢╢╢╢╢╢╢╢╢╢╢╢▓▓▓▓▄
                     J▄▄▄▄█▒╢▒╢▒▓▓▓╢╢╢╢╫▓▓▒▒╢╢╢▐█╢█,,,,
                    █▒▓▀▀█▀╢╢▓▓▒▓╣▒╢╢▒╢╢╢▒▒▓▒╢╢▒██▀╣╣╣╣▀▄
                    ▀▒▓@░█╢╢╢█╢▓\ ╫╝░╙╢╩ ▓▒╢█╢╢╢█▌░░░░▓╢█
                     ▀▓▓░█▒╢╢█╣▓`'▒░░░░æ""▄▓Ñ█╢╢▒█░░@▓▓▒█▀
                 ┌╬▀▄  ╙▀▓█▒╢▓▄░▒▒░▀▒░░▒╓▒░╟▓╢▒█▌▄▄▓╫▄▀
               ,8▓▓▄▀R▓▀██▓█▓▒▒█▄▓M░▀▓▀▀M▄▄▓▒╢▒███▓█▌
               ""▀█m░░░░▐,█▀▒▒▓╢▒█▓▓█▀▀▀█▀██▌╢█▓▀█▓▓▓▌
                ▀▄▓▀░▄▄░░▀▄╢╢▒▄█▀▓░▒▒╜╙╙█▀▓█▒▓╢╢╫█▀
                  ▀▀▀  ▀▀▄▄▀▀▀▒▓█▓╣▓╖╒▓W╥▌,`▀▀██▀█
                           ▀▄▀▄▓▒▒▒╢╢╢╢╢▒▒▐▌ ▄▀▒▀     g▓▓▌▓▓█▄
                             ▄█▄▓▓▒╢╢╢╢╢██▒▀█▒▒█▄▄  ▓▒▒▓╢╢╢╢▓▒▒█
                               J▌░█╢╢╢╢█░░░██╢╢▓▓░█ ▀▀╩▒╢╢▒▒▓▒█▀
                               ▌░░░█╢╣█░░░░▐▄▀▓▄▄▀     █╣╣▓███▀
                              ,▌░░▓▀▀██▀█░░░█▄    ,▄▄█▓▓▓▓▀
                              ▓░░░█  '▀╣▒▓▌░░█▀▀▀▀▒▒▒▓█▀▀
                             ╔▓░░▐       ▀▀▌░╜█▀▀▀
                          ▓▀▓M░░░░█       ▐▌░░░░8P▄
                          ▀▓██Å██▀        █▒░p█░█▌█

 ";
            }
            else if (enemy == 13)
            {
                sprite = @"  
                                ,▄▄▄▄▄,
                       ▐███▄▄▄██▓▓█▀▀▓▓█▄
                        █ ¬▄⌐▀▀▀▀▒▒▄▄▄▒▒▓█,
                         ▓╥     jC█g█▌╟▒▓▓▌      ▄,
                           ▐╖ ▄┐ ░ ▀▀]╫▓▓▌     ▐█▓█
                             ""▀N▄▄   ▐▓▀` ]██ ▄█▓▓▓▌   ▄,
                                ╒▄█╢▒▓▓▒▒æ█▓▓█▓▓▓▓▓▌)█▓▓█
                                ▀▀▀▀▓▐▀╙▄▐█▓▓▓▓▓▓▓▓▓▓▓▓█▀
                              ,██∞∞▓▄█▄ `▀▐▓▓▓█▓▓▓▓▓▓▓▓█
                             ▄█▌   █▓██▄  ╓█▀▒▓█▓▓▓▓▓█▀
                              ▀▀▄  ▀███▓▓▓▓██▓█▀ ▀▀▀
                                ▀╣╥,,▀▓███▓██▀
                                 ╔█▀▓█▓▄█▀▀
                                ╘▀▀▀▐▌░▄▀
                                     '▀
";
            }
            else if (enemy == 14)
            {
                sprite = sprite = @"  
                                ,,,@@@@,,,
                             g▓▓╜`        ╙╜N
                             ▓▓              ▀▓▓[
                            ╔▓ ▄▓╕ ,,,   ╓╗   ▓█▌
                            ▐L ▀█Γ╫▓▓▓▓L ██▌  ▐▌
                            `▓UH╓─ ""║▒` ,▒▒╓╥▒▓`
                              ╙╣▒▒▒▓▒▒▓▒▒▒▒▒╫▓
                           ,ƒ╜╨▓▓▓▓▓▄▒▓▓▓▓▓▓▓▓╖
                            #R▄▄▓╣╢╢▓▓▓▓▓▓▓▓   ╙▓L
                               ]▓▓▓╢▒▒╣▓▓╣╢▓▓▀▀▀▌,
                               ]█▓▓▓▒╟▓▓▓╣▓▓█▄█▓▓▓▓▄
                                ▐█▓▓▓▓▓▓▓▓▓██▓▓▀██▓▀
                               ▄▓▓▓▓▓▓▓▓▓██▓▄
                               ╙▀▀▀╜    ▀▓███ 
                      
";
            }
            else if (enemy == 15)
            {
                sprite = @"       
                                     g╖
                                    ]███▄    ▄▄╖
                                    █████▄▄████▌
                                   ███████████▀
                                   ██████████▀
                             ,,▄█████████▀╙     g▄▄,
                            ▐█▓███████▓▒▓╖     ╟▓▓▓▌
                           ▐▌▒╢▓█▌Ñ ▀╢╢╢╢╫▓   ▄█▓▓█
                         ,▄▓▒▒▒▓███ ]▓╢╢╢╢╢▓▄µ▓██▀
                        ╫▓▓▓▓▓▓▒▒╢▓╓╣╢╢╢╢╢╢╢▒▓██▌
                        ╙▀▓▓▓▓▒╢╢▒▒▓╣╢╢╢╢╢╢╢╢╢▓██L
                           ╙▀▓▓▓▓▓▒▒╢╢▒╢╢╢╢╣╢╫███[
                               ▀▓▓▓▓▓▓▓▒╢╫▓▓▓██▓█[
                               ]█╣▓▓▓▓╢╢╢▓█▓█▓▓▓▌
                                ╙██▀╙▓▓▓▓▀   ▀▀
";
            }
            return sprite;
        }

        static void StarterChoice() //This function is used to choose and name your starter, I also made the trainer essentially react to whatever choice you made by choosing whatever is "super effective" against it.
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($@"
1.Tepig (Fire Type)
The Fire Pig Pokemon
{Sprites(15)}
");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($@"
2.Oshawott (Water Type)
The Sea Otter Pokemon
{Sprites(14)}

");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($@"
3.Snivy (Grass Type)
The Grass Snake Pokemon
{Sprites(13)}
");
            starter = Parse();
            Console.ForegroundColor = ConsoleColor.White;
            switch (starter)
            {
                case 1:
                    Console.Clear();
                    StarterName();
                    Console.WriteLine("Trainer 1: I won't let you just steal a Pokemon! Fight me in a battle right now, Oshawott go!");
                    Thread.Sleep(5000);
                    Console.Clear();
                    Tepig();
                    break;

                case 2:
                    Console.Clear();
                    StarterName();
                    Console.WriteLine("Trainer 1: I won't let you just steal a Pokemon! Fight me in a battle right now, Snivy go!");
                    Thread.Sleep(5000);
                    Console.Clear();
                    Oshawott();
                    break;

                case 3:
                    Console.Clear();
                    StarterName();
                    Console.WriteLine("Trainer 1: I won't let you just steal a Pokemon! Fight me in a battle right now, Tepig go!");
                    Thread.Sleep(5000);
                    Console.Clear();
                    Snivy();
                    break;
            }

        }
        static void StarterName() //Used to name your pokemon, I thought this was a neat feature to put in.
        {
            string input;
            do
            {
                Console.WriteLine("Would you like to name your starter?[Y/N]");
                input = Console.ReadLine();
                if (input != "y" && input != "n")
                {
                    Console.WriteLine("Invalid Input.");
                }
            }
            while (input != "y" && input != "n");

            if (input.ToLower() == "y")
            {
                if (starter == 1)
                {
                    Console.WriteLine("Please enter the name:");
                    pokemonArray[9] = Console.ReadLine();
                }
                else if (starter == 2)
                {
                    Console.WriteLine("Please enter the name:");
                    pokemonArray[8] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Please enter the name:");
                    pokemonArray[7] = Console.ReadLine();
                }
            }
            else
            {
                Console.Clear();
            }
        }
        static void Movement()
        {
            bool starterPicked = true;
            Console.CursorVisible = false;
            int x = 0; int y = 0;
            char player = 'X';
            Map();
            //Sets position to middle of the screen
            Console.CursorLeft = Console.WindowWidth / 2;
            Console.CursorTop = Console.WindowHeight / 2;
            //x and y essentially contain info on where the cursor is at all times, this helps with making encounter battles work.
            x = Console.CursorLeft;
            y = Console.CursorTop;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(player);
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (Console.CursorTop <= 5)
                    {
                        continue;
                    }
                    else
                    {
                        Position(x, y);
                        y -= 1;

                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (Console.CursorTop >= 28)
                    {
                        continue;
                    }
                    else
                    {
                        Position(x, y);
                        y += 1;
                    }
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {

                    if (Console.CursorLeft <= 27)
                    {
                        continue;
                    }
                    else
                    {
                        Position(x, y);
                        x -= 1;

                    }
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {

                    if (Console.CursorLeft >= 70)
                    {
                        continue;
                    }
                    else
                    {
                        Position(x, y);
                        x += 1;

                    }
                }
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(player);

                if (Console.CursorLeft == 41 && Console.CursorTop == 15 && counterMovement == 0) //Once the green X is hit starterPicked will become false ending the loop leading to all the functions that form the beginning of the game.
                {
                    starterPicked = false;
                }
                else if (x == randomPositionX && y == randomPositionY) //This else if statement is used to put the player into a battle whenever they touch the Red X on the map.
                {
                    if (starter == 1)
                    {
                        Console.Clear();
                        Tepig();
                    }
                    else if (starter == 2)
                    {
                        Console.Clear();
                        Oshawott();
                    }
                    else
                    {
                        Console.Clear();
                        Snivy();
                    }
                }
            }
            while (starterPicked);

            if (counterMovement == 0) //This is to make sure that this only ever plays out once.
            {
                counterMovement++;
                Console.Clear();
                StarterDialogue();
                StarterChoice();
                starterPicked = true;
                Console.Clear();
            }
            Movement(); //Sends the player back to the map after completing a battle. 
        }
        static void Map()
        {
            Console.CursorTop = Console.WindowHeight - 26; //By setting the position of my map like this it's much easier to figure out where my borders should be.
            Console.CursorLeft = 27;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(@"---------------------------------------------
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         |                                              |
                         
");
            Console.CursorTop = Console.WindowHeight - 1;
            Console.CursorLeft = 27;
            Console.Write("---------------------------------------------");
            Console.CursorTop = Console.WindowHeight;
            Console.CursorLeft = 39;
            Console.Write($"Your current score:{totalPoints}");
            if (counterMap == 0) //This is to make sure that the green X that appears for the Proffesor's lab only ever appears once at the beginning of the game. 
            {
                Console.CursorLeft = 40;
                Console.CursorTop = 15;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("X");
                counterMap++;
            }
            else if (counterMap == 1)
            {
                const int MINXAXIS = 28;
                const int MAXXAXIS = 69;
                Random randomizerX = new Random();
                randomPositionX = randomizerX.Next(MINXAXIS, MAXXAXIS);
                const int MINYAXIS = 11;
                const int MAXYAXIS = 27;
                Random randomizerY = new Random();
                randomPositionY = randomizerY.Next(MINYAXIS, MAXYAXIS);
                Console.CursorLeft = randomPositionX;
                Console.CursorTop = randomPositionY;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("X");
            }
        }
        static void Position(int xAxis, int yAxis)
        {
            Console.SetCursorPosition(xAxis, yAxis);
            Console.Write(' ');
        }
        static void StartDialogue() //Dialogue that plays at the beginning of the game more or less telling you how it works. 
        {
            Console.WriteLine("*Communication Commence*");
            Console.WriteLine("");
            Thread.Sleep(2000);
            Console.WriteLine("The Boss: So your the new grunt, eh?");
            Console.WriteLine("");
            Thread.Sleep(4000);
            Console.WriteLine("The Boss: As I'm sure you already know as Team Plasma our goal is to catch other people's pokemon for profit and power.");
            Console.WriteLine("");
            Thread.Sleep(8000);
            Console.WriteLine("The Boss: As such, your mission is to catch other trainer's Pokemon in Route 1 using our specially made Pokeballs. You're to send these Pokemon straight to me through the PC.");
            Console.WriteLine("");
            Thread.Sleep(8000);
            Console.WriteLine("The Boss: DO NOT, use them in battle.");
            Console.WriteLine("");
            Thread.Sleep(4000);
            Console.WriteLine("The Boss: In exchange, you will receive your pay for each Pokemon.");
            Console.WriteLine("");
            Thread.Sleep(4000);
            Console.WriteLine("The Boss: Got all that?");
            Console.WriteLine("");
            Thread.Sleep(3000);
            Console.WriteLine("The Boss: Good.");
            Console.WriteLine("");
            Thread.Sleep(3000);
            Console.WriteLine("The Boss: The first thing you have to do is invade the Pokemon Profesor's lab and steal one of the starter Pokemon from the new trainers.");
            Console.WriteLine("");
            Thread.Sleep(8000);
            Console.WriteLine("The Boss: What? Being cruel is just another part of the job.");
            Console.WriteLine("");
            Thread.Sleep(6000);
            Console.WriteLine("The Boss: I've marked the Professor's lab on your map with a Green [X], I'll get back to you once you've succesfully gotten your Pokemon.");
            Console.WriteLine("");
            Thread.Sleep(8000);
            Console.WriteLine("*Communication End*");
            Thread.Sleep(3000);
            Console.Clear();


        }
        static void InitialDialogue() //Dialogue that plays in the initial start of the game.
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("The Boss: Did you succeed in getting one of the starters?");
            Console.WriteLine("");
            Thread.Sleep(5000);
            Console.WriteLine("The Boss: Good.");
            Console.WriteLine("");
            Thread.Sleep(3000);
            Console.WriteLine("The Boss: Well then, I won't keep you, you know your mission. Everytime a trainer shows up on Route 1 they will be marked with a Red [X].");
            Console.WriteLine("");
            Thread.Sleep(7000);
            Console.WriteLine("The Boss:Though be very careful, after you defeat 5 trainers a stronger trainer might try to challenge you!");
            Console.WriteLine("");
            Thread.Sleep(5000);
            Console.WriteLine("The Boss: Also, everytime I receive a Pokemon from you or you faint a Pokemon, I will fully heal your Pokemon remotely.");
            Console.WriteLine("");
            Thread.Sleep(7000);
            Console.WriteLine("The Boss: Yes, I know, I'm extremely generous.");
            Console.WriteLine("");
            Thread.Sleep(4000);
            Console.WriteLine("The Boss: Good luck, newbie.");
            Console.WriteLine("");
            Thread.Sleep(4000);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void StarterDialogue() //Dialogue that plays when going to get your starter pokemon.
        {
            Console.WriteLine("*You run into the Professor's lab*");
            Console.WriteLine("");
            Thread.Sleep(4000);
            Console.WriteLine("*You see three trainers, the three starters Pokeballs are still on a table.*");
            Console.WriteLine("");
            Thread.Sleep(4000);
            Console.WriteLine("*You make a mad dash past the trainers and the professor, you reach out for one the Pokeballs.*");
            Console.WriteLine("");
            Thread.Sleep(6000);
            Console.WriteLine("Proffesor: Hey! Who are you!?");
            Console.WriteLine("");
            Thread.Sleep(4000);
            Console.WriteLine("Trainer: Hes stealing one of the starters!");
            Thread.Sleep(4000);
            Console.Clear();

        }
        static void Reset(int enemy) //This function is used to essentially reset all health from both the player's pokemon and the enemy pokemon.
        {

            switch (starter)
            {
                case 1:

                    defenseArray[9] = 6;
                    healthArray[9] = 45;
                    defenseArray[enemy] = 6;
                    healthArray[enemy] += totalDamage; //As some pokemon have 36 hp by doing this it guarantees they will be healed the exact amount of hp.
                    battleCounter++;
                    playerburn = false;
                    break;

                case 2:
                    defenseArray[7] = 6;
                    healthArray[7] = 45;
                    defenseArray[enemy] = 6;
                    healthArray[enemy] += totalDamage;
                    battleCounter++;
                    playerburn = false;
                    break;
                case 3:
                    defenseArray[8] = 6;
                    healthArray[8] = 45;
                    defenseArray[enemy] = 6;
                    healthArray[enemy] = 45;
                    battleCounter++;
                    playerburn = false;
                    break;
            }
        }
        static void Start()
        {

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(@"               
                                            ▄▓▓▒▓▓▄
                              ,,▄▄▄▓▄     ▄▓▓▒▓▓▓▀'       ╓▄╖▄▄▄
     ,▄▓▓▓▓▓▓▓▓▓▓▓▄µ     ▄▄▓▓▓▓▓▓▓▓▓▓▓▓µ ▀█▓▓▓▓▓▄▄,]█▓▓▓▓▓▓▓╢▓▓▓     ▄▓▓▄▄╖,,
  ▄▓▓▓▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓    ▓▓▓▓▒▒▒▓▓▓▒▒▒▓▓▓▓▓▒▒▒▒▒╢▓▓▓▓▓▒▒▒▒▓▓▒▒╫▓▌    ▓▓▓▓▒▓▓▓▓▓▓▓
 ▀█▓▓▒▒▒▒▒▒▒▒▒▓▓▒╣▒▓▓▓   ▀▀█▓▌▒▒╢▒▒▒▒▒▓▓▓▒▒▓▀▀▓▒▒▓▓▓▓▓▒▒▒▒▓▒▒▒▒▓▓,▄▄▄▓▓▓▓▒▒▒╫▓▓▓▓▓
  ▀▓▓▓▓▓▒▒▒▒▒▓▓""▓▓▒▒▓▓▓▓▓▓▓▓▓▓▒▒▒▒▒▒▓▓`▓▌▒╫▓▄▓▒▓▓▓▓▓▓▌▒▒▒▒▒▒▒▒▒▓▓▓▓▓▒▒╢▓▓▓▒▒▒▓▓▓
   ╙▓▓▓▓▓▒▒▒▒▒▓▓▓▒▒▓▓▒▒▓▓▒▒▒╫▓▓▒▒▒▒▒▓▓▓▓▓▒▒▒▒▒▒▒▒▒▒▓▓▓▒▒▓▒▒▒▒▒▓▓▒╫▓▓▓▒▓╢▓▓▒▒▒╫▓▒▓▓
      ╙▓▓▓▒▒▒▒▒▒▒▓▓▓▒▒╫▓▓▓▓▓▒╫▓▒▒▓▓▒▒▒▒╫▓▓▓▓▒▒▒▓▓▓▓▓▓▒▒▒▓▓▒▓▓╫▓▌▒▒╫▓▓▓▒▒▓▓▒▓▒▒▒▒▓▓
       ╚█▓▓▒▒▒▒▓▓▓▓▓▒▒▒▒▒▒▒▒▒▓▓▒▒▓▓█▓▓▓▓▒▒▒╢▓▓▓▀▀▐█▓▓▓▓▓▓▓▓▓▓╣▓▓▒▒▒▒▒▒▒▓▓▒▒▓▒▒▒▒▓▓
        ▐█▓▓▒▒▒╫▓▓██▓▓▒▒╢▒▒▓▓▓▌▒▒▓▓  ▀▀█▓▓▓▓▓▓U  `▀▀▀▀▀▓▀▓▓█▓▓▒╫▓▓▓▓▓▓▓▓▒▒╫▓▒▒▒▒▓▓
         ▐█▓▓▒▒▒▓▓▌▀██▓▓▓▓▀▀█▓▓▓▓▓▀      ▀▀█▓▓U           ╘▀▓▓▓▓▓▓▓  ▓▓▓▓▓▓▓▒▒▒▓▓▓
          ▐█▓▓▒▒▓▓▓                                              -      -▓█▓▓▓▓▓
           ▀█▓▓▀▀""                                                        `▀▀▀▓▀

            :::::::::  :::            :::      ::::::::    :::   :::       :::  
           :+:    :+: :+:          :+: :+:   :+:    :+:  :+:+: :+:+:    :+: :+: 
          +:+    +:+ +:+         +:+   +:+  +:+        +:+ +:+:+ +:+  +:+   +:+ 
         +#++:++#+  +#+        +#++:++#++: +#++:++#++ +#+  +:+  +#+ +#++:++#++: 
        +#+        +#+        +#+     +#+        +#+ +#+       +#+ +#+     +#+  
       #+#        #+#        #+#     #+# #+#    #+# #+#       #+# #+#     #+#   
      ###        ########## ###     ###  ########  ###       ### ###     ###    
                                                      
");

            Console.WriteLine(@"
1.Start
2.Controls And Typings
3.Exit
");
            int selection = Parse(); //I would use Read Key here but I just wanted to show that I can make a try.Parse function.
            Console.Clear();

            switch (selection)
            {
                case 1:
                    ConsoleKeyInfo key;
                    Console.Clear();
                    Console.WriteLine("Would you like to skip beginning dialogue?[Y/N]");
                    key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.N)
                    {
                        Console.Clear();
                        StartDialogue();
                        Console.Clear();
                        Movement();
                    }
                    else
                    {
                        Console.Clear();
                        Movement();
                    }
                    break;

                case 2:

                    do
                    {
                        Console.Clear();
                        Console.WriteLine(@"
Controls:
Up:Up Arrow
Down:Down Arrow
Left: Left Arrow
Right: Right Arrow

In Battle Select Option Using Numbers Provided.

Type Weaknesses:
Grass<Fire
Grass>Water
Grass<Flying
Water>Fire
Normal=Neutral to all typings.
Grass<Grass not very effective against each other
Water<Water not very effective against each other
Fire<Fire not very effective against each other

Back?[Y]

");

                        key = Console.ReadKey(true);

                    }
                    while (key.Key != ConsoleKey.Y);
                    Console.Clear();
                    Start();
                    break;

                case 3:
                    Console.WriteLine("Thanks for playing!");
                    break;
            }

        }
    }
}