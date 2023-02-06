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
                beatEventListener.enabled = false;
            }
            foreach (var tiling in tilings)
            {
                tiling.enabled = false;
            }
            foreach (var smoother in smoothers)
            {
                smoother.enabled = false;
            }
            foreach (var root in roots)
            {
                root.enabled = false;
            }
        }
        else
        {
            foreach (var beatEventListener in listeners)
            {
                beatEventListener.enabled = true;
            }
            foreach (var tiling in tilings)
            {
                tiling.enabled = true;
            }
            foreach (var smoother in smoothers)
            {
                smoother.enabled = true;
            }
            foreach (var root in roots)
            {
                root.enabled = true;
            }
        }
    }
}
