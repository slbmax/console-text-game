using coursework.src.Potions;
using coursework.src.Entities.Enemies;
using System;
using System.Collections.Generic;
namespace coursework.src.Entities.Players
{
    abstract public class Player : Entity
    {
        public delegate void Callback(string sender, string value = null);
        public event Callback Notify;
        public string nickName;
        public string characterType {get; protected set;}
        protected int _coins = 0;
        public int Coins
        {
            get => _coins;
            set => _coins = value;
        }
        protected int _experience = 0;
        protected int _level = 1;
        public int Level
        {
            get => _level;
        }
        protected int _manaRecovery;
        public HealingPotion healingPotion;
        public RagePotion ragePotion;
        public ManaPotion manaPotion;
        public Potion mysteriousPotion;
        public bool MainAttack(Enemy en)
        {
            Console.WriteLine($"\n***{nickName}***");
            PerformMainAttack(en);
            Console.WriteLine($"***{nickName}***");
            Console.WriteLine($"{characterType} bonuses:");
            CharacterBonuses();
            RecoverMana();
            return true;
        }
        abstract protected void PerformMainAttack(Enemy enemy);
        protected void RecoverMana()
        {
            Console.Write("Mana recovery: ");
            int recoveredMana = _manaRecovery + ((_level-1)*2);
            Console.WriteLine($"+[{recoveredMana }]");
            this.Mana += recoveredMana;
            if(Mana > ManaLimit)
            {
                Mana = ManaLimit;
            }
            Console.WriteLine($"Current mana:  [{Mana}]");
        }
        public bool SecondaryAttack(List<Enemy> enemies)
        {
            Console.WriteLine($"\n***{nickName}***");
            PerformSecondaryAttack(enemies);
            Console.WriteLine($"***{nickName}***");
            Console.WriteLine($"{characterType} bonuses:");
            CharacterBonuses();
            RecoverMana();
            return true;
        }
        abstract protected void PerformSecondaryAttack(List<Enemy> enemies);
        public bool FirstSkillAttack(List<Enemy> enemies)
        {
            if(!CheckManaPoints(true))
            {
                return false;
            }
            Console.WriteLine($"\n***{nickName}***");
            PerformFirstSkillAttack(enemies);
            Console.WriteLine($"{characterType} bonuses:");
            CharacterBonuses();
            Console.WriteLine("Bonus for using skills:");
            ActivateSkillAttackBonus();
            Console.WriteLine($"***{nickName}***");
            return true;
        }
        abstract protected void PerformFirstSkillAttack(List<Enemy> enemies);
        protected bool CheckManaPoints(bool firstSkill)
        {
            double skillManaCost = CheckSkillManaCost(firstSkill);
            if(Mana < skillManaCost)
            {
                Console.WriteLine("Not enough mana to cast skill attack");
                return false;
            }
            Mana -= skillManaCost;
            return true;
        }
        abstract protected double CheckSkillManaCost(bool firstSkill);
        abstract protected void ActivateSkillAttackBonus();
        public bool SecondSkillAttack(Enemy en)
        {
            if(!CheckManaPoints(false))
            {
                return false;
            }
            Console.WriteLine($"\n***{nickName}***");
            PerformSecondSkillAttack(en);
            Console.WriteLine($"{characterType} bonuses:");
            CharacterBonuses();
            Console.WriteLine("Bonus for using skills:");
            ActivateSkillAttackBonus();
            Console.WriteLine($"***{nickName}***");
            return true;
        }
        abstract protected void PerformSecondSkillAttack(Enemy enemy);
        public void EarnAwards(int cns,int exp)
        {
            Console.WriteLine($"\nEarned coins: {cns}");
            _coins += cns;
            Console.WriteLine($"Current balance: {_coins}");
            Console.WriteLine($"Earned exp: {exp}");
            _experience += exp;
            Console.WriteLine($"Current exp: {_experience}");
            if(_experience >= _level * 500 && _experience !=0)
            {
                _level++;
                Console.WriteLine("\n[---------------Level Up---------------]");
                Console.WriteLine($"You reached {_level} level!");
                Console.WriteLine("Stats up:");
                (int,int,int,int) valuesToUp = StatsUp();
                this.HealthLimit += valuesToUp.Item1;
                this.Health = HealthLimit;
                this.ArmorLimit += valuesToUp.Item2;
                this.Armor = ArmorLimit;
                this.AttackLimit += valuesToUp.Item3;
                this.Attack = AttackLimit;
                this.ManaLimit += valuesToUp.Item4;
                Console.WriteLine($"Max hp points:     +{valuesToUp.Item1} [{HealthLimit}]");
                Console.WriteLine($"Max armor points:  +{valuesToUp.Item2} [{ArmorLimit}]");
                Console.WriteLine($"Max attack points: +{valuesToUp.Item3} [{AttackLimit}]");
                Console.WriteLine($"Max mana points:   +{valuesToUp.Item4} [{ManaLimit}]");
                CheckUniqueSkillLevel();
                Console.WriteLine("[---------------Level Up---------------]");
                Notify.Invoke(this.ToString(), "level");
            }
        }
        abstract protected (int,int,int,int) StatsUp();
        abstract protected void CheckUniqueSkillLevel();
        public override bool DrinkHealingPotion()
        {
            if(healingPotion == null)
            {
                Console.WriteLine("There aren`t any healing potions in your inventory");
                return false;
            }
            if(Health == HealthLimit)
            {
                Console.WriteLine("You can`t drink healing potion - you health points are on maximum");
                return false;
            }
            healingPotion.ActivateEffect(this);
            healingPotion = null;
            RecoverMana();
            Notify.Invoke(this.ToString(), "potion");
            return true;
        }
        public override bool DrinkRagePotion()
        {
            if(ragePotion == null)
            {
                Console.WriteLine("There aren`t any rage potions in your inventory");
                return false;
            }
            ragePotion.ActivateEffect(this);
            ragePotion = null;
            RecoverMana();
            Notify.Invoke(this.ToString(), "potion");
            return true;
        }
        public bool DrinkManaPotion()
        {
            if(manaPotion == null)
            {
                Console.WriteLine("There aren`t any mana potions in your inventory");
                return false;
            }
            if(Mana == ManaLimit)
            {
                Console.WriteLine("You can`t drink mana potion - you mana points are on maximum");
                return false;
            }
            manaPotion.ActivateEffect(this);
            manaPotion = null;
            RecoverMana();
            Notify.Invoke(this.ToString(), "potion");
            return true;
        }
        public bool DrinkMysteriousPotion()
        {
            if(mysteriousPotion == null)
            {
                Console.WriteLine("There aren`t any mysterious potions in your inventory");
                return false;
            }
            Console.WriteLine("MmMMmMmMYSTERIOUS POTION!");
            mysteriousPotion.ActivateEffect(this);
            mysteriousPotion = null;
            RecoverMana();
            Notify.Invoke(this.ToString(), "potion");
            return true;
        }
        public override bool ActivateShield()
        {
            Console.WriteLine($"\n***{nickName}***");
            PerformShieldActivation();
            Console.WriteLine($"***{nickName}***");
            CharacterBonuses();
            RecoverMana();
            return true;
        }
        abstract protected void PerformShieldActivation();
        public override void ShowInfo()
        {
            Console.WriteLine($"\n[---------------{nickName}---------------]");
            Console.WriteLine($"Character type:         [{characterType}]");
            Console.WriteLine($"Level:                  [{Level}]");
            Console.WriteLine($"Health:                 [{Health}/{HealthLimit}]");
            Console.WriteLine($"Armor:                  [{Armor}/{ArmorLimit}]");
            Console.WriteLine($"Mana:                   [{Mana}/{ManaLimit}]");
            Console.WriteLine($"Mana recovery:          [{_manaRecovery + ((_level-1)*2)}] per attack");
            Console.WriteLine($"Potions:");
            char hp, rage, mana, myst;
            hp = healingPotion == null ? '-' : '+';
            rage = ragePotion == null ? '-' : '+';
            mana = manaPotion == null ? '-' : '+';
            myst = mysteriousPotion == null ? '-' : '+';
            Console.WriteLine($"  - Healing potion      [{hp}]");
            Console.WriteLine($"  - Rage potion         [{rage}]");
            Console.WriteLine($"  - Mana potion         [{mana}]");
            Console.WriteLine($"  - Mysterious potion   [{myst}]");
            AdditionalInfo();
            Console.WriteLine($"Experience:             [{_experience}]");
            Console.WriteLine($"Balance:                [{Coins}] coins");
            Console.WriteLine($"[---------------{nickName}---------------]");
        }
        abstract protected void CharacterBonuses();
        public void RestoreStats()
        {
            this.Health = HealthLimit;
            this.Armor = ArmorLimit;
            this.Attack = AttackLimit; 
        }
    }
}