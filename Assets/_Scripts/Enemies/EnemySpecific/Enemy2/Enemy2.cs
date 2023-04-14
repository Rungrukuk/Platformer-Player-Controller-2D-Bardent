
using UnityEngine;

namespace _Scripts.Enemies.EnemySpecific.Enemy2
{
    public class Enemy2 : Entity
    {

        public E2_IdleState IdleState { get; private set; }
        public E2_MoveState MoveState { get; private set; }
        public E2_PlayerDetectedState PlayerDetectedState { get; private set; }
        public E2_LookForPlayerState LookForPlayerState { get; private set; }
        public E2_MeleeAttackState MeleeAttackState { get; private set; }
        public E2_RangedAttackState RangedAttackState { get; private set; }
        public E2_StunState StunState { get; private set; }
        public E2_DeadState DeadState { get; private set; }
        public E2_DodgeState DodgeState { get; private set; }

        [SerializeField]
        private D_IdleState idleStateData;
        [SerializeField]
        private D_MoveState moveStateData;
        [SerializeField]
        private D_PlayerDetectedState playerDetectedStateData;
        [SerializeField]
        private D_LookForPlayerState lookForPlayerStateData;
        [SerializeField]
        private D_MeleeAttackState meleeAttackStateData;
        [SerializeField]
        private D_StunState stunStateData;
        [SerializeField]
        private D_DeadState deadStateData;
        [SerializeField]
        private D_RangedAttackState rangedAttackStateData;

        public D_DodgeState dodgeStateData;

        [SerializeField]
        private Transform meleeAttackPosition;

        [SerializeField]
        private Transform rangedAttackPosition;




        public override void Awake()
        {
            base.Awake();
        
            MoveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);
            IdleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);
            PlayerDetectedState = new E2_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
            LookForPlayerState = new E2_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
            MeleeAttackState = new E2_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
            RangedAttackState = new E2_RangedAttackState(this, stateMachine,"rangedAttack",rangedAttackPosition,rangedAttackStateData, this);
            StunState = new E2_StunState(this, stateMachine, "stun", stunStateData, this);
            DeadState = new E2_DeadState(this, stateMachine, "dead", deadStateData, this);
            DodgeState = new E2_DodgeState(this, stateMachine,"dodge",dodgeStateData, this);
            DodgeState.canDodge = true;
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
