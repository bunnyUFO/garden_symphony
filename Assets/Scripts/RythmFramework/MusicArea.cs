using System;
using UnityEngine;

namespace RythmFramework
{
    public class MusicArea : MonoBehaviour
    {
        public string label;
        private void OnTriggerEnter(Collider other)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("LevelSection", label);
        }
    }
}