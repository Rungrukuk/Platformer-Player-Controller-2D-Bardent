using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_WallClimbState : P_TouchingWallState
{
    public P_WallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            if(Movement)
                Movement.SetVelocityY(playerData.wallClimbVelocity);
            if (yInput != 1)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }

    }
}
