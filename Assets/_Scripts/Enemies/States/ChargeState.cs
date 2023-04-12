using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected D_ChargeState stateData;

    protected bool isDetectingLedge;
    protected bool isDetectingWall;

    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingLedge = core.CollisionSenses.LedgeVertical;
        isDetectingWall = core.CollisionSenses.WallFront;

    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocityX(stateData.chargeSpeed * core.Movement.FacingDirection);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        core.Movement.SetVelocityX(stateData.chargeSpeed * core.Movement.FacingDirection);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }


}
