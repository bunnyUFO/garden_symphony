using System;
using Misc;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PlatformMovement
{
    public class TweenPositionVector : MonoBehaviour
    {
        public List<Vector3> positions = new List<Vector3>();
        public float duration = 1;
        public int currentPosition = 0;
        public bool debug;
        public float debugRadius;
        public Ease easeType;
        private Vector3 _initialPosition;
        
        private LateExecute _lateExecute;

        private void Awake()
        {
            _initialPosition = new Vector3(transform.position.x,transform.position.y, transform.position.z);
            _lateExecute = new LateExecute(this);
        }

        public void TweenNext()
        {
            currentPosition = (currentPosition + 1) % positions.Count;
            transform.DOMove(_initialPosition + positions[currentPosition], duration).SetEase(easeType);
        }
        
        public void TweenNext(float delay)
        {
            _lateExecute.DelayExecute(delay , x=> TweenNext());
        }
        
        private void OnDrawGizmos()
        {
            if (!debug) return;

            Gizmos.color = Color.yellow;
            foreach (var position in positions)
            {
                Gizmos.DrawSphere(transform.position + position, debugRadius);
            }
        }
    }
}