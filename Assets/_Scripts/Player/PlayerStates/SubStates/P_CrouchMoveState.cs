using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_CrouchMoveState : P_GroundedState
{
    private bool isTouchingCeiling;
    public P_CrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetColliderHeight(playerData.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetColliderHeight(playerData.standColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isTouchingCeiling = player.CheckIfTouchingCeiling();
        if (!isExitingState)
        {
            player.SetVelocityX(playerData.crouchMovementVelocity * player.FacingDirection);
            player.CheckIfShouldFlip(xInput);
            if (xInput == 0)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else if (yInput != -1 && !isTouchingCeiling)
            {
                stateMachine.ChangeState(player.MoveState);
            }
        }
    }
}
