using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using FMOD.Studio;
using UnityEngine;

namespace RythmFramework
{
    public class EventlListener : MonoBehaviour
    {
        public bool beat;
        public List<string> markers = new List<string>();

        private void Start()
        {
            if(beat) Events.current.OnBeat += OnBeat;
            if(markers.Count > 0) Events.current.OnBeatMarker += OnMarker;
        }

        public void OnBeat()
        {
            Debug.Log("It's a beat event!");
        }
        
        public void OnMarker(string markerName)
        {
            if (markers.Contains(markerName))
            {
                Debug.Log($"It's a {markerName} marker event!");
            }
        }
    }
}