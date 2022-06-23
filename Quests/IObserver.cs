namespace coursework.Quests
{
    abstract public class IObserver
    {
        abstract protected void Update(string msgSender, string value);
    }
}