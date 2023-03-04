using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDashingState : HeroBaseState
{
    private readonly int DashingHash = Animator.StringToHash("Dashing");
    private const float CrossFadeDuration = 0.1f;
    private Vector3 dashDirection;
    private float remainingDashTime;


    public HeroDashingState(HeroStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine)
    {
        dashDirection = new Vector3(dodgingDirectionInput.x, 0f,
                                    dodgingDirectionInput.z);
    }

    public override void Enter() 
    {
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.HoverEvent += OnHover;

        remainingDashTime = stateMachine.DashDuration;

        stateMachine.Animator.CrossFadeInFixedTime(DashingHash, CrossFadeDuration);

        //WebGL does not like serialized objects
        // SoundManager.Instance.PlaySound("Wrenford", "Dash", 5f);
        SoundManager.Instance.PlaySound("event:/SFX/Dash", 4f);

        stateMachine.SpeedRayParticles.Play();
    }

    public override void Tick(float deltaTime) 
    {
        stateMachine.ForceReceiver.Reset();
        var movement = dashDirection * stateMachine.DashDistance / stateMachine.DashDuration;
        Move(movement, deltaTime, stateMachine.OnPlatform, false);

        remainingDashTime -= deltaTime;

        if (remainingDashTime <= 0f) {
            ReturnToLocomotion();
        }
    }

    public override void Exit() 
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.HoverEvent -= OnHover;

        stateMachine.SpeedRayParticles.Stop();
    }

    private void OnJump()
    {
        if (stateMachine.AbilityTracker.TryAddAbility("Jump")) {
            stateMachine.SwitchState(new HeroJumpingState(stateMachine, false));
        }
    }

    private void OnHover()
    {
        if (stateMachine.AbilityTracker.TryAddAbility("Hover")) {
            stateMachine.SwitchState(new HeroHoveringState(stateMachine));
        }
    }
}
