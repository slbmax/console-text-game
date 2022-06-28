using coursework.src.Entities.Players;
using System;
namespace coursework.src.Entities.Enemies
{
    public class JungleSlime : Slime
    {
        private double _manaReductionCoeff = 0.99;
        public JungleSlime(int level, double juglesBonus) : base(level)
        {
            this._mobName = "Jungle " + _mobName;
            this.Attack = Attack + Attack*juglesBonus;
        }
        protected override void ExtraEffect(Player player)
        {
            Console.WriteLine("Mana Reduction!");
            player.Mana *= _manaReductionCoeff;
        }
    }
}