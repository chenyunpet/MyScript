using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract  class AnimSet:MonoBehaviour
{
    public abstract string GetIdleAnim();
    public abstract string GetMoveAnim(E_MotionType motion, E_MoveType move, E_WeaponType weapon, E_WeaponState weaponState);
    public abstract string GetShowWeaponAnim();
    public abstract string GetHideWeaponAnim();
}

public class AnimAttackData:System.Object
{
    /// <summary>
    /// 动画名字
    /// </summary>
    public string AnimName;
    /// <summary>
    /// 移动距离
    /// </summary>
    public float MoveDistance;
    /// <summary>
    ///Timer 
    /// </summary>
    public float AttackMoveStartTime;
    public float AttackMoveEndTime;
    public float AttackEndTime;

    //Hit
    public float HitTime;
    public float HitDamage;
    public float HitAngle;
    public AnimAttackData(string animName,float moveDistance,float hitTime,float attackEndTime,float hitDamage,float hitAngle)
    {
        AnimName = animName;
        MoveDistance = moveDistance;
        AttackMoveStartTime = 0;
        AttackMoveEndTime = attackEndTime * 0.7f;
        AttackEndTime = attackEndTime;
        HitTime = hitTime;
        HitDamage = hitDamage;
        HitAngle = hitAngle;

    }

}
