using coursework.src.Entities.Enemies;
using System;
using System.Collections.Generic;
namespace coursework.src.Entities.Players
{
    public class Witch : Player
    {
        private int _darknessSpellLevel = 1;
        private int _darknessSpellDamage = 325;
        private int _darknessSpellManaCost = 150;
        private int _soulCurseLevel = 1;
        private int _soulCurseDamage = 350;
        private int _soulCurseManaCost = 125;
        public Witch()
        {
            this.characterType = "Witch";
            this.HealthLimit = 850;
            this.Health = HealthLimit;
            this.ArmorLimit = 850;
            this.Armor = ArmorLimit;
            this.AttackLimit = 375;
            this.Attack = AttackLimit;
            this.ManaLimit = 330;
            this.Mana = ManaLimit;
            this._manaRecovery = 20;
        }
        protected override void PerformMainAttack(Enemy en)
        {
            EvilPotion(en);
        }
        protected void EvilPotion(Enemy enemy)
        {
            Console.WriteLine("Evil Potion! hihihahaha!!");
            MakeDamage(enemy, Attack);
        }
        protected override void PerformSecondaryAttack(List<Enemy> enemies)
        {
           ExplosivePotion(enemies);
        }
        private void ExplosivePotion(List<Enemy> enemies)
        {
            Console.WriteLine("Explosive Potion! hihihahaha!!");
            double damagePerEnemy = (double)Attack/enemies.Count;
            double armorDamage = damagePerEnemy*0.9;
            double directDamage = damagePerEnemy*0.1;
            foreach(Enemy e in enemies)
            {
                MakeDamage(e, armorDamage);
                e.Health -= directDamage;
            }
        }
        protected override double CheckSkillManaCost(bool firstSkill)
        {
            return firstSkill ? _darknessSpellManaCost : _soulCurseManaCost;
        }
        protected override void PerformFirstSkillAttack(List<Enemy> enemies)
        {
            CastDarknessSpell(enemies);
        }
        private void CastDarknessSpell(List<Enemy> enemies)
        {
            Console.WriteLine("Darkness spell!! wihihahaha!");
            double totalDamage = _darknessSpellDamage * _darknessSpellLevel+ Attack/2;
            double damagePerEnemy = (double)totalDamage/enemies.Count;
            double armorDamage = damagePerEnemy*0.85;
            double directDamage = damagePerEnemy*0.15;
            foreach(Enemy e in enemies)
            {
                MakeDamage(e, armorDamage);
                e.Health -= directDamage;
            }
        }
        protected override void ActivateSkillAttackBonus()
        {
            double manaCB = ManaLimit*0.05;
            Console.WriteLine($"Mana cashback: +[{manaCB}]");
            Mana += manaCB;
            Console.WriteLine($"Current Mana:  [{Mana}]");
        }
        protected override void PerformSecondSkillAttack(Enemy enemy)
        {
            CastSoulCurse(enemy);
        }
        private void CastSoulCurse(Enemy enemy)
        {
            Console.WriteLine("Your soul will be mine soon! hihihihi! Soul curse!");
            double totalDamage = _soulCurseDamage * _soulCurseLevel+ Attack/2.5;
            double armorDamage = totalDamage*0.5;
            double directDamage = totalDamage*0.5;
            MakeDamage(enemy, armorDamage);
            enemy.Health -= directDamage;
        }
        protected override (int, int, int, int) StatsUp()
        {
            int hpUp = 95;
            int armorUp = 90;
            int attackUp = 95;
            int manaUp = 9;
            return (hpUp, armorUp, attackUp, manaUp);
        }
        protected override void CheckUniqueSkillLevel()
        {
            if(_level == _soulCurseLevel * 4)
            {
                Console.WriteLine("Darkness spell level: +[1]");
                Console.WriteLine("Soul curse level:     +[1]");
                _soulCurseLevel++;
                _darknessSpellLevel++;
                Console.WriteLine($"Current darkness spell level: [{_soulCurseLevel}]");
                Console.WriteLine($"Current soul curse level:     [{_darknessSpellLevel}]");
            }
        }
        protected override void CharacterBonuses()
        {
            double armorBonus = ArmorLimit*0.05;
            Console.WriteLine($"Armor bonus: +[{armorBonus}]");
            Armor += armorBonus;
            Console.WriteLine($"Current armor:  [{Armor}]");
        }
        protected override void PerformShieldActivation()
        {
            DrinkArmorPotion();
        }
        private void DrinkArmorPotion()
        {
            Console.WriteLine("Armor Potion! wihihihi!");
            Console.WriteLine($"Armor bonus:   +[{this.ArmorLimit / 2.75}]");
            this.Armor += this.ArmorLimit / 2.75;
            Console.WriteLine($"Current armor: {Armor}");
        }
        protected override void AdditionalInfo()
        {
            Console.WriteLine($"Main attack:            Evil potion");
            Console.WriteLine($"Main attack info:       [{Attack}] damage");
            Console.WriteLine($"Secondary attack:       Explosive potion - range attack");
            Console.WriteLine($"Secondary attack info:  [{Attack}] damage; 10% -direct attack");
            Console.WriteLine($"First skill:            Darkness spell - range attack");
            Console.WriteLine($"First skill attack:     [{_darknessSpellDamage * _darknessSpellLevel+ Attack/2}] damage; 15% - direct attack");
            Console.WriteLine($"First skill level:      [{ _darknessSpellLevel}]");
            Console.WriteLine($"First skill mana cost:  [{ _darknessSpellManaCost}]");
            Console.WriteLine($"Second skill:           Soul curse");
            Console.WriteLine($"Second skill attack:    [{_soulCurseDamage * _soulCurseLevel+ Attack/2.5}] damage; 50% - direct attack");
            Console.WriteLine($"Second skill level:     [{ _soulCurseLevel}]");
            Console.WriteLine($"Second skill mana cost: [{ _soulCurseManaCost}]");
        }      
    }
}