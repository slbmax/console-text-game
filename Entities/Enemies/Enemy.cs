using coursework.Entities.Players;
using coursework.Potions;
namespace coursework.Entities.Enemies
{
    public abstract class Enemy : Entity
    {
        public delegate void Callback(string sender, string value = null);
        public event Callback Notify;
        protected string _mobName;
        public int expAward {get; protected set;}
        public int coinsAward {get; protected set;}
        abstract protected void ExtraEffect(Player p);
        List<HealingPotion> hpPotions = new List<HealingPotion>();
        List<RagePotion> ragePotions = new List<RagePotion>();
        public override bool DrinkHealingPotion()
        {
            if(Health == HealthLimit)
            {
                Console.WriteLine("Can`t cast healing potion -- hp is max");
                return false;
            }
            if(hpPotions.Count == 0)
            {
                hpPotions.Add(new SmallHealingPotion());
                hpPotions.Add(new BigHealingPotion());
                hpPotions.Add(new HugeHealingPotion());
            }
            Random rand = new Random();
            hpPotions[rand.Next(0,hpPotions.Count)].ActivateEffect(this);
            return true;
        }
        public override bool DrinkRagePotion()
        {
            if(ragePotions.Count == 0)
            {
                ragePotions.Add(new SmallRagePotion());
                ragePotions.Add(new BigRagePotion());
                ragePotions.Add(new HugeRagePotion());
            }
            Random rand = new Random();
            ragePotions[rand.Next(0,ragePotions.Count)].ActivateEffect(this);
            return true;
        }
        public override bool ActivateShield()
        {
            Console.WriteLine("Shield Activation!");
            this.Armor += ArmorLimit / 3.0;
            Console.WriteLine($"Current armor: {Armor}");
            return true;
        }
        abstract public bool PerformAttack(Player player);
        public override void ShowInfo()
        {
            Console.WriteLine($"\n[---------------{_mobName}---------------]");
            Console.WriteLine($"Health:                 [{Health}/{HealthLimit}]");
            Console.WriteLine($"Armor:                  [{Armor}/{ArmorLimit}]");
            AdditionalInfo();
            Console.WriteLine($"Experience reward:      [{expAward}]");
            Console.WriteLine($"Coins reward:           [{coinsAward}] coins");
            Console.WriteLine($"[---------------{_mobName}---------------]");
        }
        public bool IsAlive()
        {
            if(Health > 0)
            {
                return true;
            }
            else
            {
                Notify.Invoke(this.ToString());
                return false;
            }
        }
    }    
}