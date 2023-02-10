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
        if (! stateMachine.Controller.enabled) return;
        
        Vector3 verticalVelocity = stateMachine.ForceReceiver.Movement;
        
        //modified gravity while on platforms
        if (onPlatform)
        {
            verticalVelocity = stateMachine.PlatformVelocity.y < 1 ? Vector3.down * 2 : Vector3.zero;
        }

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

    protected void ReturnToLocomotion()
    {
        if (stateMachine.OnPlatform)
        {
            stateMachine.SwitchState(new HeroFreeLookState(stateMachine));
        }
        else if (!stateMachine.Grounded) {
            stateMachine.SwitchState(new HeroFallingState(stateMachine));
        } else {
            stateMachine.SwitchState(new HeroFreeLookState(stateMachine));
        }
    }
}
