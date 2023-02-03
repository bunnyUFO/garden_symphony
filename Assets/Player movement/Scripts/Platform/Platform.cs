using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Platform
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float waypointDistanceTolerance = 1f;
        [SerializeField] float waypointWaitTime = 3f;
        [SerializeField] PlatformPath path;

        int currentWaypointIndex = 0;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;


        private void FixedUpdate()
        {
            if (path == null) return;

            if (AtWaypoint()) {
                timeSinceArrivedAtWaypoint = 0f;
                CycleWaypoint();
            }

            if (timeSinceArrivedAtWaypoint >= waypointWaitTime) {
                Vector3 targetDestination = (GetCurrentWaypoint() - transform.position);
                transform.Translate(targetDestination.normalized * moveSpeed * Time.deltaTime);
            }

            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(GetCurrentWaypoint(), transform.position);
            return distanceToWaypoint < waypointDistanceTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = path.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return path.GetWaypointPosition(currentWaypointIndex);
        }
    }
}
