using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFallingState : HeroBaseState
{
    private readonly int FallingHash = Animator.StringToHash("Falling");
    private const float CrossFadeDuration = 0.1f;

    private Vector3 momentum;
    private int airJumpsUsed;


    public HeroFallingState(HeroStateMachine stateMachine, int airJumpsUsed = 0) : base(stateMachine) 
    {
        this.airJumpsUsed = airJumpsUsed;
    }

    public override void Enter() 
    {
        if (airJumpsUsed < stateMachine.MaxAirJumps) {
            stateMachine.InputReader.JumpEvent += OnJump;
        }
        stateMachine.InputReader.DashEvent += OnDash;

        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(FallingHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime) 
    {
        //Move(momentum, deltaTime);

        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.AerialMovementSpeed + momentum, deltaTime);

        if (stateMachine.Controller.isGrounded) {
            stateMachine.SwitchState(new HeroLandingState(stateMachine));
        }
    }

    public override void Exit() 
    {
        if (airJumpsUsed < stateMachine.MaxAirJumps) {
            stateMachine.InputReader.JumpEvent -= OnJump;
        }
        stateMachine.InputReader.DashEvent += OnDash;
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new HeroJumpingState(stateMachine, airJumpsUsed + 1));
    }

    private void OnDash()
    {
        stateMachine.SwitchState(new HeroDashingState(stateMachine, Vector2.up));
    }
}
