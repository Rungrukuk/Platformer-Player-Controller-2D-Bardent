using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;
    public Vector2 RawMovementInput { get; private set; }

    public Vector2 RawDashDirectionInput { get;private set; }

    public Vector2Int DashDirectionInput { get;private set; }

    public int NormalizedInputX { get; private set; }

    public int NormalizedInputY { get; private set; }

    public bool JumpInput { get; private set; }

    public bool JumpInputStopped { get;private set; }

    public bool GrabInput { get;private set; }

    public bool DashInput { get;private set; }

    public bool DashInputStopped { get; private set; }

    public bool[] AttackInputs { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.2f;
    private float jumpInputStartTime;
    private float DasInputStartTime;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        cam = Camera.main;
        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];
    }
    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.primary] = true;
        }
        if (context.canceled)
        {
            AttackInputs[((int)CombatInputs.primary)] = false;
        }
    }
    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.secondary] = true;
        }
        if (context.canceled)
        {
            AttackInputs[((int)CombatInputs.secondary)] = false;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        if (Mathf.Abs(RawMovementInput.x) > 0.68f)
            NormalizedInputX = (int)(RawMovementInput.x * Vector2.right).normalized.x;
        else
            NormalizedInputX = 0;

        if(Mathf.Abs(RawMovementInput.y)>0.68f)
            NormalizedInputY = (int)(RawMovementInput.y * Vector2.up).normalized.y;
        else
            NormalizedInputY = 0;
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            JumpInput = true;
            JumpInputStopped = false;
            jumpInputStartTime = Time.time;
        }
        if (context.canceled)
        {
            JumpInputStopped = true;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStopped = false;
            DasInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            DashInputStopped = true;
        }
    }

    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();
        if(playerInput.currentControlScheme == "Keyboard")
        {
            RawDashDirectionInput = cam.ScreenToWorldPoint(RawDashDirectionInput) - transform.position;
        }
        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }
        if (context.canceled)
        {
            GrabInput = false;
        }
    }

    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;
    private void CheckJumpInputHoldTime()
    {
        if(Time.time>=jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
    private void CheckDashInputHoldTime()
    {
        if (Time.time >= DasInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }
}

public enum CombatInputs
{
    primary,
    secondary
}