using coursework.src.Entities.Players;
namespace coursework.src.Shop
{
    abstract public class IShop // or abstract
    {
        public int bigHealingPotionPrice {get;} = 100;
        public int smallHealingPotionPrice {get;} = 75;
        public int bigRagePotionPrice {get;} = 75;
        public int smallRagePotionPrice {get;} = 50;
        public int bigManaPotionPrice {get;} = 150;
        public int smallManaPotionPrice {get;} = 120;
        public int mysteriousPotionPrice {get;} = 200;
        abstract public void SellBigHealingPotion(Player p);
        abstract public void SellSmallHealingPotion(Player p);
        abstract public void SellBigRagePotion(Player p);
        abstract public void SellSmallRagePotion(Player p);
        abstract public void SellBigManaPotion(Player p);
        abstract public void SellSmallManaPotion(Player p);
        abstract public void SellMysteriousPotion(Player p);
        abstract public void SellUpgradingSkills(Player p); //!!!
    }
}