using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionGoalKillEnemies : CompletionGoal
{
    [SerializeField] private EnemyGenerator _enemyGenerator;

    public override void GoalCompletion()
    {
        if(_enemyGenerator.EnemiesKilled >= Amount)
        {
            IsCompleted = true;
        }
    }




}
