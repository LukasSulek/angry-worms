using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Sprite _soundOnImage;
    [SerializeField] private Sprite _soundOffImage;
    [SerializeField] private Image _imageComponent;

    [SerializeField] private Button _buttonComponent;

    void Start()
    {
        ChangeButtonImage();
        _buttonComponent.onClick.AddListener(ChangeButtonImage);
    }

    void ChangeButtonImage()
    {
        if(AudioManager.Instance.SoundValue == 1)
        {
            _imageComponent.sprite = _soundOnImage;
        }
        else if(AudioManager.Instance.SoundValue == 0)
        {
            _imageComponent.sprite = _soundOffImage;
        }
    }


}
