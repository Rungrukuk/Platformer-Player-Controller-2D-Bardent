using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AttackState : P_AbilityState
{
    private Weapon weapon;
    public P_AttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        weapon.EnterWeapon();
    }
    public override void Exit()
    {
        base.Exit();
        weapon.ExitWeapon();
    }
    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.InitializeWeapon(this);
    }
    #region Animation Triggers

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }
    #endregion
}
