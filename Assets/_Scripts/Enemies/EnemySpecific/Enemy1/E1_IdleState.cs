
namespace _Scripts.Enemies.EnemySpecific.Enemy1
{
    public class E1_IdleState : IdleState
    {
        private readonly Enemy1 enemy;

        public E1_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData,
            Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
        {
            this.enemy = enemy;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (isPlayerInMinAgroRange)
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

