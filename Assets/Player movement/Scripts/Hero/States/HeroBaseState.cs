using UnityEngine;
using GGJ.StateMachine;

public abstract class HeroBaseState : State 
{
    protected HeroStateMachine stateMachine;
    protected float groundedDelta;


    public HeroBaseState(HeroStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }
    
    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    public override void Tick(float deltaTime)
    {
        SetGroundedDelta(deltaTime);
    }

    protected void SetGroundedDelta(float deltaTime)
    {
        if (!stateMachine.Grounded)
        {
            groundedDelta += deltaTime;
        }
        else
        {
            groundedDelta = 0;
        }
    }

    protected Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.mainCameraTransform.forward;
        Vector3 right = stateMachine.mainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x;
    }

    protected void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        if (movement == Vector3.zero) return;
        
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationDamping
        );
    }

    protected void ReturnToLocomotion(bool transferMomentum = true)
    {
        if (!stateMachine.Grounded) {
            stateMachine.SwitchState(new HeroFallingState(stateMachine, transferMomentum));
        } else {
            stateMachine.SwitchState(new HeroFreeLookState(stateMachine));
        }
    }
}
