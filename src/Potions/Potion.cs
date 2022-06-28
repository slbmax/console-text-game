using coursework.src.Entities;
namespace coursework.src.Potions
{
    abstract public class Potion
    {
        protected string _name;
        abstract public void ActivateEffect(Entity p);
    }
}