using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyKillCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentEnemiesKilled;
    public TextMeshProUGUI CurrentEnemiesKilled
    {
        get { return _currentEnemiesKilled; }
    }

    [SerializeField] private TextMeshProUGUI _targetEnemiesKilled;
    public TextMeshProUGUI TargetEnemiesKilled
    {
        get { return _targetEnemiesKilled; }
    }




}
