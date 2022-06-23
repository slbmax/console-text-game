using coursework.Entities.Players;
using coursework.Potions;
namespace coursework.Shop
{
    public class ProxyShop : IShop
    {
        public RealShop realShop = new RealShop();
        private bool ValidatePlayerLevel(int playerLevel, int requiredLevel)
        {
            if(playerLevel < requiredLevel)
            {
                Console.WriteLine("Your level is too low to buy this position");
                return false;
            }
            return true;
        }
        private bool ValidatePlayerBalance(int playerBalance, int requiredBalance)
        {
            if(playerBalance < requiredBalance)
            {
                Console.WriteLine("Not enough coins to buy this position");
                return false;
            }
            return true;
        }
        private bool ValidatePlayerPotion(Potion p)
        {
            if(p != null)
            {
                Console.WriteLine("You already have such type of potion in your inventory");
                return false;
            }
            return true;
        }
        public override void SellBigHealingPotion(Player p)
        {
            if(!ValidatePlayerLevel(p.Level, 3))
            {
                return;
            }
            if(!ValidatePlayerBalance(p.Coins, bigHealingPotionPrice))
            {
                return;
            }
            if(!ValidatePlayerPotion((Potion)p.healingPotion))
            {
                return;
            }
            else
            {
                realShop.SellBigHealingPotion(p);
            }
        }
        public override void SellSmallHealingPotion(Player p)
        {
            if(!ValidatePlayerLevel(p.Level, 2))
            {
                return;
            }
            if(!ValidatePlayerBalance(p.Coins, smallHealingPotionPrice))
            {
                return;
            }
            if(!ValidatePlayerPotion((Potion)p.healingPotion))
            {
                return;
            }
            else
            {
                realShop.SellSmallHealingPotion(p);
            }
        }
        public override void SellBigRagePotion(Player p)
        {
            if(!ValidatePlayerLevel(p.Level, 4))
            {
                return;
            }
            if(!ValidatePlayerBalance(p.Coins, bigRagePotionPrice))
            {
                return;
            }
            if(!ValidatePlayerPotion((Potion)p.ragePotion))
            {
                return;
            }
            else
            {
                realShop.SellBigRagePotion(p);
            }
        }
        public override void SellSmallRagePotion(Player p)
        {
            if(!ValidatePlayerLevel(p.Level, 3))
            {
                return;
            }
            if(!ValidatePlayerBalance(p.Coins, smallRagePotionPrice))
            {
                return;
            }
            if(!ValidatePlayerPotion((Potion)p.ragePotion))
            {
                return;
            }
            else
            {
                realShop.SellSmallRagePotion(p);
            }
        }
        public override void SellBigManaPotion(Player p)
        {
            if(!ValidatePlayerLevel(p.Level, 5))
            {
                return;
            }
            if(!ValidatePlayerBalance(p.Coins, bigManaPotionPrice))
            {
                return;
            }
            if(!ValidatePlayerPotion((Potion)p.manaPotion))
            {
                return;
            }
            else
            {
                realShop.SellBigManaPotion(p);
            }
        }
        public override void SellSmallManaPotion(Player p)
        {
            if(!ValidatePlayerLevel(p.Level, 5))
            {
                return;
            }
            if(!ValidatePlayerBalance(p.Coins, smallManaPotionPrice))
            {
                return;
            }
            if(!ValidatePlayerPotion((Potion)p.manaPotion))
            {
                return;
            }
            else
            {
                realShop.SellSmallManaPotion(p);
            }
        }
        public override void SellMysteriousPotion(Player p)
        {
            if(!ValidatePlayerLevel(p.Level, 5))
            {
                return;
            }
            if(!ValidatePlayerBalance(p.Coins, mysteriousPotionPrice))
            {
                return;
            }
            if(!ValidatePlayerPotion((Potion)p.mysteriousPotion))
            {
                return;
            }
            else
            {
                realShop.SellMysteriousPotion(p);
            }
        }
        public override void SellUpgradingSkills(Player p)
        {
            //TODO:
        }
    }
}