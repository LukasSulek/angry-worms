using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : GameMenu
{
    [SerializeField] private Button _playEndlessButton;
    public Button PlayEndlessButton
    {
        get { return _playEndlessButton; }
    }

    [SerializeField] private Button _levelSelectMenuButton;
    public Button LevelSelectMenuButton
    {
        get { return _levelSelectMenuButton; }
    }

    [SerializeField] private Button _settingsMenuButton;
    public Button SettingsMenuButton
    {
        get { return _settingsMenuButton; }
    }

    [SerializeField] private Button _leaderboardMenuButton;
    public Button LeaderboardMenuButton
    {
        get { return _leaderboardMenuButton; }
    }

    /*
    [SerializeField] private Button _achievementMenuButton;
    public Button AchievementMenuButton
    {
        get { return _achievementMenuButton; }
    }
    */
    
    [SerializeField] private TextMeshProUGUI _highscoreText;
    public TextMeshProUGUI HighscoreText
    {
        get { return _highscoreText; }
    }

  



}
