using coursework.Entities.Players;
namespace coursework.Entities.Enemies
{
    public class JungleGiant : Giant
    {
        private double _manaReductionCoeff = 0.95;
        public JungleGiant(int level, double juglesBonus) : base(level)
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