using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverMenu : GameMenu
{
    [SerializeField] private Button _retryButton;
    public Button RetryButton
    {
        get { return _retryButton; }
    }

    [SerializeField] private Button _mainMenuButton;
    public Button MainMenuButton
    {
        get { return _mainMenuButton; }
    }

    [SerializeField] private ScoreCounter _endlessCounter;
    public ScoreCounter EndlessCounter
    {
        get { return _endlessCounter; }
    }

    [SerializeField] private ScoreCounter _levelScoreCounter;
    public ScoreCounter LevelScoreCounter
    {
        get { return _levelScoreCounter; }
    }

    [SerializeField] private TimeCounter _levelTimeCounter;
    public TimeCounter LevelTimeCounter
    {
        get { return _levelTimeCounter; }
    }

    [SerializeField] private EnemyKillCounter _levelKillCounter;
    public EnemyKillCounter LevelKillCounter
    {
        get { return _levelKillCounter; }
    }

}
