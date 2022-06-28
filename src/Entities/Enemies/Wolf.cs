using coursework.src.Entities.Players;
using System;
namespace coursework.src.Entities.Enemies
{
    interface IWolfPrototype
    {
        public Wolf Clone();
    }
    abstract public class Wolf : Enemy, IWolfPrototype
    {
        public Wolf(int level)
        {
            this._mobName = "wolf";
            this.HealthLimit = 750 + level * 50;
            this.Health = HealthLimit;
            this.Armor = 250 + level * 50;
            this.ArmorLimit = Armor;
            this.Attack = 220 + level * 50;
            this.coinsAward = 30;
            this.expAward = 90;
        }
        public bool UnholyBite(Player p)
        {
            Console.WriteLine($"\n***{_mobName}***");
            Console.WriteLine($"Unholy bite!");
            double directDamage = Attack*0.2;
            double armorDamage = Attack*0.8;
            MakeDamage(p, armorDamage);
            p.Health -= directDamage;
            Console.WriteLine("Extra effect:");
            ExtraEffect(p);
            Console.WriteLine($"***{_mobName}***");
            return true;
        }
        public bool UnbearableHowling(Player p)
        {
            Console.WriteLine($"\n***{_mobName}***");
            Console.WriteLine($"AUUUUUUYYFFF!");
            MakeDamage(p, Attack);
            Console.WriteLine("Extra effect:");
            ExtraEffect(p);
            Console.WriteLine($"***{_mobName}***");
            return true;
        }
        public override bool PerformAttack(Player player)
        {
            Random rand = new Random();
            bool success = false;
            switch (rand.Next(1,8))
            {
                case 1:
                case 2:
                    return UnbearableHowling(player);
                case 3:
                case 4:
                    return UnholyBite(player);
                case 5:
                    Console.WriteLine($"\n***{_mobName}***");
                    success = DrinkHealingPotion();
                    Console.WriteLine($"***{_mobName}***");
                    break;
                case 6:
                    Console.WriteLine($"\n***{_mobName}***");
                    success = DrinkRagePotion();
                    Console.WriteLine($"***{_mobName}***");
                    break;
                case 7:
                    Console.WriteLine($"\n***{_mobName}***");
                    success = ActivateShield();
                    Console.WriteLine($"***{_mobName}***");
                    break;
            }
            return success;
        }
        public Wolf Clone()
        {
            return (Wolf) this.MemberwiseClone();
        }
        protected override void AdditionalInfo()
        {
            Console.WriteLine($"Main attack:            Unholy bite");
            Console.WriteLine($"Main attack info:       [{Attack}] damage; 20% - direct attack");
            Console.WriteLine($"Secondary attack:       Unbearable howling");
            Console.WriteLine($"Secondary attack info:  [{Attack}] damage");
            Console.WriteLine($"Unique ability:         Wolf can clone itself after each turn");
        }
    }
}