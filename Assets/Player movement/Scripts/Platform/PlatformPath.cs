using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Platform
{
    public class PlatformPath : MonoBehaviour
    {
        const float waypointGizmoRadius = 0.25f;

        
        private void OnDrawGizmos() {
            for (int i = 0; i < transform.childCount; i++)
            {
                Vector3 waypoint = GetWaypointPosition(i);
                int j = GetNextIndex(i);

                Gizmos.color = Color.white;
                Gizmos.DrawSphere(waypoint, waypointGizmoRadius);
                Gizmos.DrawLine(waypoint, GetWaypointPosition(j));
            }
        }

        public int GetNextIndex(int i)
        {
            if (i + 1 >= transform.childCount) {
                return 0;
            }

            return i + 1;
        }

        public Vector3 GetWaypointPosition(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
