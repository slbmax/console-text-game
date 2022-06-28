using coursework.src.Entities.Enemies;

namespace coursework.src.Locations
{
    public class Swamp : Location
    {
        private double _attackBonus = 0.15;
        private double _hpBonus = 0.15;
        public Swamp()
        {
            this.locationName = "Swamp";
        }
        public override Giant SpawnGiant(int level)
        {
            return new SwampGiant(level, _hpBonus, _attackBonus);
        }

        public override Slime SpawnSlime(int level)
        {
            return new SwampSlime(level, _hpBonus, _attackBonus);
        }

        public override Wolf SpawnWolf(int level)
        {
            return new SwampWolf(level, _hpBonus, _attackBonus);
        }
    }
}