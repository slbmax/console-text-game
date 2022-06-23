using coursework.Entities;
namespace coursework.Potions
{
    abstract public class RagePotion : Potion
    {
        protected double _rageCoefficient;
        public override void ActivateEffect(Entity p)
        {
            Console.WriteLine($"Sipping {_name}");
            p.Attack += p.Attack * _rageCoefficient;
            Console.WriteLine($"Current attack: {p.Attack}");
        }
    }
}