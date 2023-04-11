using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationToWeapon : MonoBehaviour
{
    private Weapon weapon;

    private void Start()
    {
        weapon = GetComponentInParent<Weapon>();
    }
    private void AnimationFinishTrigger()
    {
        weapon.AnimationFinishTrigger();
    }
    private void AnimationStartMovementTrigger()
    {
        weapon.StartMovementTrigger();
    }
    private void AnimationStopMovementTrigger()
    {
        weapon.StopMovementTrigger();
    }
    private void AnimationTurnOnFlipTrigger()
    {
        weapon.AnimationTurnOnFlip();
    }
    private void AnimationTurnOfFlipTrigger()
    {
        weapon.AnimationTurnOfFlip();
    }
}
