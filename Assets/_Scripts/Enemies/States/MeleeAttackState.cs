using UnityEngine;

namespace _Scripts.Enemies.States
{
    public class MeleeAttackState : AttackState
    {
        private D_MeleeAttackState stateData;


        protected MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
        {
            this.stateData = stateData;
        }



        public override void TriggerAttack()
        {
            base.TriggerAttack();
            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);
            foreach (Collider2D collider in detectedObjects)
            {
                if(collider.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.Damage(stateData.attackDamage);
                }
                if (collider.TryGetComponent<IKnockbackable>(out var knockbackable))
                {
                    knockbackable.Knockback(stateData.knockbackAngle,stateData.knockbackStrength,Movement.FacingDirection);
                }
            }


        }
    }
}
