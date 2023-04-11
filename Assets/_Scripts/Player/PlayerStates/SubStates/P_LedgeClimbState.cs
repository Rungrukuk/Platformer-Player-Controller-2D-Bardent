using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_LedgeClimbState : PlayerState
{
    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;

    private bool isHanging;
    private bool isClimbing;
    private bool jumpInput;
    private bool isTouchingCeiling;

    private int xInput;
    private int yInput;

    public P_LedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.Anim.SetBool("climbLedge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        isHanging = true;
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityZero();
        player.transform.position = detectedPos;
        cornerPos = player.DetermineCornerPosition();

        startPos.Set(cornerPos.x - (player.FacingDirection * playerData.startOffset.x),cornerPos.y - playerData.startOffset.y);
        stopPos.Set(cornerPos.x + (player.FacingDirection * playerData.stopOffset.x), cornerPos.y + playerData.stopOffset.y);

        player.transform.position = startPos;
    }

    public override void Exit()
    {
        base.Exit();
        isHanging = false;
        if (isClimbing)
        {
            player.transform.position = stopPos;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (!isTouchingCeiling)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }

        }
        else
        {
            xInput = player.InputHandler.NormalizedInputX;
            yInput = player.InputHandler.NormalizedInputY;
            jumpInput = player.InputHandler.JumpInput;
            player.SetVelocityZero();
            player.transform.position = startPos;

            if ((xInput == player.FacingDirection || yInput == 1) && isHanging && !isClimbing)
            {
                CheckForSpace();
                isClimbing = true;
                player.Anim.SetBool("climbLedge", true);
            }
            else if (yInput == -1 && isHanging && !isClimbing)
            {
                stateMachine.ChangeState(player.InAirState);
            }
            else if (jumpInput&&!isClimbing)
            {
                player.WallJumpState.DetermineWallJumpDirection(true);
                stateMachine.ChangeState(player.WallJumpState);
            }
        }

        
    }

    public void SetDetectedPosition(Vector2 Pos)=>detectedPos = Pos;
    private void CheckForSpace()
    {
        isTouchingCeiling = Physics2D.Raycast(cornerPos + (Vector2.up * 0.015f) + (Vector2.right * player.FacingDirection * 0.015f), Vector2.up, playerData.standColliderHeight, playerData.whatIsGround);
        player.Anim.SetBool("isTouchingCeiling", isTouchingCeiling);
    }
}
