using System;
using System.Collections.Generic;
using UnityEngine;
public class GOAPActionWeaponShow : GOAPAction
{
    private AgentActionWeaponShow Action;
    public GOAPActionWeaponShow(Agent owner) : base(E_GOAPAction.weaponShow, owner)
    {

    }
    public override void InitAction()
    {
        WorldEffects.SetWSProperty(E_PropKey.E_WEAPON_IN_HANDS, true);
        Cost = 1;
        Interruptible = false;
    }
    public override void Activate()
    {
        base.Activate();
        Owner.BlackBoard.WeaponState = E_WeaponState.Ready;
        Action = AgentActionFactory.Create(AgentActionFactory.E_Type.E_WEAPON_SHOW) as AgentActionWeaponShow;
        Action.Show = true;
        Owner.BlackBoard.AddAction(Action);
    }
    public override void Deactivate()
    {
        Owner.WorldState.SetWSProperty(E_PropKey.E_WEAPON_IN_HANDS, true);
        base.Deactivate();
    }
    public override bool IsActionComplete()
    {
        if (Action.IsActive() == false)
            return true;
        return false;
    }
}





