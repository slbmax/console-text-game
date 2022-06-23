using static System.Console;
using static System.Threading.Thread;
using coursework.Entities.Players;
using coursework.Entities.Enemies;
using coursework.Locations;
using coursework.Shop;
using coursework.Quests;


using coursework.Potions;
namespace coursework
{
    public static class Game
    {
        private static Player _player;
        private static Location _location;
        private static Quest _quests;
        private static Random rand = new Random();
        public static void StartGame()
        {
            WriteLine("[---------------RPG TITLE---------------]");
            WriteLine("Welcome, stranger!");
            Sleep(1000);
            WriteLine("This is a bla bla bla");
            Sleep(1000);
            WriteLine("Here you can bla bla bla");
            Sleep(1000);
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
            WriteLine($"Great, {nickName}!");
            WriteLine("Secondly, choose your character:");
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
            WriteLine("Now you can explore this fantastic world!");
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
                WriteLine("5. Exit game");
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
                        ExploreLocation();
                        break;
                    case "5":
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
        private static void EnterShop()
        {
            IShop proxy = new ProxyShop();
            
            WriteLine("\n[---------------Shop---------------]");
            WriteLine("Welcome to the MaxShop!");
            while(true)
            {
                WriteLine("\nWhat do you want to buy?");
                WriteLine($"1. Big healing potion - {proxy.bigHealingPotionPrice} coins [3d-level character or higher]");
                WriteLine($"2. Small healing potion - {proxy.smallHealingPotionPrice} coins [2d-level character or higher]");
                WriteLine($"3. Big rage potion - {proxy.bigRagePotionPrice} coins [4th-level character or higher]");
                WriteLine($"4. Small rage potion - {proxy.smallRagePotionPrice} coins [3th-level character or higher]");
                WriteLine($"5. Big mana potion - {proxy.bigManaPotionPrice} coins [5th-level character or higher]");
                WriteLine($"6. Small mana potion - {proxy.smallManaPotionPrice} coins [4th-level character or higher]");
                WriteLine($"7. MYSTERIOUS potion - {proxy.mysteriousPotionPrice} coins [5d-level character or higher]");
                WriteLine("//TODOOOOOOO");
                WriteLine("9. Check balance");
                WriteLine("10. Exit shop");

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
                    case "9":
                        WriteLine($"Your balance is {_player.Coins} coins");
                        break;
                    case "10":
                        WriteLine("[---------------Shop---------------]");
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
            WriteLine($"Kill {_quests.CurrentSlimeAim} slimes " +
            $"[{_quests.SlimeCounter}/{_quests.CurrentSlimeAim} slimes were killed] -- reward: {_quests.SlimeQuestCost} coins");
            WriteLine($"Kill {_quests.CurrentWolfAim} wolfes " +
            $"[{_quests.WolfCounter}/{_quests.CurrentWolfAim} wolfs were killed] -- reward: {_quests.WolfQuestCost} coins");
            WriteLine($"Kill {_quests.CurrentGiantAim} giants " +
            $"[{_quests.GiantCounter}/{_quests.CurrentGiantAim} giants were killed] -- reward: {_quests.GiantQuestCost} coins");
            WriteLine($"Achieve {_quests.CurrentLevelAim} level [Current level - {_player.Level}] -- reward: {_quests.PlayerLevelQuestCost} coins");
            WriteLine($"Sip {_quests.CurrentPotionAim} potions " +
            $"[{_quests.PotionCounter}/{_quests.CurrentPotionAim} potions were sipped] -- reward: {_quests.PotionQuestCost} coins");
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
                switch(rand.Next(1,2))
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
                bool win = Combat(enemies);
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
        private static bool Combat(List<Enemy> enemies)
        {
            WriteLine("\n[---------------COMBAT---------------]");
            WriteLine("COMBAT BEGINS!");
            Sleep(2000);
            int expAward = enemies[0].expAward * enemies.Count;
            int coinsAward = enemies[0].coinsAward * enemies.Count;
            bool isPlayerWin = false;
            bool cycle = true;
            Random rand = new Random();
            int coin = rand.Next(0,2);
            Write("Defining first turn");
            Sleep(550);Write(".");Sleep(550);Write(".");Sleep(550);WriteLine(".");Sleep(750);
            int turnCounter = 0;
            while(cycle)
            {
                if(coin == 0)
                {
                    WriteLine($"\n{_player.nickName}, your turn!");
                    for(int count = 0; count < 2; count ++)
                    {
                        Console.WriteLine("\nTurn №"+(count+1));
                        PlayerCombatTurn(enemies);
                        if(_player.Health <= 0)
                        {
                            cycle = false;
                            break;
                        }
                        enemies = UpdateEnemiesList(enemies);
                        if(enemies.Count == 0)
                        {
                            cycle = false;
                            isPlayerWin = true;
                            break;
                        }
                    }
                    coin = 1;
                }
                else
                {
                    WriteLine($"\nEnemy turn!");
                    for(int count = 0; count < 2; count ++)
                    {
                        Console.WriteLine("\nTurn №"+(count+1));
                        for(int i = 0; i < enemies.Count;)
                        {
                            Console.WriteLine("\nEnemy №"+(i+1));
                            Sleep(2000);
                            bool succes = enemies[i].PerformAttack(_player);
                            if(_player.Health <= 0)
                            {
                                cycle = false;
                                break;
                            }
                            if(succes)
                            {
                                i ++;
                            }
                        }
                        if(!cycle)
                        {
                            break;
                        }
                    }
                    if(enemies[0].ToString().Contains("Wolf") && turnCounter != 0)
                    {
                        WriteLine("\nAuuuuuuuufff");
                        enemies = CloneWolfs(enemies);
                        WriteLine("\nWolfs were cloned!");
                    }
                    coin = 0;
                }
                turnCounter++;
            }
            _player.RestoreStats();
            if(isPlayerWin)
            {
                PlayerWords("YAY!!\nAll enemies are defeated");
                WriteLine("\nCongratulations! Your reward!");
                _player.EarnAwards(coinsAward, expAward);
            }
            else
            {
                PlayerWords("NOOooo.....");
                WriteLine("\nYOU LOST");
                WriteLine($"You lost 50 coins");
                _player.Coins -= 50;
                if(_player.Coins < 0)   _player.Coins = 0;
            }
            WriteLine("\nPress any button to continue");
            ReadKey();
            WriteLine("[---------------COMBAT---------------]");
            return isPlayerWin;
        }
        private static void PlayerCombatTurn(List<Enemy> enemies)
        {
            while(true)
            {
                WriteLine("\nWhat do you want to do?");
                WriteLine("1. Perform main attack");
                WriteLine("2. Perform secondary attack");
                WriteLine("3. Perform first skill attack");
                WriteLine("4. Perform second skill attack");
                WriteLine("5. Activate shield");
                WriteLine("6. Drink healing potion");
                WriteLine("7. Drink rage potion");
                WriteLine("8. Drink mana potion");
                WriteLine("9. Drink MYSTERIOUS potion");
                WriteLine("10. Wiew enemies info");
                WriteLine("11. Wiew personal info");
                switch(ReadLine())
                {
                    case "1":
                    case "2":
                        if(!_player.SecondaryAttack(enemies))
                        {
                            continue;
                        }
                        else
                        {
                            return;
                        }
                    case "3":
                        if(!_player.FirstSkillAttack(enemies))
                        {
                            continue;
                        }
                        else
                        {
                            return;
                        }
                    case "4":
                    case "5":
                        if(!_player.ActivateShield())
                        {
                            continue;
                        }
                        else
                        {
                            return;
                        }
                    case "6":
                        if(!_player.DrinkHealingPotion())
                        {
                            continue;
                        }
                        else
                        {
                            return;
                        }
                    case "7":
                        if(!_player.DrinkRagePotion())
                        {
                            continue;
                        }
                        else
                        {
                            return;
                        }
                    case "8":
                        if(!_player.DrinkManaPotion())
                        {
                            continue;
                        }
                        else
                        {
                            return;
                        }
                    case "9":
                        if(!_player.DrinkMysteriousPotion())
                        {
                            continue;
                        }
                        else
                        {
                            return;
                        }
                    case "10":
                        ViewEnemiesInfo(enemies);
                        WriteLine("\nPress any button to continue");
                        ReadKey();
                        continue;
                    case "11":
                        _player.ShowInfo();
                        WriteLine("\nPress any button to continue");
                        ReadKey();
                        continue;
                    default:
                        InvalidOption("Try again");
                        continue;
                }
            }
        }
        private static void ViewEnemiesInfo(List<Enemy> enemies)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                Console.WriteLine("Enemy №"+(i+1));
                enemies[i].ShowInfo();
            }
        }
        private static List<Enemy> UpdateEnemiesList(List<Enemy> enemies)
        {
            List<Enemy> newList = new List<Enemy>();
            foreach(Enemy en in enemies)
            {
                if(en.IsAlive())
                {
                    newList.Add(en);
                }
            }
            return newList;
        }
        private static List<Enemy> CloneWolfs(List<Enemy> enemies)
        {
            List<Enemy> newList = new List<Enemy>();
            foreach(Enemy en in enemies)
            {
                Wolf w = (Wolf)en;
                newList.Add(w.Clone());
                newList.Add(w.Clone());
            }
            return newList;
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