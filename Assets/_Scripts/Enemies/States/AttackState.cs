using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public Transform attackPosition;

    protected bool isAnimationFinished;
    public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(entity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        entity.atsm.attackState = this;
        isAnimationFinished = false;
        core.Movement.SetVelocityX(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        core.Movement.SetVelocityX(0);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public virtual void TriggerAttack()
    {
    }
    public virtual void FinishAttack()
    {
        isAnimationFinished=true;
    }
}
