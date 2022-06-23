using coursework.Entities.Enemies;
namespace coursework.Entities.Players
{
    public class Magician : Player
    {
        private int _fireBallLevel = 1;
        private int _fireBallDamage = 500;
        private int _fireBallManaCost = 175;
        private int _iceBallLevel = 1;
        private int _iceBallDamage = 400;
        private int _iceBallManaCost = 150;
        public Magician()
        {
            this.HealthLimit = 700;
            this.Health = HealthLimit;
            this.ArmorLimit = 700;
            this.Armor = ArmorLimit;
            this.AttackLimit = 350;
            this.Attack = AttackLimit;
            this.ManaLimit = 400;
            this.Mana = ManaLimit;
        }
        //-----------Main Attack + Recover Mana---------//
        protected override void PerformMainAttack(Enemy en)
        {
            StaffStrike(en);
        }
        protected override void RecoverMana()
        {
            this.Mana += 5*_level;
            if(Mana > ManaLimit)
            {
                Mana = ManaLimit;
            }
        }
        private void StaffStrike(Enemy enemy)
        {
            Console.WriteLine("Staff Strike!");
            MakeDamage(enemy, Attack);
            // Otheal?
        }
        //-----------Main Attack + Recover Mana---------//

        //-----------Secondary Attack---------//
        protected override void PerformSecondaryAttack(List<Enemy> enemies)
        {
            LightningStrike(enemies);
        }
        private void LightningStrike(List<Enemy> enemies)
        {
            double damagePerEnemy = (double)Attack/enemies.Count;
            double armorDamage = damagePerEnemy*0.85;
            double directDamage = damagePerEnemy*0.15;
            foreach(Enemy e in enemies)
            {
                MakeDamage(e, armorDamage);
                e.Health -= directDamage;
            }
            // Otheal?
        }
        //-----------Secondary Attack---------//
        protected override void CheckUniqueSkillLevel()
        {

        }
        protected override void StatsUp()
        {
            //TODO:
        }

        //-----------First skill attack---------//
        protected override double CheckSkillManaCost(bool firstSkill)
        {
            return firstSkill ? _fireBallManaCost : _iceBallManaCost;
        }
        protected override void PerformFirstSkillAttack(List<Enemy> enemies)
        {
            CastFireBall(enemies);
        }
        private void CastFireBall(List<Enemy> enemies) // need skill
        {
            Console.WriteLine("Katon! Gokakyu no Jutsu!");
            double totalDamage = _fireBallDamage * _fireBallLevel + Attack/2;
            double damagePerEnemy = (double)totalDamage/enemies.Count;
            double armorDamage = damagePerEnemy*0.7;
            double directDamage = damagePerEnemy*0.3;
            foreach(Enemy e in enemies)
            {
                MakeDamage(e, armorDamage);
                e.Health -= directDamage;
            }
        }
        protected override void ActivateSkillAttackBonus()
        {
            Mana += 10 * Level;
            if(Mana > ManaLimit)
            {
                Mana = ManaLimit;
            }
        }
        //-----------First skill attack---------//

        //-----------Second skill attack---------//
        protected override void PerformSecondSkillAttack(Enemy enemy)
        {
            CastIceBall(enemy);
        }
        private void CastIceBall(Enemy enemy) //need skill
        {
            Console.WriteLine("Shogyō Mujō!");
            double totalDamage = _iceBallDamage * _iceBallLevel+ Attack/2.5;
            MakeDamage(enemy, totalDamage);
        }
        //-----------Second skill attack---------//
        protected override void PerformShieldActivation()
        {
            ActivateMagicalArmor();
        }
        private void ActivateMagicalArmor()
        {
            Console.WriteLine("Magical Armor!");
            this.Armor += this.ArmorLimit / 2.5;
            Console.WriteLine($"Current armor: {Armor}");
        }

        protected override void AdditionalInfo()
        {
            Console.WriteLine($"Main attack:            Staff strike");
            Console.WriteLine($"Main attack info:       [{Attack}] damage");
            Console.WriteLine($"Secondary attack:       Lightning strike - range attack");
            Console.WriteLine($"Secondary attack info:  [{Attack}] damage; 15% - direct attack");
            Console.WriteLine($"First skill:            Fireball  - range attack");
            Console.WriteLine($"First skill attack:     [{_fireBallDamage * _fireBallLevel + Attack/2}] damage; 30% - direct attack");
            Console.WriteLine($"First skill level:      [{ _fireBallLevel}]");
            Console.WriteLine($"First skill mana cost:  [{ _fireBallManaCost}]");
            Console.WriteLine($"Second skill:           Iceball");
            Console.WriteLine($"Second skill attack:    [{_iceBallDamage * _iceBallLevel + Attack/2.5}] damage");
            Console.WriteLine($"Second skill level:     [{ _iceBallLevel}]");
            Console.WriteLine($"Second skill mana cost: [{ _iceBallManaCost}]");
            Console.WriteLine($"Mana recovery:          [...] per attack");
        }    
    }
}