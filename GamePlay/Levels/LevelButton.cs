using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button _buttonComponent;
    public Button ButtonComponent
    {
        get { return _buttonComponent; } 
    }

    [SerializeField] private TextMeshProUGUI _levelNumberText;
    public TextMeshProUGUI LevelNumberText
    {
        get { return _levelNumberText; }
    }

    [SerializeField] private GameObject _lockImage;
    public GameObject LockImage
    {
        get { return _lockImage; }
    }

}
