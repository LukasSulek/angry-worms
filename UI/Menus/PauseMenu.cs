using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : GameMenu
{
    [SerializeField] private Button _continueButton;
    public Button ContinueButton
    {
        get { return _continueButton; }
    }

    [SerializeField] private Button _restartButton;
    public Button RestartButton
    {
        get { return _restartButton; }
    }

    [SerializeField] private Button _mainMenuButton;
    public Button MainMenuButton
    {
        get { return _mainMenuButton; }
    }

    [SerializeField] private Button _settingsMenuButton;
    public Button SettingsMenuButton
    {
        get { return _settingsMenuButton; }
    }

}
