using coursework.Entities.Players;
namespace coursework.Entities.Enemies
{
    public class CaveGiant : Giant
    {
        private double _attackReductionCoeff = 0.95;
        public CaveGiant(int level, double hpBonus) : base(level)
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