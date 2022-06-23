using coursework.Entities;
namespace coursework.Potions
{
    abstract public class Potion
    {
        protected string _name;
        abstract public void ActivateEffect(Entity p);
    }
}