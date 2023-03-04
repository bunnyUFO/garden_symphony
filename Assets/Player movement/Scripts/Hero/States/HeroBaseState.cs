using UnityEngine;
using GGJ.StateMachine;

public abstract class HeroBaseState : State
{
    protected HeroStateMachine stateMachine;

    public HeroBaseState(HeroStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime, bool onPlatform = false, bool useGravity = true)
    {
        if (!stateMachine.Controller.enabled) return;
        var verticalVelocity = useGravity ? CalculateGravity(onPlatform) : Vector3.zero;
        stateMachine.Controller.Move((motion + verticalVelocity + stateMachine.PlatformVelocity) * deltaTime);
    }

    private Vector3 CalculateGravity(bool onPlatform)
    {
        Vector3 verticalVelocity = stateMachine.ForceReceiver.Movement;

        if (onPlatform)
        {
            verticalVelocity = Vector3.down * 2f;
            if (UnderPlatform() && PlatformMovingUpFast())
            {
                verticalVelocity = Vector3.up * (stateMachine.PlatformVelocity.y / 10);
            }
        }

        return verticalVelocity;
    }

    private bool PlatformMovingUpFast()
    {
        return stateMachine.PlatformVelocity.y > 5;
    }

    private bool UnderPlatform()
    {
        return stateMachine.PlatformYOffset < 0;
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
        else if (!stateMachine.Grounded)
        {
            stateMachine.SwitchState(new HeroFallingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new HeroFreeLookState(stateMachine));
        }
    }
}