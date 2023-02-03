using System;
using System.Runtime.InteropServices;
using FMOD.Studio;
using UnityEngine;

namespace RythmFramework
{
    public class FmodListener : MonoBehaviour 
    {
        [SerializeField]
        private FMODUnity.EventReference eventReference;

        private EventInstance instance;
        private EVENT_CALLBACK cb;

        void Start()
        {
            instance = FMODUnity.RuntimeManager.CreateInstance(eventReference);
            cb = new EVENT_CALLBACK(StudioEventCallback);
            instance.setCallback(cb, EVENT_CALLBACK_TYPE.TIMELINE_MARKER | EVENT_CALLBACK_TYPE.TIMELINE_BEAT);
            instance.start();
        }
        
        void OnDisable()
        {
            instance.setUserData(IntPtr.Zero);
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instance.release();
        }
        
        [AOT.MonoPInvokeCallback(typeof(EVENT_CALLBACK))]
        public FMOD.RESULT StudioEventCallback(EVENT_CALLBACK_TYPE type, IntPtr eventInstance, IntPtr parameters)
        {
            if (type == EVENT_CALLBACK_TYPE.TIMELINE_MARKER)
            {
                TIMELINE_MARKER_PROPERTIES marker = (TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameters, typeof(TIMELINE_MARKER_PROPERTIES));
                string markerName = (string)marker.name;
                BeatEvents.current.BeatMarker(markerName);
            }
            if (type == EVENT_CALLBACK_TYPE.TIMELINE_BEAT)
            {
                // TIMELINE_BEAT_PROPERTIES beat = (TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameters, typeof(TIMELINE_BEAT_PROPERTIES));
                BeatEvents.current.Beat();
            }
            return FMOD.RESULT.OK;
        }
    }
}