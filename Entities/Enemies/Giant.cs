using coursework.Entities.Players;
namespace coursework.Entities.Enemies
{
    abstract public class Giant : Enemy
    {
        private Random rand = new Random();
        public Giant(int level)
        {
            this._mobName = "giant";
            this.HealthLimit = 900 + level * 100;
            this.Health = HealthLimit;
            this.Armor = 900 + level * 100;
            this.ArmorLimit = Armor;
            this.Attack = 450 + level * 100;
            this.coinsAward = 100;
            this.expAward = 300;
        }
        public bool GiantPunch(Player p)
        {
            double currAttack = GetCurrentAttackPoints(2);
            Console.WriteLine($"\n***{_mobName}***");
            Console.WriteLine($"GIANT PUNCH!");
            MakeDamage(p, currAttack);
            Console.WriteLine("Extra effect:");
            ExtraEffect(p);
            Console.WriteLine($"***{_mobName}***");
            return true;
        }
        public bool GiantSlam(Player p)
        {
            double currAttack = GetCurrentAttackPoints(1.5);
            double directDamage = currAttack*0.25;
            double armorDamage = currAttack*0.75;
            Console.WriteLine($"\n***{_mobName}***");
            Console.WriteLine($"GIANT SLAAAAMMM!");
            MakeDamage(p, armorDamage);
            p.Health -= directDamage;
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
                    return GiantPunch(player);
                case 3:
                case 4:
                    return GiantSlam(player);
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
        protected double GetCurrentAttackPoints(double coeff)
        {
            return rand.Next(0,2) == 0 ? coeff * this.Attack : this.Attack / coeff;
        }
        protected override void AdditionalInfo()
        {
            Console.WriteLine($"Main attack:            Giant punch");
            Console.WriteLine($"Main attack info:       [{Attack}] damage (in average) (multiply coeff. - 2)");
            Console.WriteLine($"Secondary attack:       Giant slam");
            Console.WriteLine($"Secondary attack info:  [{Attack}] damage (in average) (multiply coeff. - 1.5); 25% - direct attack");
            Console.WriteLine($"Unique ability:         Giants attack can be stronger or weaker - raaandoom :)");
        }
    }
}