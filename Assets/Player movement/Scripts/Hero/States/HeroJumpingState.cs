using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroJumpingState : HeroBaseState
{
    private readonly int JumpingHash = Animator.StringToHash("Jumping");
    private const float CrossFadeDuration = 0.1f;

    private Vector3 momentum;
    private int airJumpsUsed;


    public HeroJumpingState(HeroStateMachine stateMachine, int airJumpsUsed = 0) : base(stateMachine) 
    {
        this.airJumpsUsed = airJumpsUsed;
    }

    public override void Enter()
    {
        if (airJumpsUsed < stateMachine.MaxAirJumps) {
            stateMachine.InputReader.JumpEvent += OnJump;
        }
        stateMachine.InputReader.DashEvent += OnDash;

        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);

        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(JumpingHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime) 
    {
        //Move(momentum, deltaTime);

        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.AerialMovementSpeed + momentum, deltaTime);

        if (stateMachine.Controller.velocity.y <= 0f) {
            stateMachine.SwitchState(new HeroFallingState(stateMachine));
            return;
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
