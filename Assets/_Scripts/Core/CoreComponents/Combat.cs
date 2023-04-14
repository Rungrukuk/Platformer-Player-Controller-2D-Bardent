using UnityEngine;

namespace _Scripts.Core.CoreComponents
{
    public class Combat : CoreComponent,IDamageable,IKnockbackable
    {
        [SerializeField]
        private bool isKnockbackActive;


        public override void LogicUpdate()
        {
            CheckKnockback();
        }
        public void Damage(float amount)
        {
            Stats.DecreaseHealth(amount);
        }

        public void Knockback(Vector2 angle, float strength, int direction)
        {
            Movement.SetVelocity(strength,angle,direction);
            Movement.CanSetVelocity = false;
            isKnockbackActive = true;
        }
        private void CheckKnockback()
        {
            if(isKnockbackActive && Movement.CurrentVelocity.y<=0.01f && CollisionSenses.Ground)
            {
                isKnockbackActive=false;
                Movement.CanSetVelocity = true;
            }
        }
    }
}
