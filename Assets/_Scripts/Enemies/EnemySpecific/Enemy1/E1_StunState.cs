namespace _Scripts.Enemies.EnemySpecific.Enemy1
{
    public class E1_StunState : StunState
    {
        private readonly Enemy1 enemy;

        public E1_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData,Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
                    stateMachine.ChangeState(enemy.ChargeState);
                }
                else
                {
                    enemy.LookForPlayerState.SetTurnImmediately(true);
                    stateMachine.ChangeState(enemy.LookForPlayerState);
                }
            }
        }
    }
}
