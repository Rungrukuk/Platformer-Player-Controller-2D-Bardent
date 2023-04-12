using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    public Transform GroundCheck { get => groundCheck; private set => groundCheck = value; }
    public Transform WallCheck { get => wallCheck; private set => wallCheck = value; }
    public Transform CeilingCheck { get => ceilingCheck; private set => ceilingCheck = value; }
    public Transform LedgeCheck { get => ledgeCheck; private set => ledgeCheck = value; }
    public float GroundCheckRadius { get => groundCheckRadius; private set => groundCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; private set => wallCheckDistance = value; }
    public float CeilingCheckDistance { get => ceilingCheckDistance; private set => ceilingCheckDistance = value; }

    public LayerMask WhatIsGround { get =>whatIsGround; set => whatIsGround = value;}

    [SerializeField]
    private Transform
    groundCheck,
    wallCheck,
    ledgeCheck,
    ceilingCheck;

    [SerializeField]
    private float
        groundCheckRadius,
        wallCheckDistance,
        ceilingCheckDistance;

    [SerializeField] 
    private LayerMask whatIsGround;


    public bool Ground{ get => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);}
    public bool WallFront{ get => Physics2D.Raycast(wallCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);}
    public bool WallBack{ get => Physics2D.Raycast(wallCheck.position, Vector2.right * -core.Movement.FacingDirection, wallCheckDistance, whatIsGround);}
    public bool Ledge{ get => Physics2D.Raycast(ledgeCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);}
    public bool Ceiling{ get => Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckDistance, whatIsGround);}
}
