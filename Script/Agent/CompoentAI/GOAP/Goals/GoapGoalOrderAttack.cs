using System;
using System.Collections.Generic;
using UnityEngine;


public class GoapGoalOrderAttack : GOAPGoal
{
    public GoapGoalOrderAttack(Agent ai) : base(E_GOAPGoals.E_ORDER_ATTACK, ai)
    {

    }

    public override void SetWSSatisfactionForPlanning(WorldState worldState)
    {
        worldState.SetWSProperty(E_PropKey.E_ATTACK_TARGET, true);
    }
    public override bool IsWSSatisfiedForPlanning(WorldState worldState)
    {
        WorldStateProp prop = worldState.GetWSProperty(E_PropKey.E_ATTACK_TARGET);
        if (prop != null && prop.GetBool() == true)
        {
            return true;
        }
        return false;
    }
    public override float GetMaxRelevancy()
    {
        return Owner.BlackBoard.GOAP_KillTargetRelevancy;
    }
    public override void CalculateGoalRelevancy()
    {
   
        WorldStateProp prop = Owner.WorldState.GetWSProperty(E_PropKey.E_ATTACK_TARGET);

        if (prop != null && prop.GetOrder() == AgentOrder.E_OrderType.E_ATTACK)
        {
            GoalRelevancy = Owner.BlackBoard.GOAP_KillTargetDelay;
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
        NextEvaluationTime = Owner.BlackBoard.GOAP_KillTargetDelay + Time.timeSinceLevelLoad;
    }
    public override bool ReplanRequired()
    {
        if(IsPlanFinished() && Owner.WorldState.GetWSProperty(E_PropKey.E_ORDER).GetOrder()==AgentOrder.E_OrderType.E_ATTACK)
        {
            return true;
        }
        return false;
    }
    public override void Deactivate()
    {
        WorldStateProp prop = Owner.WorldState.GetWSProperty(E_PropKey.E_ORDER);
        if(prop.GetOrder()==AgentOrder.E_OrderType.E_ATTACK)
        {
            Owner.WorldState.SetWSProperty(E_PropKey.E_ORDER,AgentOrder.E_OrderType.E_NONE);
        }
        base.Deactivate();
    }
}

