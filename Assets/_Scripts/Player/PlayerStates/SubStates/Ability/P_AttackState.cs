using System.Collections;
using System.Collections.Generic;
using _Scripts.Player.PlayerStates.SuperStates;
using UnityEngine;

public class P_AttackState : P_AbilityState
{
    private Weapon weapon;

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
            if(Movement)
            Movement.CheckIfShouldFlip(xInput);
        }

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormalizedInputX;
        if(setVelocity)
        {
            if(Movement)
            Movement.SetVelocityX(velocityToSet*Movement.FacingDirection);
        }
    }
    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.InitializeWeapon(this,core);
    }

    public void SetPlayerVelocity(float velocity)
    {
        if (Movement)
            Movement.SetVelocityX(velocity * Movement.FacingDirection);
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
