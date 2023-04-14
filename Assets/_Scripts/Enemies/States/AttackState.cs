
using UnityEngine;

namespace _Scripts.Enemies.States
{
    public class AttackState : State
    {
        public Transform attackPosition;

        protected bool isAnimationFinished;


        public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(entity, stateMachine, animBoolName)
        {
            this.attackPosition = attackPosition;
        }



        public override void Enter()
        {
            base.Enter();
            entity.AnimationToStateMachine.attackState = this;
            isAnimationFinished = false;
            if(Movement)
                Movement.SetVelocityX(0);
        }



        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if(Movement)
                Movement.SetVelocityX(0);
        }



        public virtual void TriggerAttack()
        {
        }
        public virtual void FinishAttack()
        {
            isAnimationFinished=true;
        }
    }
}
