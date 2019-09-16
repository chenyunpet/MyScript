using System;
using System.Collections.Generic;
using UnityEngine;


public class GOAPGoalGoTo:GOAPGoal
{
    public GOAPGoalGoTo(Agent ai) : base(E_GOAPGoals.E_GOTO, ai)
    {

    }
  
    public override void SetWSSatisfactionForPlanning(WorldState worldState)
    {
        worldState.SetWSProperty(E_PropKey.E_AT_TARGET_POS, true);
    }
    public override bool IsWSSatisfiedForPlanning(WorldState worldState)
    {
        WorldStateProp prop = worldState.GetWSProperty(E_PropKey.E_AT_TARGET_POS);
        if(prop!=null&& prop.GetBool()==true)
        {
            return true;
        }
        return false;
    }
    public override float GetMaxRelevancy()
    {
        return Owner.BlackBoard.GOAP_GoToRelevancy;
    }
    public override void CalculateGoalRelevancy()
    {
        if (Owner.BlackBoard.MotionType != E_MotionType.None)
        {
            GoalRelevancy = 0;
        }
        else
        {
            WorldStateProp prop = Owner.WorldState.GetWSProperty(E_PropKey.E_AT_TARGET_POS);
            if (prop != null && prop.GetBool() == false)
                GoalRelevancy = Owner.BlackBoard.GOAP_GoToRelevancy;
            else
                GoalRelevancy = 0;
        }
    }
    public override bool IsSatisfied()
    {
        WorldStateProp prop = Owner.WorldState.GetWSProperty(E_PropKey.E_AT_TARGET_POS);
        if(prop!=null&&prop.GetBool()==true)
        {
            return true;
        }
        return false;
    }
    public override void InitGoal()
    {
        
    }
    public override void SetDisableTime()
    {
        NextEvaluationTime = Owner.BlackBoard.GOAP_GoToDelay + Time.timeSinceLevelLoad;
    }
}

