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
        isTouchingCeiling = core.CollisionSenses.Ceiling;
        if (!isExitingState)
        {
            core.Movement.SetVelocityX(playerData.crouchMovementVelocity * core.Movement.FacingDirection);
            core.Movement.CheckIfShouldFlip(xInput);
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
