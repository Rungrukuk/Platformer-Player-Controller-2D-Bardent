using UnityEngine;


public class PlayerCombatController : MonoBehaviour
{
    //[SerializeField]
    //private bool combatEnabled;
    //[SerializeField]
    //private float inputTimer,attack1Radius,attack1Damage,stunDamageAmount;
    //[SerializeField]
    //private Transform attack1HitBoxPos;
    //[SerializeField]
    //private LayerMask whatIsDamageable;

    //private bool gotInput,isAttacking,isFirstAttack;

    //private float lastInputTime = Mathf.NegativeInfinity;

    //private AttackDetails attackDetails;

    //private Animator animator;

    //private PlayerController PC;

    //private PlayerStats PS;

    //private void Start()
    //{
    //    animator = GetComponent<Animator>();
    //    animator.SetBool("canAttack", combatEnabled);
    //    PC = GetComponent<PlayerController>();
    //    PS = GetComponent<PlayerStats>();
    //}
    //private void Update()
    //{
    //    CheckCombatInput();
    //    CheckAttacks();
    //}
    //private void CheckCombatInput()
    //{
    //    if(Input.GetMouseButton(0))
    //    {
    //        if (combatEnabled)
    //        {
    //            //Attempt combat
    //            gotInput=true;
    //            lastInputTime = Time.time;
    //        }
    //    }

    //}
    //private void CheckAttacks()
    //{
    //    if (gotInput)
    //    {
    //        //PerformanceReporting Attack1
    //        if (!isAttacking)
    //        {
    //            gotInput = false;
    //            isAttacking = true;
    //            isFirstAttack = !isFirstAttack;
    //            animator.SetBool("attack1", true);
    //            animator.SetBool("firstAttack", isFirstAttack);
    //            animator.SetBool("isAttacking", isAttacking);
    //        }
    //    }
    //    if (Time.time >= lastInputTime + inputTimer)
    //    {
    //        //Wait for the input
    //        gotInput = false;
    //    }
    //}

    //private void CheckAttackHitBox()
    //{
    //    Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position,attack1Radius,whatIsDamageable);
    //    attackDetails.damageAmount = attack1Damage;
    //    attackDetails.position = transform.position;
    //    attackDetails.stunDamageAmount = stunDamageAmount;
    //    foreach (Collider2D collider in detectedObjects)
    //    {
    //        collider.transform.parent.SendMessage("Damage",attackDetails);
    //        //Instatiate hit particle
    //    }
    //}

    //private void FinishAttack1()
    //{
    //    isAttacking = false;
    //    animator.SetBool("attack1", false);
    //    animator.SetBool("isAttacking", false);
    //}
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(attack1HitBoxPos.position,attack1Radius);
    //}

    //private void Damage(AttackDetails attackDetails)
    //{
    //    if (!PC.GetDashStatus())
    //    {
    //        int direction;
    //        PS.DecreaseHealth(attackDetails.damageAmount);
    //        if (attackDetails.position.x < transform.position.x)
    //        {
    //            direction = 1;
    //        }
    //        else
    //        {
    //            direction = -1;
    //        }

    //        PC.Knockback(direction);
    //    }

    //}

}
