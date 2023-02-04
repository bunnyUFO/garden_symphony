using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFallingState : HeroBaseState
{
    private readonly int FallingHash = Animator.StringToHash("Falling");
    private const float CrossFadeDuration = 0.1f;

    private Vector3 momentum;
    private bool transferMomentum;


    public HeroFallingState(HeroStateMachine stateMachine, bool transferMomentum = false) : base(stateMachine) 
    {
        this.transferMomentum = transferMomentum;
    }

    public override void Enter() 
    {
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.DashEvent += OnDash;
        stateMachine.InputReader.HoverEvent += OnHover;

        if (transferMomentum) {
            momentum = stateMachine.Controller.velocity;
            momentum.y = 0f;
        } else {
            momentum = Vector3.zero;
        }

        stateMachine.Animator.CrossFadeInFixedTime(FallingHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime) 
    {
        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.AerialMovementSpeed + momentum, deltaTime);

        FaceMovementDirection(movement, deltaTime);

        if (stateMachine.Controller.isGrounded) {
            stateMachine.SwitchState(new HeroLandingState(stateMachine));
            return;
        }
    }

    public override void Exit() 
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.DashEvent -= OnDash;
        stateMachine.InputReader.HoverEvent -= OnHover;
    }

    private void OnJump()
    {
        if (stateMachine.AbilityTracker.TryAddAbility("Jump")) {
            stateMachine.SwitchState(new HeroJumpingState(stateMachine, false));
        }
    }

    private void OnDash()
    {
        if (stateMachine.AbilityTracker.TryAddAbility("Dash")) {
            stateMachine.SwitchState(new HeroDashingState(stateMachine, Vector2.up));
        }
    }

    private void OnHover()
    {
        if (stateMachine.AbilityTracker.TryAddAbility("Hover")) {
            stateMachine.SwitchState(new HeroHoveringState(stateMachine));
        }
    }
}
