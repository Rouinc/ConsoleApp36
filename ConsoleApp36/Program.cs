using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_project_AP
{
    public class Player
    {
        Random rand = new Random();

        public string name;
        public int coins = 300;
        public int health = 10;
        public int damage = 1;
        public int armorvalue = 0;
        public int potion = 5;
        public int weaponvalue = 2;

        public int mods = 0;

        public enum PlayerClass { Mage, Archer, Warrior };
        public PlayerClass currentClass = PlayerClass.Warrior;

        public int GetHealth()
        {
            int upper = (2 * mods + 5);
            int lower = (mods + 2);
            return rand.Next(lower, upper);
        }
        public int GetPower()
        {
            int upper = (2 * mods + 2);
            int lower = (mods + 1);
            return rand.Next(lower, upper);
        }
    }

    public class Program
    {
        public static Player currentPlayer = new Player();
        public static bool mainLoop = true;
        static void Main(string[] args)
        {
            Start();
            Encounters.FirstEncounter();
            while (mainLoop)
            {
                Encounters.RandomEncounter();
            }
        }
        private static bool random;
        private static string name;
        private static int power;
        public static int health;
        public static int potionV;
        public static int mage;
        public static int warrior;
        private static void Start(bool power, bool name)
        {
            throw new NotImplementedException();
        }

        static void Start()
        {
            Player p = new Player();
            Console.WriteLine("Forest of Death");
            Console.WriteLine("Name: ");
            currentPlayer.name = Console.ReadLine();
            Console.WriteLine("Class: Mage  Archer  Warrior");
            bool flag = false;
            while (flag == false)
            {
                mage = 0;
                warrior = 0;
                flag = true;
                string input = Console.ReadLine().ToLower();
                if (input == "mage")
                {
                    p.currentClass = Player.PlayerClass.Mage;
                    mage = 1;
                }
                else if (input == "archer")
                {
                    p.currentClass = Player.PlayerClass.Archer;
                }
                else if (input == "warrior")
                {
                    p.currentClass = Player.PlayerClass.Warrior;
                    warrior = 1;
                }
                else
                {
                    Console.WriteLine("Please choose an existing class!!");
                    flag = false;
                }
            }
            Console.WriteLine("You awake in a cold, dark forest. You are confused and are having trouble remembering...");
            Console.WriteLine("Anything about how do you get into this dark forbibben forest!!!");
            Console.WriteLine("In this forest no one has live to tell the tale.....");
            if (currentPlayer.name == "")
                Console.WriteLine("You can't even remember name...");
            else
                Console.WriteLine("You know your name is " + currentPlayer.name);
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("You grope around in the darkness of the forest until you find an old house. You feel some resistance as...");
            Console.WriteLine("You turn the handle, but the rusty lock breaks with little effort. You feel something is not right about this house!");
            Console.WriteLine("You will a clue about this forbidden forest name called the FOREST OF DEATH!!!");
        }
        class Encounters
        {
            static Random rand = new Random();

            public static void FirstEncounter()
            {
                Console.WriteLine("You look around the old house to find some weapon and find a sword in old house...");
                Console.WriteLine();
                Console.ReadKey();
                Combat(false, "Human Rogue", 3, 7);
            }
            public static void BasicFightEncounter()
            {
                Console.Clear();
                Console.WriteLine("You turn the corner and there you see a hulking beast looks at you...");
                Console.ReadKey();
                Combat(true, "", 0, 0);
            }
            public static void WizardEncounter()
            {
                Console.Clear();
                Console.WriteLine("The door slowly creaks open as you explore the house most creepy room. You see a tall man with a");
                Console.WriteLine("long beard looking at a large tome looking at you.....");
                Console.ReadKey();
                Combat(false, "Dark Wizard", 4, 2);
            }
            // encounter tools
            public static void RandomEncounter()
            {
                switch (rand.Next(0, 1))
                {
                    case 0:
                        BasicFightEncounter();
                        break;
                    case 1:
                        WizardEncounter();
                        break;
                }
            }
            public static void Combat(bool random, string name, int power, int health)
            {
                string n = "";
                int p = 0;
                int h = 0;
                if (random)
                {
                    n = getName();
                    p = Program.currentPlayer.GetPower();
                    h = Program.currentPlayer.GetHealth();
                }
                else
                {
                    n = name;
                    p = power;
                    h = health;
                }
                while (h > 0)
                {
                    Console.Clear();
                    Console.WriteLine(n);
                    Console.WriteLine(p + "/" + h);
                    Console.WriteLine("=====================");
                    Console.WriteLine("| (A)ttack (D)efend |");
                    Console.WriteLine("| (R)un    (H)eal   |");
                    Console.WriteLine("=====================");
                    Console.WriteLine("potions: " + Program.currentPlayer.potion + " Health: " + Program.currentPlayer.health);
                    string input = Console.ReadLine();
                    if (input.ToLower() == "a" || input.ToLower() == "attack")
                    {
                        //Attack
                        Console.WriteLine("How dare you attack me, Die you foul creature! As you pass, the " + n + " strikes you back ");
                        int damage = p - Program.currentPlayer.armorvalue;
                        if (damage < 0)
                            damage = 0;
                        int attack = rand.Next(0, currentPlayer.weaponvalue) + rand.Next(1, 4) + ((warrior == 1) ? 2 : 0);
                        Console.WriteLine("You lose " + damage + "health and deal " + attack + " damage");
                        Program.currentPlayer.health -= damage;
                        h -= attack;
                    }
                    else if (input.ToLower() == "d" || input.ToLower() == "defend")
                    {
                        //Defend
                        Console.WriteLine("As the " + n + "its impossible to hurt me with your attacks!");
                        int damage = (p / 4) - Program.currentPlayer.armorvalue;
                        if (damage < 0)
                            damage = 0;
                        int attack = rand.Next(0, currentPlayer.weaponvalue) / 2;
                        Console.WriteLine("You lose " + damage + "health and deal " + attack + " damage");
                        Program.currentPlayer.health -= damage;
                        h -= attack;
                    }
                    else if (input.ToLower() == "r" || input.ToLower() == "run")
                    {
                        //Run
                        if (Program.currentPlayer.currentClass != Player.PlayerClass.Archer && rand.Next(0, 2) == 0)
                        {
                            Console.WriteLine("When you sprint aways from the " + n + ", Its strike cathes you in the back, now is the perfect time to run!");
                            int damage = p - Program.currentPlayer.armorvalue;
                            if (damage < 0)
                                damage = 0;
                            Console.WriteLine("You lose " + damage + "health and are unable to escape.");
                            Console.ReadKey();
                            Shop.LoadShop(Program.currentPlayer);
                        }
                        else
                        {
                            Console.WriteLine("You use your agile ninja moves to evade the " + n + " and you succesfully escape!");

                        }
                    }
                    else if (input.ToLower() == "h" || input.ToLower() == "heal")
                    {
                        //Heal
                        if (Program.currentPlayer.potion == 0)
                        {
                            Console.WriteLine("As you desperately grasp for a potion in your bag, all that you can do is using Performanace of a 1000 healing priestess!");
                            int damage = p - Program.currentPlayer.armorvalue;
                            if (damage < 0)
                                damage = 0;
                            Console.WriteLine("The " + n + " strikes you with a heavy hit and you lose" + damage + " health!");
                        }
                        else
                        {
                            Console.WriteLine("You can reach into your bag and pull out a glowing , black flask. You take a long drink.");
                            int potionV = 5;
                            if (mage == 1)
                            {
                                potionV += 4;
                            }
                            Console.WriteLine(value: $"You obtain {potionV} health");
                            Program.currentPlayer.health += potionV;
                            Program.currentPlayer.potion--;
                            Console.WriteLine("As you are occupied, the " + n + " advanced and struck.");
                            int damage = (p / 2) - Program.currentPlayer.armorvalue;
                            if (damage < 0)
                                damage = 0;
                            Console.WriteLine("You lose " + damage + "health.");
                        }
                        Console.ReadKey();
                    }
                    if (Program.currentPlayer.health <= 0)
                    {
                        //Death code
                        Console.WriteLine("As the " + n + " stands tall and comes down to strike. You have been slayn by the mighty " + n);
                        Console.ReadKey();
                        System.Environment.Exit(0);

                    }
                    Console.ReadKey();
                }
                int c = rand.Next(10, 50);
                Console.WriteLine("As you stand victorious over the " + n + ", its body dissolves into " + c + " gold coins!");
                Program.currentPlayer.coins += c;
                Console.ReadKey();
            }
            public static string getName()
            {
                switch (rand.Next(0, 4))
                {
                    case 0:
                        return "Skeleton";
                    case 1:
                        return "Zombie";
                    case 2:
                        return "Human Cultist";
                    case 3:
                        return "Grave RObber";
                }
                return "Human Rogue";
            }
        }
    }

    public class Shop
    {
        static int armorMod;
        static int weaponMod;
        static int difMod;

        public static void LoadShop(Player p)
        {
            armorMod = p.armorvalue;
            weaponMod = p.weaponvalue;
            difMod = p.mods;

            RunShop(p);
        }

        public static void RunShop(Player p)
        {
            int potionP;
            int armorP;
            int weaponP;
            int difP;

            while (true)
            {
                potionP = 20 + 10 * p.mods;
                armorP = 100 * (p.armorvalue + 1);
                weaponP = 100 * p.weaponvalue;
                difP = 300 + 100 * p.mods;
                Console.WriteLine("~ ~ ~ ~ Shop ~ ~ ~ ~ ~");
                Console.WriteLine("======================");
                Console.WriteLine("| (W)eapon         :$ " + weaponP);
                Console.WriteLine("| (A)rmor          :$ " + armorP);
                Console.WriteLine("| (P)otions        :$ " + potionP);
                Console.WriteLine("| (D)ifficulty Mod :$ " + difP);
                Console.WriteLine("======================");
                Console.WriteLine("(E)xit");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(p.name + "'s Stats ");
                Console.WriteLine("======================");
                Console.WriteLine("| Current health : " + p.health);
                Console.WriteLine("| Coins : " + p.coins);
                Console.WriteLine("| Weapon strength: " + p.weaponvalue);
                Console.WriteLine("| Armor toughness: " + p.armorvalue);
                Console.WriteLine("| Potions        : " + p.potion);
                Console.WriteLine("| Difficulty Mod : " + p.mods);
                Console.WriteLine("======================");

                //wait for the input 
                string input = Console.ReadLine().ToLower();
                if (input == "p" || input == "potion")
                {
                    TryBuy("potion", potionP, p);
                }
                else if (input == "w" || input == "weapon")
                {
                    TryBuy("weapon", weaponP, p);
                }
                else if (input == "a" || input == "armor")
                {
                    TryBuy("armor", armorP, p);
                }
                else if (input == "d" || input == "difficulty mod")
                {
                    TryBuy("difficulty mod", difP, p);
                }
                else if (input == "e" || input == "exit")
                    break;
            }
        }
        static void TryBuy(string item, int cost, Player P)
        {
            if (P.coins >= cost)
            {
                if (item == "potion")
                    P.potion++;
                else if (item == "weapon")
                    P.weaponvalue++;
                else if (item == "armor")
                    P.armorvalue++;
                else if (item == "difficulty mod")
                    P.mods++;

                P.coins -= cost;
            }
            else
            {
                Console.WriteLine("You don't have enough gold!");
                Console.ReadKey();
            }
        }
    }
}
