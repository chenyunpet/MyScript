
using System;
using System.Collections.Generic;
using UnityEngine;


public class GOAPGoalPlayAnim : GOAPGoal
{
    public GOAPGoalPlayAnim(Agent ai) : base(E_GOAPGoals.E_PLAY_ANIM, ai)
    {

    }

    public override void SetWSSatisfactionForPlanning(WorldState worldState)
    {
        worldState.SetWSProperty(E_PropKey.E_PLAY_ANIM,false);
    }
    public override bool IsWSSatisfiedForPlanning(WorldState worldState)
    {
        WorldStateProp prop = Owner.WorldState.GetWSProperty(E_PropKey.E_PLAY_ANIM);
        if (prop != null && prop.GetBool() == false)
        {
            return true;
        }
        return false;
    }
    public override float GetMaxRelevancy()
    {
        return Owner.BlackBoard.GOAP_PlayAnimRelevancy;
    }
    public override void CalculateGoalRelevancy()
    {
        WorldStateProp prop = Owner.WorldState.GetWSProperty(E_PropKey.E_PLAY_ANIM);
        if(prop!=null && prop.GetBool()==true)
        {
            GoalRelevancy = Owner.BlackBoard.GOAP_PlayAnimRelevancy;
        }
        else
        {
            GoalRelevancy = 0;
        }
    }

    public override bool IsSatisfied()
    {
        return IsPlanFinished();
    }
    public override void InitGoal()
    {

    }
    public override void SetDisableTime()
    {
        //base.SetDisableTime();
        NextEvaluationTime = Owner.BlackBoard.GOAP_PlayAnimDelay + Time.timeSinceLevelLoad;
    }
}

