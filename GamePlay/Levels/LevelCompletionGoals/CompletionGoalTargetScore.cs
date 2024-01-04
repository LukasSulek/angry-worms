using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionGoalTargetScore : CompletionGoal
{
    public override void GoalCompletion()
    {
        if(ScoreManager.Instance.Score >= Amount)
        {
            IsCompleted = true;
        }
    }    
}
