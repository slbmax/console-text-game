namespace coursework.src.Potions
{
    public class SmallManaLossPotion : ManaLossPotion
    {
        public SmallManaLossPotion()
        {
            this._manaCoefficient = 0.2;
            this._name = "Small Mana Loss Potion";
        }
    }
}