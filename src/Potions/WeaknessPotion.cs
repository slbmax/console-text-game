using coursework.src.Entities;
using System;
namespace coursework.src.Potions
{
    abstract public class WeaknessPotion : Potion
    {
        protected double _rageCoefficient;
        public override void ActivateEffect(Entity p)
        {
            Console.WriteLine($"Sipping {_name}");
            p.Attack -= p.Attack * _rageCoefficient;
            if(p.Attack < 0)
            {
                p.Attack = 0;
            }
            Console.WriteLine($"Current attack: {p.Attack}");
        }
    }
}