using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroJumpingState : HeroBaseState
{
    private readonly int JumpingHash = Animator.StringToHash("Jumping");
    private readonly int RunningJumpHash = Animator.StringToHash("RunningJump");
    private readonly int DoubleJumpHash = Animator.StringToHash("DoubleJump");
    private const float CrossFadeDuration = 0.1f;

    private Vector3 momentum;
    private bool transferMomentum;
    private float runningJumpSpeed = 0.2f;


    public HeroJumpingState(HeroStateMachine stateMachine, bool transferMomentum = true) : base(stateMachine) 
    {
        this.transferMomentum = transferMomentum;
    }

    public override void Enter()
    {
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.DashEvent += OnDash;
        stateMachine.InputReader.HoverEvent += OnHover;

        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce, transferMomentum);

        if (transferMomentum) {
            momentum = stateMachine.Controller.velocity * stateMachine.MomentumFactor;
            momentum.y = 0f;
        } else {
            momentum = Vector3.zero;
        }

        if (!stateMachine.Controller.isGrounded) {
            stateMachine.Animator.CrossFadeInFixedTime(DoubleJumpHash, CrossFadeDuration);    
        } else if (stateMachine.Controller.velocity.magnitude >= runningJumpSpeed) {
            stateMachine.Animator.CrossFadeInFixedTime(RunningJumpHash, CrossFadeDuration);    
        } else {
            stateMachine.Animator.CrossFadeInFixedTime(JumpingHash, CrossFadeDuration);
        }
    }

    public override void Tick(float deltaTime) 
    {
        //Move(momentum, deltaTime);

        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.AerialMovementSpeed + momentum, deltaTime);

        FaceMovementDirection(movement, deltaTime);

        if (stateMachine.Controller.velocity.y <= 0f) {
            stateMachine.SwitchState(new HeroFallingState(stateMachine));
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
