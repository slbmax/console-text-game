using static System.Console;
using static System.Threading.Thread;
using coursework.src.Entities.Players;
using coursework.src.Entities.Enemies;
using coursework.src.Locations;
using coursework.src.Shop;
using coursework.src.Quests;

namespace coursework.src
{
    public static class Game
    {
        private static Player _player;
        private static Location _location;
        private static Quest _quests;
        public static void StartGame()
        {
            WriteLine("[---------------WarMax---------------]");
            WriteLine("Welcome, stranger!");
            Sleep(2500);
            WriteLine("This is a WarMax - text role-playing game");
            Sleep(3000);
            WriteLine("Here you can fight against different mobs, complete various quests and boost your character");
            Sleep(4000);
            WriteLine("You can find more information about WarMax later, now lets create your character");
            Sleep(3000);
            string nickName;
            while(true)
            {
                WriteLine("\nFirstly, enter your nickname:");
                nickName = ReadLine();
                if(nickName != "" && nickName != null && nickName.Replace(" ","")!="")
                {
                    break;
                }
                InvalidOption("Enter valid name (not null)");
            }
            WriteLine($"\nGreat, {nickName}!");
            WriteLine("\nSecondly, choose your character:");
            while(true)
            {
                WriteLine("\n1. Magician - a bit frail but can cast serious things");
                WriteLine("2. Warrior - аll brawn and no brains");
                WriteLine("3. Witch - try not to throw 100500 poitions");
                switch(ReadLine())
                {
                    case "1":
                        _player = new Magician();
                        break;
                    case "2":
                        _player = new Warrior();
                        break;
                    case "3":
                        _player = new Witch();
                        break;
                    default:
                        InvalidOption("Try again");
                        continue;
                }
                break;
            }
            _player.nickName = nickName;
            _quests = new Quest(_player);
            WriteLine("\nGreat choise!");
            Sleep(1000);
            WriteLine("Now you can explore this fantastic world!");
            WriteLine("[---------------WarMax---------------]");
            Sleep(1500);
            GameChoise();
        }
        private static void GameChoise()
        {
            while(true)
            {
                WriteLine($"\n[---------------Home---------------]");
                WriteLine("What do you want to do?");
                WriteLine("1. View my stats");
                WriteLine("2. Vizit a shop");
                WriteLine("3. Check quests");
                WriteLine("4. Explore the world");
                WriteLine("5. Read about WarMax");
                WriteLine("6. Exit game");
                WriteLine("[---------------Home---------------]");
                switch(ReadLine())
                {
                    case "1":
                        _player.ShowInfo();
                        WriteLine("\nPress any button to exit");
                        ReadKey();
                        break;
                    case "2":
                        EnterShop();
                        break;
                    case "3":
                        CheckQuests();
                        break;
                    case "4":
                        _location = ChooseLocation();
                        if(_location == null)
                        {
                            break;
                        }
                        ExploreLocation();
                        break;
                    case "5":
                        string info = GameInfo();
                        WriteLine();
                        WriteLine(info);
                        WriteLine("\nPress any button to return");
                        ReadKey();
                        break;
                    case "6":
                        WriteLine("\nDo you want to exit? {yes/no}");
                        string input = ReadLine();
                        if(input == "y" || input == "yes")
                        {
                            WriteLine($"Goodbye, {_player.nickName}!");
                            return;
                        }
                        break;
                    default:
                        InvalidOption("Try again");
                        continue;
                }
            }
        }
        private static string GameInfo()
        {
            string filePath = @".\data\info.txt";
            return File.ReadAllText(filePath);
        }
        private static void EnterShop()
        {
            IShop proxy = new ProxyShop();
            
            WriteLine("\n[---------------Shop---------------]");
            WriteLine("Welcome to the MaxShop!");
            while(true)
            {
                WriteLine("\nWhat do you want to buy?");
                WriteLine($"1. Big healing potion    [{proxy.bigHealingPotionPrice}] coins [2d-level character or higher]");
                WriteLine($"2. Small healing potion  [{proxy.smallHealingPotionPrice}] coins [1st-level character or higher]");
                WriteLine($"3. Big rage potion       [{proxy.bigRagePotionPrice}] coins [3th-level character or higher]");
                WriteLine($"4. Small rage potion     [{proxy.smallRagePotionPrice}] coins [2th-level character or higher]");
                WriteLine($"5. Big mana potion       [{proxy.bigManaPotionPrice}] coins [4th-level character or higher]");
                WriteLine($"6. Small mana potion     [{proxy.smallManaPotionPrice}] coins [3th-level character or higher]");
                WriteLine($"7. MYSTERIOUS potion     [{proxy.mysteriousPotionPrice}] coins [4d-level character or higher]");
                WriteLine("8. Check balance");
                WriteLine("9. Exit shop");
                WriteLine("[---------------Shop---------------]");
                switch(ReadLine())
                {
                    case "1":
                        proxy.SellBigHealingPotion(_player);
                        break;
                    case "2":
                        proxy.SellSmallHealingPotion(_player);
                        break;
                    case "3":
                        proxy.SellBigRagePotion(_player);
                        break;
                    case "4":
                        proxy.SellSmallRagePotion(_player);
                        break;
                    case "5":
                        proxy.SellBigManaPotion(_player);
                        break;
                    case "6":
                        proxy.SellSmallManaPotion(_player);
                        break;
                    case "7":
                        proxy.SellMysteriousPotion(_player);
                        break;        
                    case "8":
                        WriteLine($"\nYour balance is {_player.Coins} coins");
                        break;
                    case "9":
                        return;
                    default:
                        InvalidOption("Try again");
                        continue;
                }
            }    
        }
        private static void CheckQuests()
        {
            WriteLine("\n[---------------Quests---------------]");
            WriteLine("Current quests:");
            WriteLine($"Kill {_quests.CurrentSlimeAim} slimes" +
            $"  [{_quests.SlimeCounter}/{_quests.CurrentSlimeAim} slimes were killed] -- reward: {_quests.SlimeQuestCost} coins");
            WriteLine($"Kill {_quests.CurrentWolfAim} wolfes " +
            $"  [{_quests.WolfCounter}/{_quests.CurrentWolfAim} wolfs were killed] -- reward: {_quests.WolfQuestCost} coins");
            WriteLine($"Kill {_quests.CurrentGiantAim} giants " +
            $"  [{_quests.GiantCounter}/{_quests.CurrentGiantAim} giants were killed] -- reward: {_quests.GiantQuestCost} coins");
            WriteLine($"Achieve {_quests.CurrentLevelAim} level [Current level - {_player.Level}] -- reward: {_quests.PlayerLevelQuestCost} coins");
            WriteLine($"Sip {_quests.CurrentPotionAim} potions " +
            $"  [{_quests.PotionCounter}/{_quests.CurrentPotionAim} potions were sipped] -- reward: {_quests.PotionQuestCost} coins");
            WriteLine("[---------------Quests---------------]");
            WriteLine("\nPress any button to exit");
            ReadKey();
        }
        private static Location ChooseLocation()
        {
            Location loc = null;
            bool option = false;
            while(!option)
            {
                WriteLine("\n[---------------Choose location---------------]");
                WriteLine("What location do you want to explore?");
                WriteLine("1. Jungles. Only the stronger survives in the wild conditions"+
                "\n   -Mobs are 1.3 times stronger\n   -Extra effect - mana reduction");
                WriteLine("2. Cave. There aren`t any sunlights"+
                "\n   -Mobs are 1.3 times bigger (have more health)\n   -Extra effect - attack reduction");
                WriteLine("3. Swamp. Mud is everywhere"+
                "\n   -Mobs are 1.15 times bigger and stronger\n   -Extra effect - health reduction");
                WriteLine("4. Exit");
                WriteLine("[---------------Choose location---------------]");
                switch(ReadLine())
                {
                    case "1":
                        loc = new Jungles();
                        option = true;
                        break;
                    case "2":
                        loc = new Cave();
                        option = true;
                        break;
                    case "3":
                        loc = new Swamp();
                        option = true;
                        break;
                    case "4":
                        return null;
                    default:
                        InvalidOption("Try again");
                        continue;
                }
            }
            return loc;
        }
        private static void ExploreLocation()
        {
            PlayerWords($"Goodbye, my sweety, goodbye my home, i`m going to explore the {_location.locationName}!");
            Sleep(2500);
            while(true)
            {
                PlayerWords("Such a good day! Who`s gonna be a problem today?");
                Sleep(2000);
                List<Enemy> enemies = new List<Enemy>();
                Random rand = new Random();
                switch(rand.Next(0,3))
                {
                    case 0:
                        PlayerWords("Eeeeewww, slimes!");
                        int slimeCount = rand.Next(3,6);
                        for(int i = 0; i < slimeCount; i++)
                        {
                            enemies.Add(_location.SpawnSlime(_player.Level));
                        }
                        break;
                    case 1:
                        PlayerWords("Oh no, a wolf!");
                        enemies.Add(_location.SpawnWolf(_player.Level));
                        break;
                    case 2:
                        PlayerWords("So huge! It is a giant!");
                        enemies.Add(_location.SpawnGiant(_player.Level));
                        break;
                }
                Sleep(2500);
                foreach(Enemy en in enemies)
                {
                    _quests.SubscribeOnEnemy(en);
                }
                Combat combat = new Combat(_player, enemies);
                bool win = combat.StartCombat(_quests.WolfCounter);
                if(!win) return;
                else
                {
                    WriteLine("\nDo you want to continue your journey? {yes/no}");
                    string input = ReadLine();
                    if(input == "y" || input == "yes")
                    {
                        continue;
                    }
                    else
                    {
                        return;
                    }
                }
            }  
        }
        private static void InvalidOption(string msg)
        {
            WriteLine("\n[---------------Invalid Input---------------]");
            WriteLine(msg);
            WriteLine("[---------------Invalid Input---------------]");
        }
        private static void PlayerWords(string msg)
        {
            WriteLine($"\n***{_player.nickName}***");
            WriteLine(msg);
            WriteLine($"***{_player.nickName}***");
        }
    }
}