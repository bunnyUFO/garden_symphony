using System;
using System.Runtime.InteropServices;
using FMOD.Studio;
using UnityEngine;

namespace RythmFramework
{
    public class BeatEvents : MonoBehaviour
    {
        public static BeatEvents current;

        private void Awake()
        {
            current = this;
        }

        public event Action OnBeat;
        public event Action<string> OnBeatMarker;

        public void Beat()
        {
            if (OnBeat != null)
            {
                OnBeat();
            }
        }
        
        
        public void BeatMarker(string marker)
        {
            if (OnBeatMarker != null)
            {
                OnBeatMarker(marker);
            }
        }
        
    }
}