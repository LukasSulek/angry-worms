using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompletionGoal : MonoBehaviour
{
    [SerializeField] private string _type;
    public string Type
    {
        get { return _type; }
    }

    [SerializeField] private int _amount;
    public int Amount
    {
        get { return _amount; }
        set { _amount = value; }
    }

    private bool _isCompleted = false;
    public bool IsCompleted
    {
        get { return _isCompleted; }
        set
        {
            _isCompleted = value;

            EnemyGenerator.OnCompletionGoalComplete?.Invoke();
        }
    }

    public abstract void GoalCompletion();
}
