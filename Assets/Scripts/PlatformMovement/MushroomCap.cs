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
        public float bounceCheckRadius = 2;
        public float bounceSpeed = 10;
        public Vector3 bounceCheckOffset;
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
            transform.DOLocalRotateQuaternion(rotations[currentRotation], duration).SetEase(Ease.InOutElastic);
        }
        
        public void Expand(float duration)
        {
            float blend = expanded ? 100 : 10;
            DOTween.To(() => blend, x => blend = x, blend, duration).SetEase(Ease.InOutElastic) .OnUpdate(() => {
                renderer.SetBlendShapeWeight(0, blend);
                
                if (!expanded && !sentBounceEvent && blend < 30 && Physics.CheckSphere(transform.position + bounceCheckOffset, bounceCheckRadius))
                {
                    sentBounceEvent = true;
                    GroundedEvents.current.BounceUpdate(transform.up * bounceSpeed);
                }
                
            }).OnComplete(() =>
            {
                sentBounceEvent = false;
                expanded = !expanded;
            });
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + bounceCheckOffset, bounceCheckRadius);
        }
    }
}