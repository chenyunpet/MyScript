using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public  class AnimFSMPlayer:AnimFSM
{
    enum E_AnimState
    {
        E_Idle,
        E_Move,
        E_Attack,
        E_GOTO,

    }

    public AnimFSMPlayer(Animation _anims,Agent _owner):base(_anims,_owner)
    {

    }
    public override void Initialize()
    {
        AnimStates.Add(new AnimStateIdle(AnimEngine, Owner));
        AnimStates.Add(new AnimStateMove(AnimEngine, Owner));
        AnimStates.Add(new AnimStateAttackMelee(AnimEngine, Owner));
        AnimStates.Add(new AnimStateGoTo(AnimEngine, Owner));
        DefaultAnimState = AnimStates[(int)E_AnimState.E_Idle];
        base.Initialize();

    }
    public override void DoAction(AgentAction _action)
    {
        if(CurrentAnimState.HandleNewAction(_action))
        {
            NextAnimState = null;
        }
        else
        {
            //if (_action is AgentActionIdle)
            //{
            //    NextAnimState = AnimStates[(int)(E_AnimState.E_Idle)];
            //}
            if (_action is AgentActionMove)
            {
                NextAnimState = AnimStates[(int)(E_AnimState.E_Move)];
                Debug.Log("AgentActionMove");
            }
            else if(_action is AgentActionAttack)
            {
                NextAnimState = AnimStates[(int)(E_AnimState.E_Attack)];
                Debug.Log("AgentActionAttack");
            }
            else if(_action is AgentActionGoTo)
            {
                NextAnimState = AnimStates[(int)(E_AnimState.E_GOTO)];
                Debug.Log("AgentActionGoTo");
            }
            if (null!=NextAnimState)
            {
                ProgressToNextStage(_action);
            }

        }
        //base.DoAction(_action);
    }
}

