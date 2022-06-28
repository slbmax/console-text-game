using coursework.src.Entities.Enemies;
using System;
using System.Collections.Generic;
namespace coursework.src.Entities.Players
{
    public class Magician : Player
    {
        private int _fireBallLevel = 1;
        private int _fireBallDamage = 500;
        private int _fireBallManaCost = 175;
        private int _iceBallLevel = 1;
        private int _iceBallDamage = 450;
        private int _iceBallManaCost = 150;
        public Magician()
        {
            this.characterType = "Magician";
            this.HealthLimit = 700;
            this.Health = HealthLimit;
            this.ArmorLimit = 700;
            this.Armor = ArmorLimit;
            this.AttackLimit = 350;
            this.Attack = AttackLimit;
            this.ManaLimit = 400;
            this.Mana = ManaLimit;
            this._manaRecovery = 25;
        }
        protected override void PerformMainAttack(Enemy en)
        {
            StaffStrike(en);
        }
        private void StaffStrike(Enemy enemy)
        {
            Console.WriteLine("Staff Strike!");
            MakeDamage(enemy, Attack);
        }
        protected override void PerformSecondaryAttack(List<Enemy> enemies)
        {
            LightningStrike(enemies);
        }
        private void LightningStrike(List<Enemy> enemies)
        {
            Console.WriteLine("Llllightning Strike!");
            double damagePerEnemy = (double)Attack/enemies.Count;
            double armorDamage = damagePerEnemy*0.85;
            double directDamage = damagePerEnemy*0.15;
            foreach(Enemy e in enemies)
            {
                MakeDamage(e, armorDamage);
                e.Health -= directDamage;
            }
        }
        protected override void CheckUniqueSkillLevel()
        {
            if(_level == _fireBallLevel * 3)
            {
                Console.WriteLine("Fireball level: +[1]");
                Console.WriteLine("Iceball level:  +[1]");
                _fireBallLevel++;
                _iceBallLevel++;
                Console.WriteLine($"Current fireball level: [{_fireBallLevel}]");
                Console.WriteLine($"Current iceball level:  [{_fireBallLevel}]");
            }
        }
        protected override void CharacterBonuses()
        {
            double manaCB = ManaLimit*0.05;
            Console.WriteLine($"Mana cashback: +[{manaCB}]");
            Mana += manaCB;
            if(Mana > ManaLimit)
            {
                Mana = ManaLimit;
            }
            Console.WriteLine($"Current Mana:  [{Mana}]");
            double armorBonus = ArmorLimit*0.05;
            Console.WriteLine($"Armor bonus: +[{armorBonus}]");
            Armor += armorBonus;
            Console.WriteLine($"Current armor:  [{Armor}]");
        }
        protected override (int, int, int, int) StatsUp()
        {
            int hpUp = 90;
            int armorUp = 80;
            int attackUp = 85;
            int manaUp = 11;
            return (hpUp, armorUp, attackUp, manaUp);
        }
        protected override double CheckSkillManaCost(bool firstSkill)
        {
            return firstSkill ? _fireBallManaCost : _iceBallManaCost;
        }
        protected override void PerformFirstSkillAttack(List<Enemy> enemies)
        {
            CastFireBall(enemies);
        }
        private void CastFireBall(List<Enemy> enemies)
        {
            Console.WriteLine("Katon! Gokakyu no Jutsu!");
            double totalDamage = _fireBallDamage * _fireBallLevel * 0.95 + Attack/2;
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
            double armorBonus = ArmorLimit/5.0;
            Console.WriteLine($"Armor bonus:   +[{armorBonus}]");
            Armor += armorBonus;
            Console.WriteLine($"Current armor: [{Armor}]");
            double manaCB = ManaLimit*0.05;
            Console.WriteLine($"Mana cashback: +[{manaCB}]");
            Mana += manaCB;
            Console.WriteLine($"Current Mana:  [{Mana}]");
        }
        protected override void PerformSecondSkillAttack(Enemy enemy)
        {
            CastIceBall(enemy);
        }
        private void CastIceBall(Enemy enemy)
        {
            Console.WriteLine("Shogyō Mujō!");
            double totalDamage = _iceBallDamage * _iceBallLevel * 0.95 + Attack/2.5;
            MakeDamage(enemy, totalDamage);
        }
        protected override void PerformShieldActivation()
        {
            ActivateMagicalArmor();
        }
        private void ActivateMagicalArmor()
        {
            Console.WriteLine("Magical Armor!");
            Console.WriteLine($"Armor bonus:   +[{this.ArmorLimit / 2}]");
            this.Armor += this.ArmorLimit / 2.5;
            Console.WriteLine($"Current armor: [{Armor}]");
        }
        protected override void AdditionalInfo()
        {
            Console.WriteLine($"Main attack:            Staff strike");
            Console.WriteLine($"Main attack info:       [{Attack}] damage");
            Console.WriteLine($"Secondary attack:       Lightning strike - range attack");
            Console.WriteLine($"Secondary attack info:  [{Attack}] damage; 15% - direct attack");
            Console.WriteLine($"First skill:            Fireball  - range attack");
            Console.WriteLine($"First skill attack:     [{_fireBallDamage * _fireBallLevel * 0.95 + Attack/2}] damage; 30% - direct attack");
            Console.WriteLine($"First skill level:      [{ _fireBallLevel}]");
            Console.WriteLine($"First skill mana cost:  [{ _fireBallManaCost}]");
            Console.WriteLine($"Second skill:           Iceball");
            Console.WriteLine($"Second skill attack:    [{_iceBallDamage * _iceBallLevel * 0.95 + Attack/2.5}] damage");
            Console.WriteLine($"Second skill level:     [{ _iceBallLevel}]");
            Console.WriteLine($"Second skill mana cost: [{ _iceBallManaCost}]");
        }    
    }
}