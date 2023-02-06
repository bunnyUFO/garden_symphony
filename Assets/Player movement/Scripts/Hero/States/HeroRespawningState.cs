using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRespawningState : HeroBaseState
{
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;


    public HeroRespawningState(HeroStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() 
    {
        stateMachine.InputReader.enabled = false;
        stateMachine.ForceReceiver.Reset();

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f);
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);

        stateMachine.Ragdoll.ToggleRagdoll(true);
    }

    public override void Tick(float deltaTime) {}

    public override void Exit() 
    {
        stateMachine.InputReader.enabled = true;

        stateMachine.Ragdoll.ToggleRagdoll(false);
    }
}
