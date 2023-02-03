using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using SplineMesh;

namespace SplineMeshExtensions
{ 
    [ExecuteInEditMode]
    public class SetPositionToSpline  : MonoBehaviour
    {
        public Spline spline;
        [Range(0, 1)]
        public float rate;

        private void Update()
        {
            CalculatePosition();
        }
        
        void EditorUpdate()
        {
            CalculatePosition();
        }

        private void OnEnable()
        {
#if UNITY_EDITOR
            EditorApplication.update += EditorUpdate;
#endif
        }

        void OnDisable()
        {
#if UNITY_EDITOR
            EditorApplication.update -= EditorUpdate;
#endif
        }

        private void CalculatePosition()
        {
            if(spline is null) return; 
            transform.position = spline.transform.position +  spline.GetSampleAtDistance(rate*spline.Length).location;
        }
    }
}