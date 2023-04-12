using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AbilityState : PlayerState
{
    protected bool isAbilityDone;

    protected bool isGrounded;
    public int xInput;
    public P_AbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = core.CollisionSenses.Ground;
        xInput = player.InputHandler.NormalizedInputX;
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAbilityDone)
        {
            Debug.Log(core.Movement.CurrentVelocity.y);
            if (isGrounded)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
