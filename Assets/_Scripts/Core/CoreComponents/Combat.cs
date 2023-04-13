using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent,IDamageable,IKnockbackable
{
    [SerializeField]
    private bool isKnockbackActive;
    private float knockBackStartTime;

    public override void LogicUpdate()
    {
        CheckKnockback();
    }
    public void Damage(float amount)
    {
        core.Stats.DecreaseHealth(amount);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        core.Movement.SetVelocity(strength,angle,direction);
        core.Movement.CanSetVelocity = false;
        isKnockbackActive = true;
    }
    private void CheckKnockback()
    {
        if(isKnockbackActive && core.Movement.CurrentVelocity.y<=0.01f && core.CollisionSenses.Ground)
        {
            isKnockbackActive=false;
            core.Movement.CanSetVelocity = true;
        }
    }
}
