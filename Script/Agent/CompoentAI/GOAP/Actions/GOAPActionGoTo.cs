using System;
using System.Collections.Generic;
using UnityEngine;
public class GOAPActionGoTo : GOAPAction
{
    private AgentActionGoTo Action;
    private Vector3 FinalPos;
    public GOAPActionGoTo(Agent owner) : base(E_GOAPAction.move, owner)
    {

    }
    public override void InitAction()
    {
        Debug.Log("InitAction");
        WorldEffects.SetWSProperty(E_PropKey.E_AT_TARGET_POS, true);
        Cost = 5;
        Precedence = 70;
    }
    public override void Update()
    {
        if (FinalPos != Owner.BlackBoard.DesiredPosition)
        {
            ActionGoTo(Owner.BlackBoard.DesiredPosition);
        }
    }
    public override void Activate()
    {
        base.Activate();
        ActionGoTo(Owner.BlackBoard.DesiredPosition);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        Debug.Log("GOAPActionGoTo  Deactivate");
        Action.Motion = E_MotionType.None;
        Owner.WorldState.SetWSProperty(E_PropKey.E_AT_TARGET_POS, true);
    }
    public override bool IsActionComplete()
    {
        if (Action != null && Action.IsActive() == false)
            return true;
        return false;
    }
    public override bool ValidateAction()
    {
        if (Action != null && Action.IsFailed() == true)
            return false;
        return true;
    }
    private void ActionGoTo(Vector3 finalPos)
    {
        FinalPos = finalPos;
        Action = AgentActionFactory.Create(AgentActionFactory.E_Type.E_GOTO) as AgentActionGoTo;
        Action.MoveType = AgentActionGoTo.E_MoveType.E_MT_FORWARD;
        Action.Motion = E_MotionType.Run;
        Action.FinalPosition = FinalPos;
        Owner.BlackBoard.AddAction(Action);
        UnityEngine.Debug.Log(this.ToString() + "Send new goto action to pos " + FinalPos.ToString());

    }

}


