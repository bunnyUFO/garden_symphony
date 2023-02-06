using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using RythmFramework;
using SplineMesh;
using SplineMeshExtensions;
using UnityEngine;

public class PlatformActuator : MonoBehaviour
{
    public float enableDistance;
    private BeatEventListener[] listeners;
    private SplineMeshTiling[] tilings;
    private SplineSmoother[] smoothers;
    private ExampleTentacle[] roots;
    private Transform player;

    private void Start()
    {
        listeners = GetComponentsInChildren<BeatEventListener>();
        tilings = GetComponentsInChildren<SplineMeshTiling>();
        smoothers = GetComponentsInChildren<SplineSmoother>();
        roots = GetComponentsInChildren<ExampleTentacle>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void FixedUpdate()
    {
        if ((player.transform.position - transform.position).magnitude > enableDistance)
        {
            foreach (var beatEventListener in listeners)
            {
                beatEventListener.ignore = true;
                beatEventListener.enabled = false;
            }
        }
        else
        {
            foreach (var beatEventListener in listeners)
            {
                beatEventListener.ignore = false;
                beatEventListener.enabled = true;
            }
        }
    }
}
