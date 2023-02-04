using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using FMOD.Studio;
using UnityEngine;

namespace RythmFramework
{
    public class BeatTester : MonoBehaviour
    {
        public  bool beat;
        public  float beatFrequency;
        public  List<String> markers = new List<string>();
        public  List<float> markerFrquencies = new List<float>();
        public  List<float> markerDelays = new List<float>();

        private List<Coroutine> coroutinesList = new List<Coroutine>();


        void Start()
        {
            coroutinesList.Add(StartCoroutine(BeatEvent(beatFrequency)));;
            for (int i = 0; i < markers.Count; i++)
            {
                coroutinesList.Add(StartCoroutine(BeatEvent(markerFrquencies[i], markerDelays[i],  markers[i])));   
            }
        }
        
        
        void OnDisable()
        {
            foreach (var coroutine in coroutinesList)
            {
                StopCoroutine(coroutine);
            }
        }

        IEnumerator BeatEvent(float  repeatRate, float delay = 0,  string marker = "")
        {
            yield return new WaitForSeconds(delay);
            
            while(true) {
                if(marker == "") BeatEvents.current.Beat();
                if(marker != "")BeatEvents.current.BeatMarker(marker);
                yield return new WaitForSeconds(repeatRate);
            }
        }
    }
}