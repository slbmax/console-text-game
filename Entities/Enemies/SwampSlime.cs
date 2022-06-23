using coursework.Entities.Players;
namespace coursework.Entities.Enemies
{
    public class SwampSlime : Slime
    {
        private double _healthReductionCoeff = 0.99;
        public SwampSlime(int level, double hpBonus, double attackBonus) : base(level)
        {
            this._mobName = "Swamp " + _mobName;
            this.Attack = Attack + Attack*attackBonus;
            this.HealthLimit= HealthLimit + HealthLimit*hpBonus;
            this.Health = HealthLimit;
        }
        protected override void ExtraEffect(Player player)
        {
            Console.WriteLine("Health Reduction!");
            player.Health *= _healthReductionCoeff;
        }
    }
}