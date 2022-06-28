using coursework.src.Entities.Players;
using System;
namespace coursework.src.Entities.Enemies
{
    public class CaveSlime : Slime
    {
        private double _attackReductionCoeff = 0.99;
        public CaveSlime(int level, double hpBonus) : base(level)
        {
            this._mobName = "Cave " + _mobName;
            this.HealthLimit= HealthLimit + HealthLimit*hpBonus;
            this.Health = HealthLimit;
        }
        protected override void ExtraEffect(Player player)
        {
            Console.WriteLine("Attack Reduction!");
            player.Attack *= _attackReductionCoeff;
        }
    }
}