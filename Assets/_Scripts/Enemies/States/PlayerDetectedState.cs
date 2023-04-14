
using UnityEngine;

namespace _Scripts.Enemies.States
{
    public class PlayerDetectedState : State
    {
        protected bool performLongRangeAction;
        private bool isDetectingLedge;

        private readonly D_PlayerDetectedState stateData;

        protected PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData) : base(entity, stateMachine, animBoolName)
        {
            this.stateData = stateData;
        }

        public override void DoChecks()
        {
            base.DoChecks();
            isDetectingLedge = CollisionSenses.LedgeVertical;
        }

        public override void Enter()
        {
            base.Enter();
            performLongRangeAction = false;
            Movement.SetVelocityX(0);

        }



        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Movement.SetVelocityX(0);
            if (Time.time>=startTime + stateData.longRangeActionTime&&isPlayerInMaxAgroRange && isDetectingLedge)
            {
                performLongRangeAction = true;
            }
        }
        
    }
}
