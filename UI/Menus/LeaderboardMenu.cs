using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardMenu : GameMenu
{
    [SerializeField] private Button _mainMenuButton;
    public Button MainMenuButton
    {
        get { return _mainMenuButton; }
    }

    void Awake()
    {
        StartCoroutine(DatabaseManager.Instance.LoadLeaderboardData());
    }
}
