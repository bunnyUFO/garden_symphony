using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroJumpingState : HeroBaseState
{
    private readonly int JumpingHash = Animator.StringToHash("Jumping");
    private const float CrossFadeDuration = 0.1f;

    private Vector3 momentum;


    public HeroJumpingState(HeroStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
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

    public override void Exit() {}
}
