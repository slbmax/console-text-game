using coursework.src.Entities;
using System;
namespace coursework.src.Potions
{
    abstract public class ManaPotion : Potion
    {
        protected double _manaCoefficient;
        public override void ActivateEffect(Entity p)
        {
            Console.WriteLine($"Sipping {_name}");
            p.Mana += p.ManaLimit * _manaCoefficient;
            if(p.Mana > p.ManaLimit)
            {
                p.Mana = p.ManaLimit;
            }
            Console.WriteLine($"Current mana points: {p.Mana}/{p.ManaLimit}");
        }
    }
}