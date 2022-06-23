using coursework.Entities.Players;
using coursework.Entities.Enemies;
namespace coursework.Quests
{
    public class Quest : IObserver
    {
        private Player _player;
        private (int,int,int,int) _slimeKillsCounter;
        public int SlimeCounter {get => _slimeKillsCounter.Item1;}
        public int CurrentSlimeAim {get => _slimeKillsCounter.Item2;}
        public int SlimeQuestCost {get => _slimeKillsCounter.Item3;}
        private (int,int,int,int) _wolfKillsCounter;
        public int WolfCounter {get => _wolfKillsCounter.Item1;}
        public int CurrentWolfAim {get => _wolfKillsCounter.Item2;}
        public int WolfQuestCost {get => _wolfKillsCounter.Item3;}
        private (int,int,int,int) _giantKillsCounter;
        public int GiantCounter {get => _giantKillsCounter.Item1;}
        public int CurrentGiantAim {get => _giantKillsCounter.Item2;}
        public int GiantQuestCost {get => _giantKillsCounter.Item3;}
        private (int,int) _playerLevelCounter;
        public int CurrentLevelAim {get => _playerLevelCounter.Item1;}
        public int PlayerLevelQuestCost {get => _playerLevelCounter.Item2;}
        private (int,int,int,int) _playerPotionsCounter;
        public int PotionCounter {get => _playerPotionsCounter.Item1;}
        public int CurrentPotionAim {get => _playerPotionsCounter.Item2;}
        public int PotionQuestCost {get => _playerPotionsCounter.Item3;}
        public Quest(Player p)
        {
            _player = p;
            p.Notify += Update;
            _slimeKillsCounter = (0,10,200,0);
            _wolfKillsCounter  = (0,7,210,0);
            _giantKillsCounter = (0,3,300,0);
            _playerLevelCounter = (5,400);
            _playerPotionsCounter = (0,5,200,0);
        }
        public void SubscribeOnEnemy(Enemy e)
        {
            e.Notify += Update;
        }
        protected override void Update(string msgSender, string value)
        {
            if(msgSender.Contains("Enemies"))
            {
                UpdatePlayerKills(msgSender);
            }
            else if(msgSender.Contains("Players"))
            {
                UpdatePlayerStats(value);
            }
            else
            {
                return;
            }
        }
        private void UpdatePlayerKills(string enemy)
        {
            if(enemy.Contains("Slime"))
            {
                _slimeKillsCounter.Item1++;
                if(SlimeCounter == CurrentSlimeAim)
                {
                    int slimeReward = 20;
                    AimAchieved($"kill {CurrentSlimeAim} slimes", SlimeQuestCost);
                    _slimeKillsCounter.Item4 ++ ;
                    _slimeKillsCounter.Item2 += 10 * _slimeKillsCounter.Item4;
                    _slimeKillsCounter.Item3  = 10 * _slimeKillsCounter.Item4 * slimeReward; 
                }
            }
            else if(enemy.Contains("Wolf"))
            {
                _wolfKillsCounter.Item1++;
                if(WolfCounter == CurrentWolfAim)
                {
                    int wolfReward = 30;
                    AimAchieved($"kill {CurrentWolfAim} wolfs", WolfQuestCost);
                    _wolfKillsCounter.Item4 ++ ;
                    _wolfKillsCounter.Item2 += 7 * _wolfKillsCounter.Item4;
                    _wolfKillsCounter.Item3  = 7 * _wolfKillsCounter.Item4 * wolfReward; 
                }
            }
            else if(enemy.Contains("Giant"))
            {
                _giantKillsCounter.Item1++;
                if(GiantCounter == CurrentGiantAim)
                {
                    int giantReward = 100;
                    AimAchieved($"kill {CurrentGiantAim} giants", GiantQuestCost);
                    _giantKillsCounter.Item4 ++ ;
                    _giantKillsCounter.Item2 += 3 * _giantKillsCounter.Item4;
                    _giantKillsCounter.Item3  = 3 * _giantKillsCounter.Item4 * giantReward; 
                }
            }
            else
            {
                return;
            }
        }
        private void UpdatePlayerStats(string value)
        {
            if(value.Contains("potion"))
            {
                _playerPotionsCounter.Item1++;
                if(PotionCounter == CurrentPotionAim)
                {
                    int potionReward = 100;
                    AimAchieved($"sip {CurrentPotionAim} potions", PotionQuestCost);
                    _playerPotionsCounter.Item4 ++ ;
                    _playerPotionsCounter.Item2 += 3 * _playerPotionsCounter.Item4;
                    _playerPotionsCounter.Item3  = 3 * _playerPotionsCounter.Item4 * potionReward; 
                }
            }
            else if(value.Contains("level"))
            {
                if(_player.Level == CurrentLevelAim)
                {
                    AimAchieved($"achieve {_player.Level} level", PlayerLevelQuestCost);
                    _playerLevelCounter.Item1 += 5;
                }
            }
            else
            {
                return;
            }
        }
        private void AimAchieved(string quest, int reward)
        {
            Console.WriteLine("\n[---------------Quests---------------]");
            Console.WriteLine($"Quest \"{quest}\" achieved!");
            Console.WriteLine($"Your reward is {reward} coins!");
            _player.Coins += reward;
            Console.WriteLine("[---------------Quests---------------]");
        }
    }
}