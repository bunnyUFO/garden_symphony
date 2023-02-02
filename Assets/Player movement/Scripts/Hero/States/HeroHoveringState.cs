using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHoveringState : HeroBaseState
{
    private readonly int HoveringHash = Animator.StringToHash("Hovering");
    private const float CrossFadeDuration = 0.1f;

    private float remainingHoverTime;

    public HeroHoveringState(HeroStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() 
    {
        remainingHoverTime = stateMachine.HoverDuration;
        
        stateMachine.Animator.CrossFadeInFixedTime(HoveringHash, CrossFadeDuration);

        Debug.Log($"Hover State!");
    }

    public override void Tick(float deltaTime) 
    {
        remainingHoverTime -= deltaTime;

        if (remainingHoverTime <= 0f || !stateMachine.InputReader.HoverButtonDown) {
            if (stateMachine.Controller.isGrounded) {
                stateMachine.SwitchState(new HeroFreeLookState(stateMachine));
            } else {
                stateMachine.SwitchState(new HeroFallingState(stateMachine));
            }
        }
    }

    public override void Exit() {}
}
