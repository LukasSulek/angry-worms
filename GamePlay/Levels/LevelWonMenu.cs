using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelWonMenu : GameMenu
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

}
