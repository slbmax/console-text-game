namespace coursework.src.Entities
{
    abstract public class Entity
    {
        protected (double, double) _health;
        public double Health
        {
            get => _health.Item1;
            set => _health.Item1 = value;
        }
        public double HealthLimit
        {
            get => _health.Item2;
            set => _health.Item2 = value;
        }
        protected (double, double) _attack;
        public double Attack
        {
            get => _attack.Item1;
            set => _attack.Item1 = value;
        }
        public double AttackLimit
        {
            get => _attack.Item2;
            set => _attack.Item2 = value;
        }
        protected (double, double) _armor;
        public double Armor
        {
            get => _armor.Item1;
            set => _armor.Item1 = value;
        }
        public double ArmorLimit
        {
            get => _armor.Item2;
            set => _armor.Item2 = value;
        }
        protected (double, double) _mana;
        public double Mana
        {
            get => _mana.Item1;
            set => _mana.Item1 = value;
        }
        public double ManaLimit
        {
            get => _mana.Item2;
            set => _mana.Item2 = value;
        }
        protected void MakeDamage(Entity enemy, double damage)
        {
            enemy.Armor -= damage;
            if(enemy.Armor < 0)
            {
                enemy.Health += enemy.Armor;
                enemy.Armor = 0;
            }
        }
        abstract public bool DrinkHealingPotion();
        abstract public bool DrinkRagePotion();
        abstract public bool ActivateShield();
        abstract public void ShowInfo();
        abstract protected void AdditionalInfo();
    }
}