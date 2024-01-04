using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInterfaceMenu : GameMenu
{
    [SerializeField] private Button _pauseMenuButton;
    public Button PauseMenuButton
    {
        get {return _pauseMenuButton; }
    }

    [SerializeField] private TextMeshProUGUI _endlessScoreText;
    public TextMeshProUGUI EndlessScoreText
    {
        get { return _endlessScoreText; }
    }

    [SerializeField] private ScoreCounter _scoreCounter;
    public ScoreCounter ScoreCounter
    {
        get { return _scoreCounter; }
    }

    [SerializeField] private EnemyKillCounter _enemyKillCounter;
    public EnemyKillCounter EnemyKillCounter
    {
        get { return _enemyKillCounter; }
    }

    [SerializeField] private TimeCounter _timeCounter;
    public TimeCounter TimeCounter
    {
        get { return _timeCounter; }
    }



}
