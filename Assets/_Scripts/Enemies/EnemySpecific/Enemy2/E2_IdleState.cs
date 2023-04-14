

namespace _Scripts.Enemies.EnemySpecific.Enemy2
{
    public class E2_IdleState : IdleState
    {
        private readonly Enemy2 enemy;

        public E2_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData,Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
        {
            this.enemy = enemy;
        }
    

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(enemy.PlayerDetectedState);
            }
            else if (isIdleTimeOver)
            {
                stateMachine.ChangeState(enemy.MoveState);
            }
        }
    
    }
}
