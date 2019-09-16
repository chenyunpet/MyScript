using System;
using System.Collections.Generic;
using UnityEngine;
public class GOAPActionWeaponHide : GOAPAction
{
    private AgentActionWeaponShow Action;
    public GOAPActionWeaponHide(Agent owner) : base(E_GOAPAction.weaponHide, owner)
    {

    }
    public override void InitAction()
    {
        WorldEffects.SetWSProperty(E_PropKey.E_WEAPON_IN_HANDS, false);
        Cost = 1;
        Interruptible = false;
    }
    public override void Activate()
    {
        base.Activate();
        Owner.BlackBoard.WeaponState = E_WeaponState.Ready;
        Action = AgentActionFactory.Create(AgentActionFactory.E_Type.E_WEAPON_SHOW) as AgentActionWeaponShow;
        Action.Show = false;
        Owner.BlackBoard.AddAction(Action);
    }
    public override void Deactivate()
    {
        Owner.WorldState.SetWSProperty(E_PropKey.E_WEAPON_IN_HANDS, false);
        base.Deactivate();
    }
    public override bool IsActionComplete()
    {
        if (Action.IsActive() == false)
            return true;
        return false;
    }

}




