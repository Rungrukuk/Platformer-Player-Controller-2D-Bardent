using _Scripts.Enemies.States;
using UnityEngine;

public class RangedAttackState : AttackState
{
    private readonly D_RangedAttackState stateData;

    private GameObject projectile;
    private Projectile projectileScript;

    protected RangedAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,
        Transform attackPosition, D_RangedAttackState stateData) : base(entity, stateMachine, animBoolName,
        attackPosition)
    {
        this.stateData = stateData;
    }
    

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        projectile = Object.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage);
    }
}
