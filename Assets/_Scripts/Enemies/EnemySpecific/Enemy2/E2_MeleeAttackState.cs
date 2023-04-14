
using _Scripts.Enemies.States;
using UnityEngine;

namespace _Scripts.Enemies.EnemySpecific.Enemy2
{
    public class E2_MeleeAttackState : MeleeAttackState
    {
        private readonly Enemy2 enemy;
        public E2_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
        {
            this.enemy = enemy;
        }
        

        public override void Exit()
        {
            base.Exit();
            enemy.DodgeState.canDodge = false;
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (isAnimationFinished)
            {
                stateMachine.ChangeState(enemy.LookForPlayerState);
            }

        }
        
    }
}
