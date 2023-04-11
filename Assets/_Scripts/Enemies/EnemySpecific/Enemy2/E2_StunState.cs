public class E2_StunState : StunState
{
    Enemy2 enemy;
    public E2_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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
        if (isStunTimeOver)
        {
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
            else if (isPlayerInMinAgroRange)
            {
                enemy.dodgeState.canDodge = true;
                stateMachine.ChangeState(enemy.dodgeState);
            }
            else if(!isPlayerInMaxAgroRange)
            {
                enemy.lookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
            else if (isPlayerInMaxAgroRange && !isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.rangedAttackState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
