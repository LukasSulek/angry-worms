using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionGoalTime : CompletionGoal
{
    [SerializeField] private EnemyGenerator _enemyGenerator;

    public override void GoalCompletion()
    {
        if(_enemyGenerator.TimePlaying >= Amount)
        {
            IsCompleted = true;
        }
    }




}
