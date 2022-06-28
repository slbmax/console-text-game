using coursework.src.Entities.Enemies;
using coursework.src.Entities.Players;
using static System.Console;
using static System.Threading.Thread;
namespace coursework.src
{
    public class Combat
    {
        private Player _player;
        private List<Enemy> _enemies;
        public Combat(Player p, List<Enemy> enemies)
        {
            this._player = p;
            this._enemies = enemies;
        }
        public bool StartCombat(int wolfCounter)
        {
            WriteLine("\n[---------------COMBAT---------------]");
            WriteLine("COMBAT BEGINS!");
            Sleep(2000);
            int expAward = _enemies[0].expAward;
            int coinsAward = _enemies[0].coinsAward;
            string enemyType = _enemies[0].ToString();
            int enemiesCount = _enemies.Count;
            int currwolfCounter = wolfCounter;
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
                        PlayerCombatTurn(_enemies);
                        if(_player.Health <= 0)
                        {
                            cycle = false;
                            break;
                        }
                        _enemies = UpdateEnemiesList(_enemies);
                        if(_enemies.Count == 0)
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
                        for(int i = 0; i < _enemies.Count;)
                        {
                            Console.WriteLine("\nEnemy №"+(i+1));
                            Sleep(2000);
                            bool succes = _enemies[i].PerformAttack(_player);
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
                    if(enemyType.Contains("Wolf") && turnCounter != 0)
                    {
                        WriteLine("\nAuuuuuuuufff");
                        _enemies = CloneWolfs(_enemies);
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
                if(enemyType.Contains("Wolf"))
                {
                    enemiesCount = wolfCounter - currwolfCounter;
                }
                expAward *= enemiesCount;
                coinsAward *= enemiesCount;
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
        private void PlayerCombatTurn(List<Enemy> enemies)
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
                        int curEnemy = ChooseEnemy(enemies);
                        if(curEnemy == -1 || !_player.MainAttack(enemies[curEnemy-1]))
                        {
                            continue;
                        }
                        else
                        {
                            return;
                        }
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
                        int currentEnemy = ChooseEnemy(enemies);
                        if(currentEnemy == -1 || !_player.SecondSkillAttack(enemies[currentEnemy-1]))
                        {
                            continue;
                        }
                        else
                        {
                            return;
                        }
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
                        _player.DrinkHealingPotion();
                        continue;
                    case "7":
                        _player.DrinkRagePotion();
                        continue;
                    case "8":
                        _player.DrinkManaPotion();
                        continue;
                    case "9":
                        _player.DrinkMysteriousPotion();
                        continue;
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
        private int ChooseEnemy(List<Enemy> enemies)
        {
            WriteLine("\nChoose enemy to attack: (enemy number)");
            int enemyNum;
            bool parsed = int.TryParse(ReadLine(), out enemyNum);
            if(!parsed)
            {
                InvalidOption("Invalid input");
                return -1;
            }
            if(enemyNum <= 0 || enemyNum > enemies.Count)
            {
                InvalidOption("This enemy does not exist");
                return -1;
            }
            return enemyNum;
        }
        private void ViewEnemiesInfo(List<Enemy> enemies)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                Console.WriteLine("Enemy №"+(i+1));
                enemies[i].ShowInfo();
            }
        }
        private List<Enemy> UpdateEnemiesList(List<Enemy> enemies)
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
        private List<Enemy> CloneWolfs(List<Enemy> enemies)
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
        private void InvalidOption(string msg)
        {
            WriteLine("\n[---------------Invalid Input---------------]");
            WriteLine(msg);
            WriteLine("[---------------Invalid Input---------------]");
        }
        private void PlayerWords(string msg)
        {
            WriteLine($"\n***{_player.nickName}***");
            WriteLine(msg);
            WriteLine($"***{_player.nickName}***");
        }
    }
}