[System.Serializable]
public class PlayerData
{
    private int _highscore;
    public int Highscore
    {
        get { return _highscore; }
        set { _highscore = value; }
    }

    private string _playerName;
    public string PlayerName
    {
        get { return _playerName; }
        set { _playerName = value; }
    }

    private int _musicValue;
    public int MusicValue
    {
        get { return _musicValue; }
        set { _musicValue = value; }
    }

    private int _soundValue;
    public int SoundValue
    {
        get { return _soundValue; }
        set { _soundValue = value; }
    }

    private int _maxLevelReached;
    public int MaxLevelReached
    {
        get { return _maxLevelReached; }
        set { _maxLevelReached = value; }
    }

    public PlayerData(AudioManager audioManager)
    {
        MusicValue = audioManager.MusicValue;
        SoundValue = audioManager.SoundValue;
    }

    public PlayerData(ScoreManager scoreManager)
    {
        Highscore = scoreManager.Highscore;
    }

    public PlayerData(DatabaseManager databaseManager)
    {
        PlayerName = databaseManager.PlayerName;
    }

    public PlayerData(MenuManager menuManager)
    {
        MaxLevelReached = menuManager.MaxLevelReached;
    }



}
