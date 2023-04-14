

using _Scripts.Enemies.States;

namespace _Scripts.Enemies.EnemySpecific.Enemy2
{
    public class E2_MoveState : MoveState
    {
        private readonly Enemy2 enemy;
        public E2_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            else if (isDetectingWall || !isDetectingLedge)
            {
                enemy.IdleState.SetFlipAfterIdle(true);
                stateMachine.ChangeState(enemy.IdleState);
            }
        }
    
    }
}
