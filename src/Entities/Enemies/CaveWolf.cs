using coursework.src.Entities.Players;
using System;
namespace coursework.src.Entities.Enemies
{
    public class CaveWolf : Wolf
    {
        private double _attackReductionCoeff = 0.98;
        public CaveWolf(int level, double hpBonus) : base(level)
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