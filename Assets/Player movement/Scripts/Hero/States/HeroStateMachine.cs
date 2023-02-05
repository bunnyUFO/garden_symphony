using UnityEngine;
using GGJ.StateMachine;
using RythmFramework;

public class HeroStateMachine : StateMachine
{
    [field: Header("Movement")]
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float AerialMovementSpeed { get; private set; }
    [field: SerializeField] public float HoverMovementSpeed { get; private set; }
    [field: SerializeField] public float MaxJumpSpeed { get; private set; }
    [field: SerializeField] public Vector3 PlatformVelocity { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField, Range(0, 1) ] public float MomentumFactor { get; private set; }
    [field: SerializeField] public float DashDuration { get; private set; }
    [field: SerializeField] public float DashDistance { get; private set; }
    [field: SerializeField] public float HoverDuration { get; private set; }

    [field: Header("Particle Effects")]
    [field: SerializeField] public ParticleSystem DustRingParticles { get; private set; }
    [field: SerializeField] public ParticleSystem FeatherDropParticles { get; private set; }
    [field: SerializeField] public ParticleSystem SpeedRayParticles { get; private set; }

    [field: Header("Components")]
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public AbilityTracker AbilityTracker { get; private set; }
    [field: SerializeField] public bool Grounded { get; private set; }
    [field: SerializeField] public bool OnPlatform { get; private set; }

    public Transform mainCameraTransform { get; private set; }


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mainCameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        SwitchState(new HeroFreeLookState(this));
        GroundedEvents.current.OnGrounded += OnGrounded;
        GroundedEvents.current.OnPlatform += PlatformUpdate;
    }

    private void OnGrounded(bool isGrounded)
    {
        Grounded = isGrounded;
    }
    
    private void PlatformUpdate(bool onPlatform, Vector3 velocity)
    {
        OnPlatform = onPlatform;
        PlatformVelocity = velocity;
    }
}
