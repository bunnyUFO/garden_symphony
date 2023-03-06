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

        public event Action<bool, Transform> OnGrounded;

        public void Grounded(bool grounded, Transform ground)
        {
            if (OnGrounded != null)
            {
                OnGrounded(grounded, ground);
            }
        }
        
        public event Action<bool, Vector3, Vector3> OnPlatform;

        public void PlatformUpdate(bool onPlatform, Vector3 velocity, Vector3 displacement)
        {
            if (OnPlatform != null)
            {
                OnPlatform(onPlatform, velocity, displacement);
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
        
        public event Action<bool> OnLedge;

        public void LedgeUpdate(bool ledge)
        {
            if (OnLedge != null)
            {
                OnLedge(ledge);
            }
        }
    }
}