using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_WallSlideState : P_TouchingWallState
{
    public P_WallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            core.Movement.SetVelocityY(-playerData.wallSlideVelocity);
            if (grabInput && yInput == 0)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }

        
    }
}
