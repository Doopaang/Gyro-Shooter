public class Sender
{
    public static Sender Instance { get; private set; }
    
    public string EnemyName { get; private set; }

    public Sender(string name)
    {
        Instance = this;

        EnemyName = name;
    }
}
