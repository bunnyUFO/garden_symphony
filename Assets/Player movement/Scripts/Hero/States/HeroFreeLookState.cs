using UnityEngine;

public class HeroFreeLookState : HeroBaseState
{
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int IdleBlendAnimHash = Animator.StringToHash("IdleBlendAnim");
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;
    private float chanceToChangeIdleAnim = 0.50f;
    private int countIdleAnims = 2;
    private float currentIdleState = 0f;


    public HeroFreeLookState(HeroStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() 
    {
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.DashEvent += OnDash;

        stateMachine.AbilityTracker.Reset();

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f);
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (!stateMachine.Grounded)
        {
            stateMachine.SwitchState(new HeroFallingState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement();
        
        //prevent from walking off ledge
        if(stateMachine.OnLedge && stateMachine.Grounded &&  Vector3.Dot( stateMachine.transform.forward, movement) > 0) movement = Vector3.zero;

        bool resetVerticalSpeed = stateMachine.OnPlatform && stateMachine.PlatformYOffset < 0.1 && stateMachine.PlatformYOffset < -0.1;
        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime, stateMachine.OnPlatform, resetVerticalSpeed);

        if (stateMachine.InputReader.MovementValue == Vector2.zero) {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            SelectRandomIdleAnimation(deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit() 
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.DashEvent -= OnDash;
    }

    private void SelectRandomIdleAnimation(float deltaTime)
    {
        stateMachine.Animator.SetFloat(IdleBlendAnimHash, currentIdleState, AnimatorDampTime, deltaTime);

        if (stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 > 0.98f) {
            if (Random.Range(0, 1) <= chanceToChangeIdleAnim) {
                currentIdleState = Random.Range(0, countIdleAnims);
            }
        }
    }

    private void OnJump()
    {
        // Disable jumping while moving fast on platforms because it causes issues
        if (stateMachine.PlatformVelocity.magnitude < 6)
        {
            stateMachine.Controller.SimpleMove(CalculateMovement()*3.5f); 
            stateMachine.SwitchState(new HeroJumpingState(stateMachine, true));   
        }
    }

    private void OnDash()
    {
        if (stateMachine.AbilityTracker.TryAddAbility("Dash")) {
            stateMachine.SwitchState(new HeroDashingState(stateMachine, Vector2.up));
        }
    }
    
    private void OnBounce()
    {
        stateMachine.SwitchState(new HeroJumpingState(stateMachine, true));
    }
}
