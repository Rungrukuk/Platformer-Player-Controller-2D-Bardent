

namespace _Scripts.Enemies.States
{
    public class ChargeState : State
    {
        protected D_ChargeState stateData;

        protected bool isDetectingLedge;
        protected bool isDetectingWall;

        public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity, stateMachine, animBoolName)
        {
            this.stateData = stateData;
        }
        public override void DoChecks()
        {
            base.DoChecks();
            isDetectingLedge = CollisionSenses.LedgeVertical;
            isDetectingWall = CollisionSenses.WallFront;

        }

        public override void Enter()
        {
            base.Enter();
            Movement.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);

        }



        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Movement.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);
        }
    }
}
