using coursework.Entities.Enemies;
namespace coursework.Entities.Players
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
            this.HealthLimit = 850;
            this.Health = HealthLimit;
            this.ArmorLimit = 850;
            this.Armor = ArmorLimit;
            this.AttackLimit = 375;
            this.Attack = AttackLimit;
            this.ManaLimit = 330;
            this.Mana = ManaLimit;
        }

        //-----------Main Attack + Recover Mana---------//
        protected override void PerformMainAttack(Enemy en)
        {
            EvilPotion(en);
        }
        protected void EvilPotion(Enemy enemy)
        {
            Console.WriteLine("Evil Potion! hihihahaha!!");
            MakeDamage(enemy, Attack);
        }
        protected override void RecoverMana()
        {
            //TODO:
            this.Mana += 4*_level;
            if(Mana > ManaLimit)
            {
                Mana = ManaLimit;
            }
        }
        //-----------Main Attack + Recover Mana---------//
        
        //-----------Secondary Attack---------//
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
        //-----------Secondary Attack---------//
        


        //-----------First skill attack---------//
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
            //_armor += 5*Level;
            Mana += 5 * Level;
            if(Mana > ManaLimit)
            {
                Mana = ManaLimit;
            }
        }
        //-----------First skill attack---------//


        //-----------Second skill attack---------//
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
        //-----------Second skill attack---------//

        protected override void StatsUp()
        {
            //TODO:
        }
        protected override void CheckUniqueSkillLevel()
        {
            //TODO:
        }
        

        protected override void PerformShieldActivation()
        {
            DrinkArmorPotion();
        }
        private void DrinkArmorPotion()
        {
            Console.WriteLine("Armor Potion! wihihihi!");
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
            Console.WriteLine($"Mana recovery:          [...] per attack");
        }
        
    }
}