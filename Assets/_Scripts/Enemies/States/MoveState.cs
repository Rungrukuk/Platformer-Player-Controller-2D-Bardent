

namespace _Scripts.Enemies.States
{
    public class MoveState : State
    {
        private readonly D_MoveState stateData;

        protected bool isDetectingWall;
        protected bool isDetectingLedge;

        protected MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
        {
            this.stateData = stateData;
        }

        public override void DoChecks()
        {
            base.DoChecks();
            if (CollisionSenses)
            {
                isDetectingLedge = CollisionSenses.LedgeVertical;
                isDetectingWall = CollisionSenses.WallFront;
            }

        }

        public override void Enter()
        {
            base.Enter();
            if(Movement)
                Movement.SetVelocityX(stateData.movementSpeed * Movement.FacingDirection);
        }



        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if(Movement)
                Movement.SetVelocityX(stateData.movementSpeed * Movement.FacingDirection);
        }


    }
}
