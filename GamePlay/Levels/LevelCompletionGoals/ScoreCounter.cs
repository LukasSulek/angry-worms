using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentScore;
    public TextMeshProUGUI CurrentScore
    {
        get { return _currentScore; }
    }

    [SerializeField] private TextMeshProUGUI _targetScore;
    public TextMeshProUGUI TargetScore
    {
        get { return _targetScore; }
    }



}
