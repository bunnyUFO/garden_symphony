using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFallingState : HeroBaseState
{
    private readonly int FallingHash = Animator.StringToHash("Falling");
    private const float CrossFadeDuration = 0.1f;

    private Vector3 momentum;


    public HeroFallingState(HeroStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() 
    {
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

    public override void Exit() {}
}
