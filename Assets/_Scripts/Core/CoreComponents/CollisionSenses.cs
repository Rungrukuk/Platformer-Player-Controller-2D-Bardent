using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    public Transform GroundCheck { get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name); private set => groundCheck = value; }
    public Transform WallCheck{ get=> GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name); private set => wallCheck = value; }
    public Transform CeilingCheck{ get => GenericNotImplementedError<Transform>.TryGet(ceilingCheck, core.transform.parent.name); private set => ceilingCheck = value; }
    public Transform LedgeCheckHorizontal{ get=> GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name); private set => ledgeCheckHorizontal = value; }
    public Transform LedgeCheckVertical { get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name); private set => ledgeCheckVertical = value; }
    public float GroundCheckRadius { get => groundCheckRadius; private set => groundCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; private set => wallCheckDistance = value; }
    public float CeilingCheckDistance { get => ceilingCheckDistance; private set => ceilingCheckDistance = value; }

    public LayerMask WhatIsGround { get =>whatIsGround; set => whatIsGround = value;}

    [SerializeField]
    private Transform
    groundCheck,
    wallCheck,
    ledgeCheckHorizontal,
    ceilingCheck,
    ledgeCheckVertical;

    [SerializeField]
    private float
        groundCheckRadius,
        wallCheckDistance,
        ceilingCheckDistance;

    [SerializeField] 
    private LayerMask whatIsGround;


    public bool Ground{ get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);}
    public bool WallFront{ get => Physics2D.Raycast(WallCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);}
    public bool WallBack{ get => Physics2D.Raycast(WallCheck.position, Vector2.right * -core.Movement.FacingDirection, wallCheckDistance, whatIsGround);}
    public bool LedgeHorizontal{ get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);}
    public bool Ceiling{ get => Physics2D.OverlapCircle(CeilingCheck.position, ceilingCheckDistance, whatIsGround);}
    public bool LedgeVertical { get=>Physics2D.Raycast(LedgeCheckVertical.position,Vector2.down, wallCheckDistance, whatIsGround);}
}
