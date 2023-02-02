using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDashState : HeroBaseState
{
    private readonly int DashBlendTreeHash = Animator.StringToHash("DashBlendTree");
    private readonly int DashForwardHash = Animator.StringToHash("DashForward");
    private readonly int DashRightHash = Animator.StringToHash("DashRight");
    private const float CrossFadeDuration = 0.1f;

    private Vector3 dashDirectionInput;
    private float remainingDashTime;


    public HeroDashState(HeroStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine) 
    {
        this.dashDirectionInput = dodgingDirectionInput;
    }

    public override void Enter() 
    {
        remainingDashTime = stateMachine.DashDuration;

        stateMachine.Animator.SetFloat(DashForwardHash, dashDirectionInput.y);
        stateMachine.Animator.SetFloat(DashRightHash, dashDirectionInput.x);
        stateMachine.Animator.CrossFadeInFixedTime(DashBlendTreeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime) 
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * dashDirectionInput.x * stateMachine.DashDistance / stateMachine.DashDuration;
        movement += stateMachine.transform.forward * dashDirectionInput.y * stateMachine.DashDistance / stateMachine.DashDuration;

        Move(movement, deltaTime);

        //FaceMovementDirection();

        remainingDashTime -= deltaTime;

        if (remainingDashTime <= 0f) {
            stateMachine.SwitchState(new HeroFreeLookState(stateMachine));
        }
    }

    public override void Exit() {}
}
