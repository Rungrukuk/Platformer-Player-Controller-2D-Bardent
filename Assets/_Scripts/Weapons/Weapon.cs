using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected SO_WeaponData weaponData;

    protected Animator baseAnimator;
    protected Animator weaponAnimator;

    protected P_AttackState state;

    protected int attackCounter;
    protected virtual void Awake()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        if (attackCounter >= weaponData.AmountOfAttacks)
        {
            attackCounter = 0;
        }

        gameObject.SetActive(true);
        baseAnimator.SetBool("attack", true);
        baseAnimator.SetInteger("attackCounter", attackCounter);

        weaponAnimator.SetBool("attack", true);
        weaponAnimator.SetInteger("attackCounter", attackCounter);

    }
    public virtual void ExitWeapon()
    {
        baseAnimator.SetBool("attack", false);
        weaponAnimator.SetBool("attack", false);
        attackCounter++;
        gameObject.SetActive(false);
    }
    #region Animation Triggers

    public virtual void StartMovementTrigger()
    {
        state.SetPlayerVelocity(weaponData.MovementSpeed[attackCounter]);
    }

    public virtual void StopMovementTrigger()
    {
        state.SetPlayerVelocity(0);
    }
    public virtual void AnimationTurnOnFlip()
    {
        state.SetFlipCheck(true);
    }
    public virtual void AnimationTurnOfFlip()
    {
        state.SetFlipCheck(false);
    }
    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }
    public virtual void AnimationActionTrigger()
    {

    }
    #endregion

    public void InitializeWeapon(P_AttackState state)
    {
        this.state = state;
    }
}
