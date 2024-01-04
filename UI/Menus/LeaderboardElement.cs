using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class LeaderboardElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _positionText;
    public TextMeshProUGUI PositionText
    {
        get { return _positionText; }
    }

    [SerializeField] private TextMeshProUGUI _playerNameText;
    public TextMeshProUGUI PlayerNameText
    {
        get { return _playerNameText; }
    }

    [SerializeField] private TextMeshProUGUI _playerHighscoreText;
    public TextMeshProUGUI PlayerHighscoreText
    {
        get { return _playerHighscoreText; }
    }

    

}
