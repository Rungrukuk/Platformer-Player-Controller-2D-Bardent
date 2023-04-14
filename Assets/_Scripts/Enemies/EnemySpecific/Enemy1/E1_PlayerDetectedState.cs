using _Scripts.Enemies.States;

namespace _Scripts.Enemies.EnemySpecific.Enemy1
{
    public class E1_PlayerDetectedState : PlayerDetectedState
    {
        private readonly Enemy1 enemy;
        public E1_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
                stateMachine.ChangeState(enemy.ChargeState);
            }
            else if (!isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.LookForPlayerState);
            }
        }
    }
}

