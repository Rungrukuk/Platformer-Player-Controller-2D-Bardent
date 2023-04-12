using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTestDummy : MonoBehaviour,IDamageable
{
    [SerializeField]
    private GameObject hitParticles;

    private Animator anim;
    
    public void Damage(float amount)
    {
        Debug.Log("Damage it took is: " + amount);
        Instantiate(hitParticles, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        anim.SetTrigger("damage");
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
