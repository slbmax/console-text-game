using coursework.Entities.Enemies;
namespace coursework.Locations
{
    public class Jungles : Location
    {
        private double _attackBonus = 0.3; 
        public Jungles()
        {
            this.locationName = "Jungles";
        }
        public override Slime SpawnSlime(int level)
        {
            return new JungleSlime(level, _attackBonus);
        }
        public override Wolf SpawnWolf(int level)
        {
            return new JungleWolf(level, _attackBonus);
        }
        public override Giant SpawnGiant(int level)
        {
            return new JungleGiant(level, _attackBonus);
        }
    }
}