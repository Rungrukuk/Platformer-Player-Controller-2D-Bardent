using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent,IDamageable,IKnockbackable
{
    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + ": " + amount);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        throw new System.NotImplementedException();
    }
}
