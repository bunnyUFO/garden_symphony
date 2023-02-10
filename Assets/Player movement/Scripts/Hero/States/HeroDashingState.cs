using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDashingState : HeroBaseState
{
    private readonly int DashingHash = Animator.StringToHash("Dashing");
    private const float CrossFadeDuration = 0.1f;

    private Vector3 dashDirectionInput;
    private float remainingDashTime;


    public HeroDashingState(HeroStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine) 
    {
        this.dashDirectionInput = dodgingDirectionInput;
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
        Vector3 movement = new Vector3();
        stateMachine.ForceReceiver.Reset();

        movement += stateMachine.transform.right * dashDirectionInput.x * stateMachine.DashDistance / stateMachine.DashDuration;
        movement += stateMachine.transform.forward * dashDirectionInput.y * stateMachine.DashDistance / stateMachine.DashDuration;

        Move(movement, deltaTime, stateMachine.OnPlatform, true);

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
