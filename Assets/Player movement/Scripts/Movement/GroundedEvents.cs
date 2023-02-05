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

        public void PlatformUpdate(bool grounded, Vector3 velocity)
        {
            if (OnPlatform != null)
            {
                OnPlatform(grounded, velocity);
            }
        }
    }
}