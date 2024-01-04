using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectMenu : GameMenu
{
    [SerializeField] private Button _mainMenuButton;
    public Button MainMenuButton
    {
        get { return _mainMenuButton; }
    }

    [SerializeField] private LevelSelector _levelSelector;
    public LevelSelector LevelSelector
    {
        get { return _levelSelector; }
    }
}
