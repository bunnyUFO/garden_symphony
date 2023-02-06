using System;
using System.Runtime.InteropServices;
using FMOD.Studio;
using UnityEngine;

namespace RythmFramework
{
    public class GroundedEvents : MonoBehaviour
    {
        public static GroundedEvents current;

        private void Awake()
        {
            current = this;
        }

        public event Action<bool> OnGrounded;

        public void Grounded(bool grounded)
        {
            if (OnGrounded != null)
            {
                OnGrounded(grounded);
            }
        }
        
        public event Action<bool, Vector3> OnPlatform;

        public void PlatformUpdate(bool onPlatform, Vector3 velocity)
        {
            if (OnPlatform != null)
            {
                OnPlatform(onPlatform, velocity);
            }
        }
        
        public event Action<Vector3> OnBounce;
        
        public void BounceUpdate(Vector3 velocity)
        {
            if (OnBounce != null)
            {
                OnBounce(velocity);
            }
        }
    }
}