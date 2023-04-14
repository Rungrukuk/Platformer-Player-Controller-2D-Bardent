
using _Scripts.Enemies.States;

namespace _Scripts.Enemies.EnemySpecific.Enemy2
{
    public class E2_DeadState : DeadState
    {
        //TODO: private global::Enemy2 enemy;
        public E2_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, global::_Scripts.Enemies.EnemySpecific.Enemy2.Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
        {
            //TODO: this.enemy = enemy;
        }
    
    }
}
