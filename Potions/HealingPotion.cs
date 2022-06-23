using coursework.Entities;
namespace coursework.Potions
{
    abstract public class HealingPotion : Potion
    {   
        protected double _healCoefficient;
        public override void ActivateEffect(Entity p)
        {
            Console.WriteLine($"Sipping {_name}");
            p.Health += p.HealthLimit*_healCoefficient;
            if(p.Health > p.HealthLimit)
            {
                p.Health = p.HealthLimit;
            }
            Console.WriteLine($"Current health points: {p.Health}/{p.HealthLimit}");
        }
    }
}