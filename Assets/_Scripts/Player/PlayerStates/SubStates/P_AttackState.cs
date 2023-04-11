using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AttackState : P_AbilityState
{
    private Weapon weapon;

    private int xInput;

    private float velocityToSet;

    private bool setVelocity;
    private bool shouldCheckFlip;

    public P_AttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        setVelocity = false;
        weapon.EnterWeapon();
    }
    public override void Exit()
    {
        base.Exit();
        weapon.ExitWeapon();
    }
    public override void DoChecks()
    {
        base.DoChecks();
        if(shouldCheckFlip)
        {
            player.CheckIfShouldFlip(xInput);
        }

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormalizedInputX;
        if(setVelocity)
        {
            player.SetVelocityX(velocityToSet*player.FacingDirection);
        }
    }
    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.InitializeWeapon(this);
    }

    public void SetPlayerVelocity(float velocity)
    {
        player.SetVelocityX(velocity * player.FacingDirection);
        velocityToSet = velocity;
        setVelocity = true;
    }

    public void SetFlipCheck(bool value)
    {
        shouldCheckFlip = value;
    }
    
    #region Animation Triggers

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }
    #endregion
}
