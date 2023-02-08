using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using RythmFramework;

namespace DefaultNamespace
{
    public class MushroomCap : MonoBehaviour
    {
        public List<Quaternion> rotations = new List<Quaternion>();
        public Ease rotationEase;
        public Ease expandEase;
        private int currentRotation = 0;
        private SkinnedMeshRenderer renderer;
        private MeshCollider meshCollider;
        private bool expanded  = true;
        private bool sentBounceEvent  = false; 

        public void Start()
        {
            renderer = GetComponent<SkinnedMeshRenderer>();
            meshCollider = GetComponent<MeshCollider>();
        }

        private void FixedUpdate()
        {
            Mesh bakeMesh = new Mesh();
            renderer.BakeMesh(bakeMesh);
            meshCollider.sharedMesh = bakeMesh;
        }

        public void NextRotation(float duration)
        {
            currentRotation = (currentRotation + 1) % rotations.Count;
            transform.DOLocalRotateQuaternion(rotations[currentRotation], duration).SetEase(rotationEase);
        }
        
        public void Expand(float duration)
        {
            float blend = renderer.GetBlendShapeWeight(0);
            float newBlend = expanded ? 100 : 0;
            DOTween.To(() => blend, x => blend = x, newBlend, duration).SetEase(expandEase).OnUpdate(() => {
                renderer.SetBlendShapeWeight(0, blend);
            }).OnComplete(() =>
            {
                expanded = !expanded;
            });
        }
    }
}