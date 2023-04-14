
using UnityEngine;

namespace _Scripts.Enemies.States
{
    public class DodgeState : State
    {
        private readonly D_DodgeState stateData;
        protected bool isDodgeOver;
        public bool canDodge;

        protected DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,D_DodgeState stateData) : base(entity, stateMachine, animBoolName)
        {
            this.stateData = stateData;
        }


        public override void Enter()
        {
            base.Enter();
            Movement.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -Movement.FacingDirection);
            canDodge = false;
            isDodgeOver = false;
        }


        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (Time.time>= startTime + stateData.dodgeTime && isGrounded)
            {
                isDodgeOver=true;
            }
        }


    }
}
