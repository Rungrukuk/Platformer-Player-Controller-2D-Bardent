

namespace _Scripts.Enemies.EnemySpecific.Enemy2
{
    public class E2_StunState : StunState
    {
        private readonly Enemy2 enemy;
        public E2_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
        {
            this.enemy = enemy;
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (isStunTimeOver)
            {
                if (performCloseRangeAction)
                {
                    stateMachine.ChangeState(enemy.MeleeAttackState);
                }
                else if (isPlayerInMinAgroRange)
                {
                    enemy.DodgeState.canDodge = true;
                    stateMachine.ChangeState(enemy.DodgeState);
                }
                else if(!isPlayerInMaxAgroRange)
                {
                    enemy.LookForPlayerState.SetTurnImmediately(true);
                    stateMachine.ChangeState(enemy.LookForPlayerState);
                }
                else if (isPlayerInMaxAgroRange && !isPlayerInMinAgroRange)
                {
                    stateMachine.ChangeState(enemy.RangedAttackState);
                }
            }
        }
    }
}
