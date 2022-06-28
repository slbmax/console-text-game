using coursework.src.Entities;
using System;
namespace coursework.src.Potions
{
    abstract public class PoisonousPotion : Potion
    {   
        protected double _poisonCoefficient;
        public override void ActivateEffect(Entity p)
        {
            Console.WriteLine($"Sipping {_name}");
            p.Health -= p.HealthLimit*_poisonCoefficient;
            if(p.Health < 0)
            {
                p.Health = 0;
            }
            Console.WriteLine($"Current health points: {p.Health}/{p.HealthLimit}");
        }
    }
}