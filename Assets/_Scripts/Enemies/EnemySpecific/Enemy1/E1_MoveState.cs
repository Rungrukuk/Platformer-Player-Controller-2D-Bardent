using _Scripts.Enemies.States;

namespace _Scripts.Enemies.EnemySpecific.Enemy1
{
    public class E1_MoveState : MoveState
    {
        private readonly Enemy1 enemy;

        public E1_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData,
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
            else if (isDetectingWall || !isDetectingLedge)
            {
                enemy.IdleState.SetFlipAfterIdle(true);
                stateMachine.ChangeState(enemy.IdleState);
            }
        }
    }
}

