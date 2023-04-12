using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public D_Entity entityData;

    public FiniteStateMachine stateMachine;
    public Animator anim { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }
    public int lastDamageDirection { get; private set; }
    public Core Core { get;private set; }


    [SerializeField]
    protected Transform 
        wallCheck, 
        ledgeCheck,
        playerCheck,
        groundCheck;

    private float currentHealth, currentStunResistance,lastDamageTime;



    private Vector2 velocityWorkspace;

    protected bool isStunned, isDead;

    public virtual void Awake()
    {
        isStunned = false;
        isDead = false;
        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;
        anim = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStateMachine>();
        Core = GetComponentInChildren<Core>();
        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        Core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();
        if (Time.time>=lastDamageTime + entityData.stunRecoveryTime)
        {
            
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    public virtual void DamageHop(float velocity)
    {
        
        velocityWorkspace.Set(Core.Movement.RB.velocity.x,!isStunned?velocity:velocity/2);
        Core.Movement.RB.velocity = velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {

        lastDamageTime = Time.time;
        currentHealth -= attackDetails.damageAmount;
        currentStunResistance -= attackDetails.stunDamageAmount;
        if(Core.CollisionSenses.Ground)
            DamageHop(entityData.damageHopSpeed);

        Instantiate(entityData.hitParticle, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0, 360)));

        if (attackDetails.position.x > transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }
        if (currentStunResistance <= 0)
        {
            isStunned = true;
        }
        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }


    public virtual void OnDrawGizmos()
    {
        if(Core != null)
        {
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * Core.Movement.FacingDirection * entityData.wallCheckDistance));
            Gizmos.color = Color.green;
            Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(Vector2.right * Core.Movement.FacingDirection * entityData.maxAgroDistance));
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
            Gizmos.color = Color.red;
            Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(Vector2.right * Core.Movement.FacingDirection * entityData.minAgroDistance));

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(Vector2.right * Core.Movement.FacingDirection * entityData.closeRangeActionDistance));
        }

    }
}
