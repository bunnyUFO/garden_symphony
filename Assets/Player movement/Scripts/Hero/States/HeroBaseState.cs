using UnityEngine;
using GGJ.StateMachine;

public abstract class HeroBaseState : State 
{
    protected HeroStateMachine stateMachine;


    public HeroBaseState(HeroStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    
    protected void Move(Vector3 motion, float deltaTime, bool onPlatform = false, bool resetVerticalSpeed = false)
    {
        Vector3 verticalVelocity = onPlatform ? Vector3.down*2 : stateMachine.ForceReceiver.Movement;
        if(resetVerticalSpeed) verticalVelocity = Vector3.zero;
        stateMachine.Controller.Move((motion + verticalVelocity + stateMachine.PlatformVelocity) * deltaTime);
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
