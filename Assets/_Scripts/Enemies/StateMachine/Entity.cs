using _Scripts.Core;
using _Scripts.Core.CoreComponents;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public D_Entity entityData;

    protected FiniteStateMachine stateMachine;
    public Animator Anim { get; private set; }
    public AnimationToStateMachine AnimationToStateMachine { get; private set; }
    public int LastDamageDirection { get; private set; }
    public Core Core { get; private set; }

    private Movement Movement { get => movement = movement != null ? movement : GetComponent<Movement>(); }

    private Movement movement;

    [SerializeField]
    protected Transform
        wallCheck,
        ledgeCheck,
        playerCheck,
        groundCheck;

    private float currentHealth, currentStunResistance, lastDamageTime;



    private Vector2 velocityWorkspace;

    protected bool isStunned, isDead;

    public virtual void Awake()
    {
        isStunned = false;
        isDead = false;
        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;
        Anim = GetComponent<Animator>();
        AnimationToStateMachine = GetComponent<AnimationToStateMachine>();
        Core = GetComponentInChildren<Core>();
        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        Core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();
        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
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

        velocityWorkspace.Set(Movement.Rb.velocity.x, !isStunned ? velocity : velocity / 2);
        Movement.Rb.velocity = velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }



    public virtual void OnDrawGizmos()
    {
        if (Movement != null)
        {
            var position = wallCheck.position;
            var position2 = ledgeCheck.position;
            var position1 = playerCheck.position;
            Gizmos.DrawLine(position, position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.wallCheckDistance));
            Gizmos.color = Color.green;

            Gizmos.DrawLine(position1, position1 + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.maxAgroDistance));

            Gizmos.DrawLine(position2, position2 + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
            Gizmos.color = Color.red;
            Gizmos.DrawLine(position1, position1 + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.minAgroDistance));

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(position1, position1 + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.closeRangeActionDistance));
        }

    }
}
