using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_LandState : P_GroundedState
{

    public P_LandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else if (isAnimationFinished)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }

    }
}
