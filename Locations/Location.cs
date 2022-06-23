using coursework.Entities.Enemies;
namespace coursework.Locations
{
    abstract public class Location
    {
        public string locationName;
        abstract public Slime SpawnSlime(int level);
        abstract public Wolf  SpawnWolf (int level);
        abstract public Giant SpawnGiant(int level);
    }
}