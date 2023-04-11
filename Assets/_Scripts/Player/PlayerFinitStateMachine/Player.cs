using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public P_IdleState IdleState { get; private set; }
    public P_MoveState MoveState { get; private set; }
    public P_JumpState JumpState { get; private set; }
    public P_InAirState InAirState { get; private set; }
    public P_LandState LandState { get; private set; }
    public P_WallSlideState WallSlideState { get;private set; }
    public P_WallGrabState WallGrabState { get; private set; }
    public P_WallClimbState WallClimbState { get; private set; }
    public P_WallJumpState WallJumpState { get; private set; }
    public P_LedgeClimbState LedgeClimbState { get; private set; }
    public P_DashState DashState { get; private set; }
    public P_CrouchIdleState CrouchIdleState { get; private set; }
    public P_CrouchMoveState CrouchMoveState { get; private set; }
    public P_AttackState primaryAttackState { get; private set; }
    public P_AttackState secondaryAttackState { get; private set; }

    [SerializeField]
    PlayerData playerData;
    #endregion

    #region Components
    public Animator Anim { get; set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; set; }
    public Transform DashDirectionIndicator { get;private set; }
    public BoxCollider2D PlayerCollider { get;private set; }
    public PlayerInventory Inventory { get; set; }
    #endregion

    #region CheckTransforms
    [SerializeField]
    private Transform 
        groundCheck,
        wallCheck,
        ledgeCheck,
        ceilingCheck;
    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; set; }
    public int FacingDirection { get; set; }


    private Vector2 workSpace;
    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new P_IdleState(this, StateMachine, playerData, "idle");

        MoveState = new P_MoveState(this, StateMachine, playerData, "move");

        JumpState = new P_JumpState(this, StateMachine, playerData, "inAir");

        InAirState = new P_InAirState(this, StateMachine, playerData, "inAir");

        LandState = new P_LandState(this, StateMachine, playerData, "land");

        WallClimbState = new P_WallClimbState(this, StateMachine, playerData, "wallClimb");

        WallSlideState = new P_WallSlideState(this, StateMachine, playerData, "wallSlide");

        WallGrabState = new P_WallGrabState(this, StateMachine, playerData, "wallGrab");

        WallJumpState = new P_WallJumpState(this, StateMachine, playerData, "inAir");

        LedgeClimbState = new P_LedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");

        DashState = new P_DashState(this, StateMachine, playerData, "inAir");

        CrouchIdleState = new P_CrouchIdleState(this, StateMachine, playerData, "crouchIdle");

        CrouchMoveState = new P_CrouchMoveState(this, StateMachine, playerData, "crouchMove");

        primaryAttackState = new P_AttackState(this, StateMachine, playerData, "attack");

        secondaryAttackState = new P_AttackState(this, StateMachine, playerData, "attack");
    }

    private void Start()
    {
        FacingDirection = 1;

        Anim = GetComponent<Animator>();

        RB = GetComponent<Rigidbody2D>();

        InputHandler = GetComponent<PlayerInputHandler>();

        PlayerCollider = GetComponent<BoxCollider2D>();

        DashDirectionIndicator = transform.Find("DashDirectionIndicator");

        Inventory = GetComponent<PlayerInventory>();

        primaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);

        //secondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.secondary]);

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;

        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region SetFunctions

    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    public void SetVelocity(float velocity, Vector2 angle,int direction)
    {
        angle.Normalize();
        workSpace.Set(angle.x * velocity * direction,angle.y * velocity);
        RB.velocity = workSpace;
        CurrentVelocity = workSpace;
    }
    public void SetVelocity(float velocity, Vector2 direction)
    {
        workSpace = direction * velocity;
        RB.velocity = workSpace;
        CurrentVelocity = workSpace;
    }
    public void SetVelocityX(float velocity)
    {
        workSpace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    public void SetVelocityY(float velocity)
    {
        workSpace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput !=0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    public bool CheckIfGrounded() => Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    public bool CheckIfTouchingWall() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    public bool CheckIfTouchingWallBack() => Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    public bool CheckIfTouchingLedge() => Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    public bool CheckIfTouchingCeiling() => Physics2D.OverlapCircle(ceilingCheck.position, playerData.ceilingCheckDistance, playerData.whatIsGround);
    #endregion

    #region Other Functions
    public void SetColliderHeight(float height)
    {
        Vector2 center = PlayerCollider.offset;
        workSpace.Set(PlayerCollider.size.x, height);
        center.y += (height - PlayerCollider.size.y) / 2;
        PlayerCollider.size = workSpace;
        PlayerCollider.offset = center;
    }
    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position,Vector2.right * FacingDirection,playerData.wallCheckDistance,playerData.whatIsGround);
        float xDistance = xHit.distance;
        workSpace.Set((xDistance + 0.015f) * FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workSpace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y + 0.015f, playerData.whatIsGround);
        float yDistance = yHit.distance;
        workSpace.Set(wallCheck.position.x + (xDistance * FacingDirection),ledgeCheck.position.y - yDistance);
        return workSpace;
    }
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    #endregion
}
