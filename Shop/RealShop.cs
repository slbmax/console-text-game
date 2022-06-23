using coursework.Entities.Players;
using coursework.Potions;
namespace coursework.Shop
{
    public class RealShop : IShop
    {
        private List<Potion> _allPotions;
        public RealShop()
        {
            _allPotions = new List<Potion>();
            _allPotions.Add(new BigHealingPotion());
            _allPotions.Add(new BigRagePotion());
            _allPotions.Add(new BigManaPotion());
            _allPotions.Add(new BigPoisonousPotion());
            _allPotions.Add(new BigWeaknessPotion());
            _allPotions.Add(new BigManaLossPotion());
            _allPotions.Add(new SmallHealingPotion());
            _allPotions.Add(new SmallRagePotion());
            _allPotions.Add(new SmallManaPotion());
            _allPotions.Add(new SmallPoisonousPotion());
            _allPotions.Add(new SmallWeaknessPotion());
            _allPotions.Add(new SmallManaLossPotion());
            _allPotions.Add(new HugeHealingPotion());
            _allPotions.Add(new HugeRagePotion());
            _allPotions.Add(new HugeManaPotion());
        }
        public override void SellBigHealingPotion(Player p)
        {
            Console.WriteLine("You successfully bought a big healing potion!");
            p.Coins = p.Coins-bigHealingPotionPrice;
            p.healingPotion = new BigHealingPotion();
            Console.WriteLine($"Your balance now is {p.Coins} coins");
        }
        public override void SellSmallHealingPotion(Player p)
        {
            Console.WriteLine("You successfully bought a small healing potion!");
            p.Coins = p.Coins-smallHealingPotionPrice;
            p.healingPotion = new SmallHealingPotion();
            Console.WriteLine($"Your balance now is {p.Coins} coins");
        }
        public override void SellBigRagePotion(Player p)
        {
            Console.WriteLine("You successfully bought a big rage potion!");
            p.Coins = p.Coins-bigRagePotionPrice;
            p.ragePotion = new BigRagePotion();
            Console.WriteLine($"Your balance now is {p.Coins} coins");
        }
        public override void SellSmallRagePotion(Player p)
        {
            Console.WriteLine("You successfully bought a small rage potion!");
            p.Coins = p.Coins-smallRagePotionPrice;
            p.ragePotion = new SmallRagePotion();
            Console.WriteLine($"Your balance now is {p.Coins} coins");
        }
        public override void SellSmallManaPotion(Player p)
        {
            Console.WriteLine("You successfully bought a small mana potion!");
            p.Coins = p.Coins-smallManaPotionPrice;
            p.manaPotion= new SmallManaPotion();
            Console.WriteLine($"Your balance now is {p.Coins} coins");
        }
        public override void SellBigManaPotion(Player p)
        {
            Console.WriteLine("You successfully bought a big mana potion!");
            p.Coins = p.Coins-bigManaPotionPrice;
            p.manaPotion= new BigManaPotion();
            Console.WriteLine($"Your balance now is {p.Coins} coins");
        }
        public override void SellMysteriousPotion(Player p)
        {
            Console.WriteLine("You successfully bought a MmMmMysterious potion!");
            p.Coins = p.Coins-mysteriousPotionPrice;
            p.mysteriousPotion = GetRandomPotion();
            Console.WriteLine($"Your balance now is {p.Coins} coins");
        }
        private Potion GetRandomPotion()
        {
            Random rand = new Random();
            return _allPotions[rand.Next(0,_allPotions.Count)];
        }
        public override void SellUpgradingSkills(Player p)
        {
            //TODO:
        }
    }
}