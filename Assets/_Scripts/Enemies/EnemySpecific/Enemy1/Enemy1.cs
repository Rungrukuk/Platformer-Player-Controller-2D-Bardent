using UnityEngine;

namespace _Scripts.Enemies.EnemySpecific.Enemy1
{
    public class Enemy1 : Entity
    {
        public E1_IdleState IdleState { get; private set; }
        public E1_MoveState MoveState { get; private set; }
        public E1_PlayerDetectedState PlayerDetectedState { get; private set; }
        public E1_ChargeState ChargeState { get; private set; }
        public E1_LookForPlayerState LookForPlayerState { get; private set; }
        public E1_MeleeAttackState MeleeAttackState { get; private set; }
        public E1_StunState StunState { get; private set; }
        public E1_DeadState DeadState { get; private set; }


        [SerializeField]
        private D_IdleState idleStateData;
        [SerializeField]
        private D_MoveState moveStateData;
        [SerializeField]
        private D_PlayerDetectedState playerDetectedStateData;
        [SerializeField]
        private D_ChargeState chargeStateData;
        [SerializeField]
        private D_LookForPlayerState lookForPlayerStateData;
        [SerializeField]
        private D_MeleeAttackState meleeAttackStateData;
        [SerializeField]
        private D_StunState stunStateData;
        [SerializeField]
        private D_DeadState deadStateData;


        [SerializeField]
        private Transform meleeAttackPosition;


        public override void Awake()
        {
            base.Awake();

            MoveState = new E1_MoveState(this, stateMachine, "move",moveStateData,this);
            IdleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
            PlayerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
            LookForPlayerState = new E1_LookForPlayerState(this, stateMachine,"lookForPlayer",lookForPlayerStateData, this);
            MeleeAttackState = new E1_MeleeAttackState(this, stateMachine, "meleeAttack",meleeAttackPosition,meleeAttackStateData, this);
            StunState = new E1_StunState(this, stateMachine, "stun", stunStateData, this);
            DeadState = new E1_DeadState(this, stateMachine,"dead",deadStateData, this);
            ChargeState = new E1_ChargeState(this, stateMachine, "charge", chargeStateData, this);

        }
        private void Start()
        {
            stateMachine.Initialize(MoveState);
        }
        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
        }

    }
}
