using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.CoreComponents;
using Unity.VisualScripting;
using UnityEngine;

public class P_LedgeClimbState : PlayerState
{
    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;
    private Vector2 workSpace;

    private bool isHanging;
    private bool isClimbing;
    private bool jumpInput;
    private bool isTouchingCeiling;

    private int xInput;
    private int yInput;

    private Movement Movement{get=>movement??core.GetCoreComponent(ref movement);}

    private Movement movement;

    private CollisionSenses collisionSenses;
    private CollisionSenses CollisionSenses{ get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

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
        if(Movement)
            Movement.SetVelocityZero();
        player.transform.position = detectedPos;
        cornerPos = DetermineCornerPosition();
        player.transform.position = startPos;
        if (Movement)
        {

            startPos.Set(cornerPos.x - (Movement.FacingDirection * playerData.startOffset.x), cornerPos.y - playerData.startOffset.y);
            stopPos.Set(cornerPos.x + (Movement.FacingDirection * playerData.stopOffset.x), cornerPos.y + playerData.stopOffset.y);
        }


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
            Movement.SetVelocityZero();
            player.transform.position = startPos;

            if ((xInput == Movement.FacingDirection || yInput == 1) && isHanging && !isClimbing)
            {
                CheckForSpace();
                isClimbing = true;
                player.Anim.SetBool("climbLedge", true);
            }
            else if ((yInput == -1 || xInput == -1) && isHanging && !isClimbing)
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
    private Vector2 DetermineCornerPosition()
    {
        if(Movement && CollisionSenses)
        {
            RaycastHit2D xHit = Physics2D.Raycast(CollisionSenses.WallCheck.position, Vector2.right * Movement.FacingDirection, CollisionSenses.WallCheckDistance, CollisionSenses.WhatIsGround);
            float xDistance = xHit.distance;
            workSpace.Set((xDistance + 0.015f) * Movement.FacingDirection, 0f);
            RaycastHit2D yHit = Physics2D.Raycast(CollisionSenses.LedgeCheckHorizontal.position + (Vector3)(workSpace), Vector2.down, CollisionSenses.LedgeCheckHorizontal.position.y - CollisionSenses.WallCheck.position.y + 0.015f, CollisionSenses.WhatIsGround);
            float yDistance = yHit.distance;
            workSpace.Set(CollisionSenses.WallCheck.position.x + (xDistance * Movement.FacingDirection), CollisionSenses.LedgeCheckHorizontal.position.y - yDistance);
        }

        return workSpace;
    }
    public void SetDetectedPosition(Vector2 Pos)=>detectedPos = Pos;
    private void CheckForSpace()
    {
        if(Movement && CollisionSenses)
        isTouchingCeiling = Physics2D.Raycast(cornerPos + (Vector2.up * 0.015f) + (Vector2.right * Movement.FacingDirection * 0.015f), Vector2.up, playerData.standColliderHeight, CollisionSenses.WhatIsGround);
        player.Anim.SetBool("isTouchingCeiling", isTouchingCeiling);
    }
}
