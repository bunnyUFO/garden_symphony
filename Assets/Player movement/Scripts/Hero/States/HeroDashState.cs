using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDashState : HeroBaseState
{
    private readonly int DashHash = Animator.StringToHash("Dash");
    private const float CrossFadeDuration = 0.1f;

    public HeroDashState(HeroStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() 
    {
        stateMachine.Animator.CrossFadeInFixedTime(DashHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime) {}

    public override void Exit() {}
}
