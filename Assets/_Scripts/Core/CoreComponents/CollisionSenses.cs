using UnityEngine;

namespace _Scripts.Core.CoreComponents
{
    public class CollisionSenses : CoreComponent
    {
        private Transform GroundCheck => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
        public Transform WallCheck => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name);
        private Transform CeilingCheck => GenericNotImplementedError<Transform>.TryGet(ceilingCheck, core.transform.parent.name);
        public Transform LedgeCheckHorizontal => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name);
        private Transform LedgeCheckVertical => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name);
        public float WallCheckDistance => wallCheckDistance;

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


        public bool Ground => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
        public bool WallFront => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround);
        public bool WallBack => Physics2D.Raycast(WallCheck.position, Vector2.right * -Movement.FacingDirection, wallCheckDistance, whatIsGround);
        public bool LedgeHorizontal => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround);
        public bool Ceiling => Physics2D.OverlapCircle(CeilingCheck.position, ceilingCheckDistance, whatIsGround);
        public bool LedgeVertical => Physics2D.Raycast(LedgeCheckVertical.position,Vector2.down, wallCheckDistance, whatIsGround);
    }
}
