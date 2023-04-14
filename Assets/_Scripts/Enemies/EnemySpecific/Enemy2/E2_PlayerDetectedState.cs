

using _Scripts.Enemies.States;

namespace _Scripts.Enemies.EnemySpecific.Enemy2
{
    public class E2_PlayerDetectedState : PlayerDetectedState
    {
        private readonly Enemy2 enemy;

        public E2_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData,Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
        {
            this.enemy = enemy;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.MeleeAttackState);
            }
            else if (performLongRangeAction)
            {
                stateMachine.ChangeState(enemy.RangedAttackState);
            }
            else if (!isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(enemy.LookForPlayerState);
            }

        }
    
    }
}
