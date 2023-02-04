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
    }
}