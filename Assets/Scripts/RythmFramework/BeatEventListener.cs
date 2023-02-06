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
        public List<int> beatCounts = new List<int>();
        public List<int> beatOffsets = new List<int>();
        public List<UnityEvent> beatActions = new List<UnityEvent>();
        public List<string> markers = new List<string>();
        public List<UnityEvent> markerActions = new List<UnityEvent>();
        public bool ignore;

        private int beatCount = 0;
        
        private void Start()
        {
            if(beatCounts.Count > 0) BeatEvents.current.OnBeat += OnBeat;
            if(markers.Count > 0) BeatEvents.current.OnBeatMarker += OnMarker;
        }
        
        private void OnEnable()
        {
            if(beatCounts.Count > 0) BeatEvents.current.OnBeat += OnBeat;
            if(markers.Count > 0) BeatEvents.current.OnBeatMarker += OnMarker;
        }
        
        
        private void OnDisable()
        {
            if(beatCounts.Count > 0) BeatEvents.current.OnBeat -= OnBeat;
            if(markers.Count > 0) BeatEvents.current.OnBeatMarker -= OnMarker;
        }

        public void OnBeat()
        {
            beatCount++;
            if (ignore) return;
            for (int i = 0; i < beatCounts.Count; i++)
            {
                if ((beatCount+beatOffsets[i]) % beatCounts[i] == 0)
                {
                    beatActions[i].Invoke();
                }
            }
        }
        
        public void OnMarker(string markerName)
        {
            if (ignore) return;
            if (markers.Contains(markerName))
            {
                int markerIndex = markers.IndexOf(markerName);
                if(markerIndex > -1) markerActions[markerIndex].Invoke();
            }
        }
    }
}