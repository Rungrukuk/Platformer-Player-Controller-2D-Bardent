
using _Scripts.Enemies.States;
using UnityEngine;

namespace _Scripts.Enemies.EnemySpecific.Enemy2
{
    public class E2_RangedAttackState : RangedAttackState
    {
        private readonly Enemy2 enemy;

        public E2_RangedAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangedAttackState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
        {
            this.enemy = enemy;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (Time.time >= startTime + enemy.dodgeStateData.dodgeCooldown && !enemy.DodgeState.canDodge)
            {
                enemy.DodgeState.canDodge = true;
            }
            if (!isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(enemy.LookForPlayerState);
            }
            else if (isPlayerInMinAgroRange && enemy.DodgeState.canDodge)
            {
                stateMachine.ChangeState(enemy.DodgeState);
            }
            else if (performCloseRangeAction)
            {
                enemy.DodgeState.canDodge = false;
                stateMachine.ChangeState(enemy.MeleeAttackState);
            }

        }
    }
}
