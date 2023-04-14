
using _Scripts.Enemies.States;

namespace _Scripts.Enemies.EnemySpecific.Enemy1
{
    public class E1_DeadState : DeadState
    {
        private Enemy1 enemy;

        public E1_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData,
            Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
        {
            this.enemy = enemy;
        }
    }
}

