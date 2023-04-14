using _Scripts.Enemies.States;

namespace _Scripts.Enemies.EnemySpecific.Enemy1
{
    public class E1_ChargeState : ChargeState
    {
        private readonly Enemy1 enemy;

        public E1_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,
            D_ChargeState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            else if (!isDetectingLedge || isDetectingWall || !isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(enemy.LookForPlayerState);
            }
        }
    }
}