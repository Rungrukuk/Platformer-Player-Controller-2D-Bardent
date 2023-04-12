using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected bool performLongRangeAction;
    protected bool isDetectingLedge;

    protected D_PlayerDetectedState stateData;
    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingLedge = core.CollisionSenses.LedgeVertical;
    }

    public override void Enter()
    {
        base.Enter();
        performLongRangeAction = false;
        core.Movement.SetVelocityX(0);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time>=startTime + stateData.longRangeActionTime&&isPlayerInMaxAgroRange && isDetectingLedge)
        {
            performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
