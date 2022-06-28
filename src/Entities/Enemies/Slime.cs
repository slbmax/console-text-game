using coursework.src.Entities.Players;
using System;
namespace coursework.src.Entities.Enemies
{
    abstract public class Slime : Enemy
    {
        public Slime(int level)
        {
            this._mobName = "slime";
            this.HealthLimit = 200 + level*25;
            this.Health = HealthLimit;
            this.Armor = 100 + level*25;
            this.ArmorLimit = Armor;
            this.Attack = 70 + level*25;
            this.coinsAward = 20;
            this.expAward = 60;
        }
        public bool SlimeBlast(Player player)
        {
            Console.WriteLine($"\n***{_mobName}***");
            Console.WriteLine($"ssSSsSlimee Blast!");
            MakeDamage(player, Attack);
            Console.WriteLine("Extra effect:");
            ExtraEffect(player);
            Console.WriteLine($"***{_mobName}***");
            return true;
        }
        public bool ThrowSpikeSlime(Player player)
        {
            Console.WriteLine($"\n***{_mobName}***");
            Console.WriteLine($"Slime spikes!");
            double directDamage = Attack*0.3;
            double armorDamage = Attack*0.7;
            MakeDamage(player, armorDamage);
            player.Health -= directDamage;
            Console.WriteLine("Extra effect:");
            ExtraEffect(player);
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
                    return SlimeBlast(player);
                case 3:
                case 4:
                    return ThrowSpikeSlime(player);
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
        protected override void AdditionalInfo()
        {
            Console.WriteLine($"Main attack:            Slime blast");
            Console.WriteLine($"Main attack info:       [{Attack}] damage");
            Console.WriteLine($"Secondary attack:       Slime spikes");
            Console.WriteLine($"Secondary attack info:  [{Attack}] damage; 30% - direct attack");
            Console.WriteLine($"Unique ability:         Slimes always appear in groups");
        }
    }
}