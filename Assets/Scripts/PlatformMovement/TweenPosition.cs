using System;
using Misc;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PlatformMovement
{
    public class TweenPosition : MonoBehaviour
    {
        public List<Transform> positions = new List<Transform>();
        public float duration = 1;
        public int currentPosition = 0;
        
        private LateExecute _lateExecute;

        private void Awake()
        {
            _lateExecute = new LateExecute(this);
        }

        public void TweenNext()
        {
            currentPosition = (currentPosition + 1) % positions.Count;
            transform.DOMove(positions[currentPosition].position, duration);
        }
        
        public void TweenNext(float delay)
        {
            _lateExecute.DelayExecute(delay , x=> TweenNext());
        }
    }
}