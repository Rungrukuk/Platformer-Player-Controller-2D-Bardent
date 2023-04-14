
using _Scripts.Enemies.States;
using UnityEngine;

namespace _Scripts.Enemies.EnemySpecific.Enemy1
{
    public class E1_MeleeAttackState : MeleeAttackState
    {
        private readonly Enemy1 enemy;
        public E1_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
        {
            this.enemy = enemy;
        }
    

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (isAnimationFinished)
            {
                if (isPlayerInMinAgroRange)
                    stateMachine.ChangeState(enemy.PlayerDetectedState);
                else
                    stateMachine.ChangeState(enemy.LookForPlayerState);
            }
        }
    
    }
}

