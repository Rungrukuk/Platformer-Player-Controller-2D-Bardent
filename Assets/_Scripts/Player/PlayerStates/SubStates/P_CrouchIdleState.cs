using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_CrouchIdleState : P_GroundedState
{
    private bool isTouchingCeiling;
    public P_CrouchIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocityZero();
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
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.CrouchMoveState);
            }
            else if(yInput != -1 && !isTouchingCeiling)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            
        }
    }
}
