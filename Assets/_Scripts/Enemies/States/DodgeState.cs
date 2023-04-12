using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
    protected D_DodgeState stateData;
    protected bool isDodgeOver;
    public bool canDodge;
    public DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,D_DodgeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -core.Movement.FacingDirection);
        canDodge = false;
        isDodgeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time>= startTime + stateData.dodgeTime && isGrounded)
        {
            isDodgeOver=true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
