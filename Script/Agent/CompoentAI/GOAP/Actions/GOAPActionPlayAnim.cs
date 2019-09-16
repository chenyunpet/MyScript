using System;
using System.Collections.Generic;
using UnityEngine;
public class GOAPActionPlayAnim:GOAPAction
{
    private AgentActionPlayAnim Action;
    public GOAPActionPlayAnim( Agent owner):base(E_GOAPAction.playAnim, owner)
    {

    }
    public override void InitAction()
    {
        WorldEffects.SetWSProperty(E_PropKey.E_PLAY_ANIM,false);
        Cost = 1;
        Interruptible = false;
    }
    public override void Activate()
    {
        base.Activate();
        Action = AgentActionFactory.Create(AgentActionFactory.E_Type.E_PLAY_ANIM) as AgentActionPlayAnim;
        Action.AnimName = Owner.BlackBoard.DesiredAnimName;
        Owner.BlackBoard.AddAction(Action);
    }
    public override void Deactivate()
    {
        Owner.WorldState.SetWSProperty(E_PropKey.E_PLAY_ANIM,false);
        base.Deactivate();
    }
    public override bool IsActionComplete()
    {
        if (Action.IsActive() == false)
            return true;
        return false;
    }
}

