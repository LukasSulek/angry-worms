using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : GameMenu
{
    [SerializeField] private Button _mainMenuButton;
    public Button MainMenuButton
    {
        get { return _mainMenuButton; }
    }

    [SerializeField] private Button _musicButton;
    public Button MusicButton
    {
        get { return _musicButton; }
    }

    [SerializeField] private Button _soundButton;
    public Button SoundButton
    {
        get { return _soundButton; }
    }

    

}
