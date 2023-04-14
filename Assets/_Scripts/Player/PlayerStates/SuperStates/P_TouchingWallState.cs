using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.CoreComponents;
using UnityEngine;

public class P_TouchingWallState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool grabInput;
    protected bool jumpInput;
    protected bool isTouchingLedge;
    protected int xInput;
    protected int yInput;

    protected Movement Movement{get=>movement??core.GetCoreComponent(ref movement);}

    private Movement movement;

    private CollisionSenses collisionSenses;
    private CollisionSenses CollisionSenses{ get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }


    public P_TouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = CollisionSenses.Ground;
        isTouchingWall = CollisionSenses.WallFront;
        isTouchingLedge = CollisionSenses.LedgeHorizontal;
        jumpInput = player.InputHandler.JumpInput;

        if(isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormalizedInputX;
        yInput = player.InputHandler.NormalizedInputY;
        grabInput = player.InputHandler.GrabInput;
        if (jumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if(!isTouchingWall||(xInput!=Movement.FacingDirection&&!isGrounded&&!grabInput))
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else if(isTouchingWall&& !isTouchingLedge)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
