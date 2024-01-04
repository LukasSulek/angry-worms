using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentTime;
    public TextMeshProUGUI CurrentTime
    {
        get { return _currentTime; }
    }

    [SerializeField] private TextMeshProUGUI _targetTime;
    public TextMeshProUGUI TargetTime
    {
        get { return _targetTime; }
    }
}
