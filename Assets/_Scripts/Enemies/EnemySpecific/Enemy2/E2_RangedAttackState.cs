using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_RangedAttackState : RangedAttackState
{
    protected Enemy2 enemy;

    public E2_RangedAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangedAttackState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + enemy.dodgeStateData.dodgeCooldown && !enemy.dodgeState.canDodge)
        {
            enemy.dodgeState.canDodge = true;
        }
        if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (isPlayerInMinAgroRange && enemy.dodgeState.canDodge)
        {
            stateMachine.ChangeState(enemy.dodgeState);
        }
        else if (performCloseRangeAction)
        {
            enemy.dodgeState.canDodge = false;
            stateMachine.ChangeState(enemy.meleeAttackState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
