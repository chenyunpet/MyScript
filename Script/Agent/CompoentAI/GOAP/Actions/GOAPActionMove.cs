using System;
using System.Collections.Generic;
using UnityEngine;
public class GOAPActionMove : GOAPAction
{
    private AgentActionMove Action;
    private Vector3 FinalPos;
    public GOAPActionMove(Agent owner) : base(E_GOAPAction.move, owner)
    {

    }
    public override void InitAction()
    {
        WorldEffects.SetWSProperty(E_PropKey.E_AT_TARGET_POS,true);
        Cost = 5;
        Precedence = 30;
    }
    public override void Update()
    {
        if (WorldEffects.GetWSProperty(E_PropKey.E_AT_TARGET_POS).GetBool()==true)
        {

            Action = AgentActionFactory.Create(AgentActionFactory.E_Type.E_MOVE) as AgentActionMove;
            Action.MoveType = AgentActionMove.E_MoveType.E_MT_FORWARD;
            Owner.BlackBoard.AddAction(Action);
        }
        else
        {
            Debug.Log("AgentActionIdle......");
            AgentActionIdle a = AgentActionFactory.Create(AgentActionFactory.E_Type.E_IDLE) as AgentActionIdle;
            Owner.BlackBoard.AddAction(a);

        }
    }
    public override void Activate()
    {
        base.Activate();
        Action = AgentActionFactory.Create(AgentActionFactory.E_Type.E_MOVE) as AgentActionMove;
        Action.MoveType = AgentActionMove.E_MoveType.E_MT_FORWARD;
        Owner.BlackBoard.AddAction(Action);
    }

    public override void Deactivate()
    {
        Owner.WorldState.SetWSProperty(E_PropKey.E_AT_TARGET_POS, true);
        AgentActionIdle a = AgentActionFactory.Create(AgentActionFactory.E_Type.E_IDLE) as AgentActionIdle;
        Owner.BlackBoard.AddAction(a);
        base.Deactivate();
    }
    public override bool IsActionComplete()
    {
        if (Action!=null&& Action.IsActive() == false)
            return true;
        return false;
    }
    public override bool ValidateAction()
    {
        if (Action != null && Action.IsFailed() == true)
            return false;
        return true;
    }
}


