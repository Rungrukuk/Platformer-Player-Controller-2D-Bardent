using System.Collections;
using System.Collections.Generic;
using _Scripts.Player.PlayerStates.SuperStates;
using UnityEngine;

public class P_WallJumpState : P_AbilityState
{
    private int wallJumpDirection;
    public P_WallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        if (Movement)
        {
            Movement.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
            Movement.CheckIfShouldFlip(wallJumpDirection);
        }

        player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Movement)
        {
            player.Anim.SetFloat("yVelocity", Movement.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));
        }
        if(Time.time>=startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall && Movement)
            wallJumpDirection = -Movement.FacingDirection;
        else
            wallJumpDirection = Movement.FacingDirection;
    }

}
