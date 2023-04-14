using System.Collections;
using System.Collections.Generic;
using _Scripts.Player.PlayerStates.SuperStates;
using UnityEngine;

public class P_DashState : P_AbilityState
{
    public bool CanDash { get;private set; }

    private bool isHolding;
    private bool dashInputStopped;

    private float lastDashTime;

    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;
    private Vector2 lastAfterImagePosition;
    public P_DashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        player.InputHandler.UseDashInput();

        isHolding = true;
        if(Movement)
        dashDirection = Vector2.right * Movement.FacingDirection;

        Time.timeScale = playerData.holdTimeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        startTime = Time.unscaledTime;

        player.DashDirectionIndicator.gameObject.SetActive(true);


    }

    public override void Exit()
    {
        base.Exit();

        if (Movement)
        {
            if (Movement.CurrentVelocity.y > 0)
            {
                Movement.SetVelocityY(Movement.CurrentVelocity.y * playerData.dashEndYMultiplier);
            }
        }

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            if (Movement)
            {
                player.Anim.SetFloat("yVelocity", Movement.CurrentVelocity.y);
                player.Anim.SetFloat("xVelocity", Movement.CurrentVelocity.x);
            }

            if (isHolding)
            {
                dashInputStopped = player.InputHandler.DashInputStopped;
                dashDirectionInput = player.InputHandler.DashDirectionInput;
                if(dashDirectionInput != Vector2.zero)
                {
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right,dashDirection);
                player.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

                if(dashInputStopped || Time.unscaledTime >= startTime + playerData.maxHoldTime)
                {
                    isHolding = false;
                    Time.fixedDeltaTime = 0.02f;
                    Time.timeScale = 1f;
                    startTime = Time.time;
                    if (Movement)
                    {
                        Movement.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                        Movement.SetVelocity(playerData.dashVelocity, dashDirection);
                    }

                    player.RB.drag = playerData.drag;
                    player.DashDirectionIndicator.gameObject.SetActive(false);
                    PlaceAfterImage();
                }
            }
            else
            {
                if(Movement)
                Movement.SetVelocity(playerData.dashVelocity,dashDirection);
                CheckIfShouldPlaceAfterImage();
                if(Time.time>=startTime + playerData.dashTime)
                {
                    player.RB.drag = 0f;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
        }
    }
    private void CheckIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(player.transform.position, lastAfterImagePosition) >= playerData.distanceBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }
    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAfterImagePosition = player.transform.position;
    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= playerData.dashCooldown + lastDashTime;
    }

    public void ResetCanDash()
    {
        CanDash = true;
    }

}
