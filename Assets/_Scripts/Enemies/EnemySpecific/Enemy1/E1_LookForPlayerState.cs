using _Scripts.Enemies.States;

namespace _Scripts.Enemies.EnemySpecific.Enemy1
{
    public class E1_LookForPlayerState : LookForPlayerState
    {
        private readonly Enemy1 enemy;

        public E1_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,
            D_LookForPlayerState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
        {
            this.enemy = enemy;
        }


        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (isPlayerInMinAgroRange) stateMachine.ChangeState(enemy.PlayerDetectedState);
            if (isAllTurnsTimeDone) stateMachine.ChangeState(enemy.MoveState);
        }
    }
}

