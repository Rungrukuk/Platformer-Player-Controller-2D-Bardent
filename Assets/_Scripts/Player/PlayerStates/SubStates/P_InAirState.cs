using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.CoreComponents;
using UnityEngine;

public class P_InAirState : PlayerState
{
    //Input
    private int xInput;
    private bool dashInput;
    private bool jumpInput;
    private bool grabInput;
    private bool jumpInputStopped;

    //Checks
    private bool isTouchingLedge;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isJumping;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    
    private bool cayoteTime;
    private bool wallJumpCayoteTime;
    private float startWallJumpCayoteTime;

    private Movement Movement{get=>movement??core.GetCoreComponent(ref movement);}

    private Movement movement;

    private CollisionSenses collisionSenses;
    private CollisionSenses CollisionSenses{ get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

    public P_InAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        if(CollisionSenses)
        {
            isGrounded = CollisionSenses.Ground;
            isTouchingWall = CollisionSenses.WallFront;
            isTouchingWallBack = CollisionSenses.WallBack;
            isTouchingLedge = CollisionSenses.LedgeHorizontal;
        }
        if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }

        if (!wallJumpCayoteTime &&!isTouchingWall && !isTouchingWallBack &&(oldIsTouchingWall||oldIsTouchingWallBack))
        {
            StartWallJumpCayoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCayoteTime();
        CheckWallJumpCayoteTime();

        xInput = player.InputHandler.NormalizedInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStopped = player.InputHandler.JumpInputStopped;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        CheckJumpMultiplier();
        if (player.InputHandler.AttackInputs[(int)CombatInputs.primary])
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary])
        {
            stateMachine.ChangeState(player.secondaryAttackState);
        }
        else if (isGrounded && Movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (isTouchingWall&&!isTouchingLedge && !isGrounded)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        else if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCayoteTime))
        {
            StopWallJumpCayoteTime();
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if(jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (grabInput && isTouchingWall && isTouchingLedge)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if(isTouchingWall&&xInput == Movement.FacingDirection && Movement.CurrentVelocity.y<=0)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else if (dashInput&&player.DashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }
        else
        {
            if (Movement)
            {
                Movement.CheckIfShouldFlip(xInput);
                Movement.SetVelocityX(playerData.movementVelocity * xInput);
            }


            player.Anim.SetFloat("yVelocity", player.RB.velocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.RB.velocity.x));
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (Movement)
            {
                if (jumpInputStopped)
                {
                    Movement.SetVelocityY(Movement.CurrentVelocity.y * playerData.jumpHeightMultiplier);
                    isJumping = false;
                }
                else if (Movement.CurrentVelocity.y <= 0f)
                {
                    isJumping = false;
                }
            }

        }
    }

    private void CheckCayoteTime()
    {
        if(cayoteTime&&Time.time>startTime + playerData.cayoteTime)
        {
            cayoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }
    private void CheckWallJumpCayoteTime()
    {
        if(wallJumpCayoteTime && Time.time>=startWallJumpCayoteTime + playerData.cayoteTime)
        {
            wallJumpCayoteTime = false;
        }
    }
    public void StartCayoteTime()=>cayoteTime = true;
    public void StartWallJumpCayoteTime()
    {
        wallJumpCayoteTime = true;
        startWallJumpCayoteTime = Time.time;
    }
    public void StopWallJumpCayoteTime() => wallJumpCayoteTime = false;
    public void SetIsJumping()=>isJumping = true;

}
