using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CameraAim
{
    public class CameraTargetSelector
    {
        private readonly Transform _source;
        private readonly Camera _camera;
        private readonly LayerMask _layerMask;
        private readonly float _radius;
        private readonly float _ignoreRadius;
        private List<Transform> ignoreTargets = new List<Transform>();


        public CameraTargetSelector(Transform source, Camera camera, LayerMask layerMask, float radius = 50f, float ignoreRadius = 0f)
        {
            _source = source;
            _camera = camera;
            _layerMask = layerMask;
            _radius = radius;
            _ignoreRadius = ignoreRadius;
        }

        public Collider FindClosestVisibleCollider()
        {
            foreach (var c in GetPlatformColliders())
            {
                if (IsVisible(c) && WithinDistance(c) && NotIgnoreTarget(c))
                {
                    return c;
                }
            }

            return null;
        }
        
        public bool TransformIsIgnored(Transform t)
        {
            return ignoreTargets.Contains(t);
        }

        public void AddIgnoreTarget(Transform t)
        {
            ignoreTargets.Add(t);
        }

        public void ResetIgnoreTarget()
        {
            ignoreTargets.Clear();
        }

        private bool NotIgnoreTarget(Collider c)
        {
            return !ignoreTargets.Contains(c.transform);
        }

        private bool WithinDistance(Collider c)
        {
            return Vector3.Distance(_source.position, c.transform.position) > _ignoreRadius;
        }

        private Collider[] GetPlatformColliders()
        {
            var colliders = Physics.OverlapSphere(_source.position, _radius, _layerMask);
            return colliders.OrderBy(c => (_source.position - c.transform.position).sqrMagnitude).ToArray();
        }

        private bool IsVisible(Collider collider)
        {
            Plane[] cameraFrustrum = GeometryUtility.CalculateFrustumPlanes(_camera);
            return GeometryUtility.TestPlanesAABB(cameraFrustrum, collider.bounds);
        }
    }
}