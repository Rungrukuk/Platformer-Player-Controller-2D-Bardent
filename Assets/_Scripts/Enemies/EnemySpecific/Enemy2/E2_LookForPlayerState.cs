


using _Scripts.Enemies.States;

namespace _Scripts.Enemies.EnemySpecific.Enemy2
{
    public class E2_LookForPlayerState : LookForPlayerState
    {
        private readonly Enemy2 enemy;

        public E2_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData,Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
        {
            this.enemy = enemy;
        }
    

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(enemy.RangedAttackState);
            }
            if (isAllTurnsTimeDone)
            {
                enemy.DodgeState.canDodge = true;
                stateMachine.ChangeState(enemy.MoveState);
            }
        }
        
    }
}
