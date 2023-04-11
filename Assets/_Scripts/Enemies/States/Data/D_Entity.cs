using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "E?_BaseData", menuName = "Data/Entity Data/Base Data")]

public class D_Entity : ScriptableObject
{
    public float maxHealth = 30;

    public float damageHopSpeed = 3;

    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float groundCheckRadius = 0.3f;

    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;

    public float minAgroDistance = 8f;
    public float maxAgroDistance = 10f;

    public float closeRangeActionDistance = 1f;

    public GameObject hitParticle;

    public LayerMask whatIsPlayer;
    public LayerMask whatIsGround;



}
