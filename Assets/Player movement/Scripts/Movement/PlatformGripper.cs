using System.Collections;
using System.Collections.Generic;
using GGJ.Platform;
using UnityEngine;

public class PlatformGripper : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Platform>()) {
            transform.SetParent(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Platform>()) {
            transform.SetParent(null);
        }
    }
}
