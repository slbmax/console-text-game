using coursework.Entities.Enemies;
namespace coursework.Entities.Players
{
    public class Warrior : Player
    {
        private int _bloodlustLevel = 1;
        private int _bloodlustDamage = 325;
        private int _bloodlustManaCost = 125;
        private int _almightyPushLevel = 1;
        private int _almightyPushDamage = 350;
        private int _almightyPushManaCost = 150;
        public Warrior()
        {
            this.characterType = "Warrior";
            this.HealthLimit = 1000;
            this.Health = HealthLimit;
            this.ArmorLimit = 1000;
            this.Armor = ArmorLimit;
            this.AttackLimit = 400;
            this.Attack = AttackLimit;
            this.ManaLimit = 275;
            this.Mana = ManaLimit;
            this._manaRecovery = 15;
        }
        //-----------Main Attack + Recover Mana---------//
        protected override void PerformMainAttack(Enemy en)
        {
            FuriousKick(en);
        }
        public void FuriousKick(Enemy enemy)
        {
            Console.WriteLine("Fffurious Kick!");
            MakeDamage(enemy, Attack);
            //attack bonus?
        }
        //-----------Main Attack + Recover Mana---------//


        //-----------Secondary Attack---------//
        protected override void PerformSecondaryAttack(List<Enemy> enemies)
        {
            SmashingBlade(enemies);
        }
        private void SmashingBlade(List<Enemy> enemies) // rand wrag + probivka + echo na ostalnuh
        {
            Console.WriteLine("Let my blade smash your head! Smashing blade!");
            Random rand = new Random();
            int unlucky = rand.Next(0,enemies.Count);
            MakeDamage(enemies[unlucky], Attack*0.7);
            enemies[unlucky].Health -= Attack*0.3;
            foreach(Enemy e in enemies)
            {
                MakeDamage(e, Attack*0.07);
            }
            //attack bonus?
        }
        //-----------Secondary Attack---------//


        //-----------First skill attack---------//
        protected override double CheckSkillManaCost(bool firstSkill)
        {
            return firstSkill ? _almightyPushManaCost : _bloodlustManaCost;
        }
        protected override void PerformFirstSkillAttack(List<Enemy> enemies)
        {
            AlmightyPush(enemies);
        }
        public void AlmightyPush(List<Enemy> enemies)
        {
            Console.WriteLine("The world shall know pain... ALMIGHTY PUSH!");
            double totalDamage = _almightyPushDamage * _almightyPushLevel + Attack/2;
            double damagePerEnemy = (double)totalDamage/enemies.Count;
            foreach(Enemy e in enemies)
            {
                MakeDamage(e, damagePerEnemy);
            }
        }
        protected override void ActivateSkillAttackBonus()
        {
            double armorBonus = ArmorLimit/5.5;
            Console.WriteLine($"Armor bonus:   +[{armorBonus}]");
            Armor += armorBonus;
            Console.WriteLine($"Current armor: [{Armor}]");
        }
        //-----------First skill attack---------//


        //-----------Second skill attack---------//
        protected override void PerformSecondSkillAttack(Enemy enemy)
        {
            BloodlustAttack(enemy);
        }
        private void BloodlustAttack(Enemy enemy)
        {
            Console.WriteLine("I need more blood! Bloodlust attack!");
            double totalDamage = _bloodlustDamage * _bloodlustLevel+Attack/2.5;
            double armorDamage = totalDamage*0.7;
            double directDamage = totalDamage*0.3;
            MakeDamage(enemy, armorDamage);
            enemy.Health -= directDamage;
        }
        //-----------Second skill attack---------//

        protected override (int, int, int, int) StatsUp()
        {
            int hpUp = 100;
            int armorUp = 90;
            int attackUp = 95;
            int manaUp = 8;
            return (hpUp, armorUp, attackUp, manaUp);
        }
        protected override void CheckUniqueSkillLevel()
        {
            //TODO:
        }
        protected override void PerformShieldActivation()
        {
            ActivateWarriorShield();
        }
        private void ActivateWarriorShield()
        {
            Console.WriteLine("Warrior Shield!");
            Console.WriteLine($"Armor bonus:   +[{this.ArmorLimit / 3}]");
            this.Armor += this.ArmorLimit / 3;
            Console.WriteLine($"Current armor: {Armor}");
        }

        protected override void AdditionalInfo()
        {
            Console.WriteLine($"Main attack:            Furious kick");
            Console.WriteLine($"Main attack info:       [{Attack}] damage");
            Console.WriteLine($"Secondary attack:       Smashing blade");
            Console.WriteLine($"Secondary attack info:  [{Attack}] damage to random enemy; 30% - direct attack; 7% - echo");
            Console.WriteLine($"First skill:            Almighty push - range attack");
            Console.WriteLine($"First skill attack:     [{_almightyPushDamage * _almightyPushLevel + Attack/2}] damage");
            Console.WriteLine($"First skill level:      [{ _almightyPushLevel}]");
            Console.WriteLine($"First skill mana cost:  [{ _almightyPushManaCost}]");
            Console.WriteLine($"Second skill:           Bloodlust");
            Console.WriteLine($"Second skill attack:    [{_bloodlustDamage * _bloodlustLevel+Attack/2.5}] damage; 30% - direct attack");
            Console.WriteLine($"Second skill level:     [{ _bloodlustLevel}]");
            Console.WriteLine($"Second skill mana cost: [{ _bloodlustManaCost}]");
        }  
    }
}