using System.Collections;
using System.Collections.Generic;
using _Scripts.Core;
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
    public Core Core { get; private set; }
    #endregion

    #region CheckTransforms

    #endregion

    #region Other Variables

    private Vector2 workSpace;
    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

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
        Anim = GetComponent<Animator>();

        RB = GetComponent<Rigidbody2D>();

        InputHandler = GetComponent<PlayerInputHandler>();

        PlayerCollider = GetComponent<BoxCollider2D>();

        DashDirectionIndicator = transform.Find("DashDirectionIndicator");

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
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



    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    #endregion
}
