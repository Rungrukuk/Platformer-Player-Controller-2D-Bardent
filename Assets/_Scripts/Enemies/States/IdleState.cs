
using UnityEngine;

public class IdleState : State
{
    private readonly D_IdleState stateData;

    private bool flipAfterIdle;
    protected bool isIdleTimeOver;

    private float idleTime;

    protected IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }



    public override void Enter()
    {
        base.Enter();
        if(Movement)
            Movement.SetVelocityX(0);
        isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if(flipAfterIdle)
        {
            if(Movement)
                Movement.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Movement)
            Movement.SetVelocityX(0);
        if (Time.time>=startTime + idleTime)
        {
            isIdleTimeOver=true;
        }
    }
    

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
