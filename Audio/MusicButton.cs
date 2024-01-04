using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    [SerializeField] private Sprite _musicOnImage;
    [SerializeField] private Sprite _musicOffImage;
    [SerializeField] private Image _imageComponent;

    [SerializeField] private Button _buttonComponent;

    void Start()
    {
        ChangeButtonImage();
        _buttonComponent.onClick.AddListener(ChangeButtonImage);
    }

    void ChangeButtonImage()
    {
        if(AudioManager.Instance.MusicValue == 1)
        {
            _imageComponent.sprite = _musicOnImage;
        }
        else if(AudioManager.Instance.MusicValue == 0)
        {
            _imageComponent.sprite = _musicOffImage;
        }
    }
}
