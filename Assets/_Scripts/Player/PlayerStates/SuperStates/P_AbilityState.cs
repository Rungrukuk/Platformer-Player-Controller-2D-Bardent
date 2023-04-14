using _Scripts.Core.CoreComponents;

namespace _Scripts.Player.PlayerStates.SuperStates
{
    public class P_AbilityState : PlayerState
    {
        protected bool isAbilityDone;

        private bool isGrounded;
        protected Movement Movement => movement ? movement : core.GetCoreComponent(ref movement);

        private Movement movement;

        private CollisionSenses collisionSenses;
        private CollisionSenses CollisionSenses => collisionSenses ? collisionSenses : core.GetCoreComponent(ref collisionSenses);
        public int xInput;

        protected P_AbilityState(global::Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
            if(CollisionSenses)
                isGrounded = CollisionSenses.Ground;
            xInput = player.InputHandler.NormalizedInputX;
        }

        public override void Enter()
        {
            base.Enter();
            isAbilityDone = false;
        }
    

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (isAbilityDone)
            {
                if (isGrounded)
                {
                    stateMachine.ChangeState(player.IdleState);
                }
                else
                {
                    stateMachine.ChangeState(player.InAirState);
                }
            }
        }
    
    }
}
