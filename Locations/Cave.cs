using coursework.Entities.Enemies;

namespace coursework.Locations
{
    public class Cave : Location
    {
        private double _hpBonus = 0.3;
        public Cave()
        {
            this.locationName = "Cave";
        }
        public override Giant SpawnGiant(int level)
        {
            return new CaveGiant(level, _hpBonus);
        }

        public override Slime SpawnSlime(int level)
        {
            return new CaveSlime(level, _hpBonus);
        }

        public override Wolf SpawnWolf(int level)
        {
            return new CaveWolf(level, _hpBonus);
        }
    }
}