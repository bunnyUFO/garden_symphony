using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using SplineMesh;
using UnityEngine.UIElements;

namespace SplineMeshExtensions
{ 
    [ExecuteInEditMode]
    public class SetPositionToSpline  : MonoBehaviour
    {
        public Spline spline;
        [Range(0, 1)]
        public float rate;
        public Vector3 offset;

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
            // will break if spline or parent has a rotation
            transform.position = spline.transform.position +  spline.GetSampleAtDistance(rate*spline.Length).location + offset;
        }
    }
}