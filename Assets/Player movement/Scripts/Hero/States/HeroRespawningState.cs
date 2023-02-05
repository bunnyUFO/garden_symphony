using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRespawningState : HeroBaseState
{
    public HeroRespawningState(HeroStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() 
    {
        stateMachine.InputReader.enabled = false;
        stateMachine.ForceReceiver.Reset();
    }

    public override void Tick(float deltaTime) {}

    public override void Exit() 
    {
        stateMachine.InputReader.enabled = true;
    }
}
