using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroLandingState : HeroBaseState
{
    private readonly int LandingHash = Animator.StringToHash("Landing");
    private const float CrossFadeDuration = 0.1f;

    public HeroLandingState(HeroStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() 
    {
        stateMachine.Animator.CrossFadeInFixedTime(LandingHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime) 
    {
        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Landing");

        if (normalizedTime >= 1f) {
            stateMachine.SwitchState(new HeroFreeLookState(stateMachine));
        }
    }

    public override void Exit() {}
}
