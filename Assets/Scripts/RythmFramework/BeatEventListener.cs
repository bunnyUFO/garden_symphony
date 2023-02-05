using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Events;

namespace RythmFramework
{
    public class BeatEventListener : MonoBehaviour
    {
        public bool beat;
        public UnityEvent beatAction;
        
        public List<string> markers = new List<string>();
        public List<UnityEvent> markerActions = new List<UnityEvent>();

        private void Start()
        {
            if(beat) BeatEvents.current.OnBeat += OnBeat;
            if(markers.Count > 0) BeatEvents.current.OnBeatMarker += OnMarker;
        }

        public void OnBeat()
        {
            if(beat) beatAction.Invoke();
        }
        
        public void OnMarker(string markerName)
        {
            if (markers.Contains(markerName))
            {
                int markerIndex = markers.IndexOf(markerName);
                if(markerIndex > -1) markerActions[markerIndex].Invoke();
            }
        }
    }
}