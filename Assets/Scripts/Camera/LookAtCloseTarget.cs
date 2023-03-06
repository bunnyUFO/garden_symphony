using UnityEngine;
using Cinemachine;
using RythmFramework;

namespace CameraAim
{
    public class LookAtCloseTarget : MonoBehaviour
    {
        public Transform source;
        public float distance;
        public float targetWeight;
        public LayerMask layerMask;

        private CinemachineTargetGroup _cinemachineTargetGroup;
        private Transform target;
        private CameraTargetSelector _cameraTargetSelector;

        private void Start()
        {
            GroundedEvents.current.OnGrounded += OnGrounded;
            _cinemachineTargetGroup = gameObject.GetComponent<CinemachineTargetGroup>();
            _cameraTargetSelector = new CameraTargetSelector(source, Camera.main, layerMask, distance);
        }

        private void Update()
        {
            Collider c = _cameraTargetSelector.FindClosestVisibleCollider();

            if (c != null)
            {
                if (c != null && _cinemachineTargetGroup.FindMember(c.transform) < 0)
                {
                    ReplaceTarget(c);
                }
            }
            else
            {
                RemoveTarget();
            }
        }

        private void ReplaceTarget(Collider c)
        {
            RemoveTarget();
            _cinemachineTargetGroup.AddMember(c.transform, targetWeight, 0);
            target = c.transform;
        }

        private void RemoveTarget()
        {
            if (target != null) _cinemachineTargetGroup.RemoveMember(target);
        }

        private void OnGrounded(bool grounded, Transform ground)
        {
            if (grounded)
            {
                if (!_cameraTargetSelector.TransformIsIgnored(ground))
                {
                    _cameraTargetSelector.ResetIgnoreTarget();
                    _cameraTargetSelector.AddIgnoreTarget(ground);
                }
            }
        }
    }
}