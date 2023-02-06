using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using RythmFramework;
using UnityEngine;

public class PlatformActuator : MonoBehaviour
{
    public float enableDistance;
    private BeatEventListener[] listeners;
    private Transform player;

    private void Start()
    {
        listeners = GetComponentsInChildren<BeatEventListener>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void FixedUpdate()
    {
        if ((player.transform.position - transform.position).magnitude > enableDistance)
        {
            foreach (var beatEventListener in listeners)
            {
                beatEventListener.enabled = false;
            }
        }
        else
        {
            foreach (var beatEventListener in listeners)
            {
                beatEventListener.enabled = true;
            }
        }
    }
}
