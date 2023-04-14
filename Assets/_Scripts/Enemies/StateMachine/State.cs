
using _Scripts.Core;
using _Scripts.Core.CoreComponents;

using UnityEngine;

public class State
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;
    protected Core core;

    protected float startTime;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performCloseRangeAction;
    protected bool isGrounded;
    protected Movement Movement{get=>movement??core.GetCoreComponent(ref movement);}

    private Movement movement;

    private CollisionSenses collisionSenses;
    protected CollisionSenses CollisionSenses{ get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

    protected string animBoolName;

    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        core = entity.Core;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        entity.Anim.SetBool(animBoolName, true);
        DoChecks();
    }

    public virtual void Exit()
    {
        entity.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        if(CollisionSenses)
            isGrounded = CollisionSenses.Ground;
    }
}
