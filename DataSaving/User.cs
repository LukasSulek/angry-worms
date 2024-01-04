public class User
{
    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    private int _highscore;
    public int Highscore
    {
        get { return _highscore; }
        set { _highscore = value; }
    }

    public User(string name, int highscore)
    {
        this.Name = name;
        this.Highscore = highscore;
    }
}
