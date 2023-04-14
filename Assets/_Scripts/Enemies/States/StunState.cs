
using UnityEngine;


public class StunState : State
{
    private readonly D_StunState stateData;

    protected bool isStunTimeOver;
    private bool isMovementStopped;

    protected StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    

    public override void Enter()
    {
        base.Enter();
        isStunTimeOver = false;
        isMovementStopped = false;
        Movement.SetVelocity(stateData.stunKnockbackSpeed, stateData.stunKnockbackAngle, entity.LastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();
        entity.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time>= startTime + stateData.stunTime)
        {
            isStunTimeOver=true;
        }
        if(isGrounded&&Time.time>=startTime+stateData.stunKnockbackTime && !isMovementStopped)
        {
            isMovementStopped = true;
            Movement.SetVelocityX(0);
        }
    }
    
}
