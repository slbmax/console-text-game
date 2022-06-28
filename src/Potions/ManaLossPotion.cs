using coursework.src.Entities;
using System;
namespace coursework.src.Potions
{
    abstract public class ManaLossPotion : Potion
    {
        protected double _manaCoefficient;
        public override void ActivateEffect(Entity p)
        {
            Console.WriteLine($"Sipping {_name}");
            p.Mana -= p.ManaLimit * _manaCoefficient;
            if(p.Mana < 0)
            {
                p.Mana = 0;
            }
            Console.WriteLine($"Current mana points: {p.Mana}/{p.ManaLimit}");
        }
    }
}