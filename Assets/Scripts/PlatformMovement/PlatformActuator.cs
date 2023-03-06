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
    private InterpolateFromTransforms[] interpolates;
    private MeshBender[] benders;
    private ExampleTentacle[] roots;
    private Transform player;

    private void Start()
    {
        listeners = GetComponentsInChildren<BeatEventListener>();
        tilings = GetComponentsInChildren<SplineMeshTiling>();
        smoothers = GetComponentsInChildren<SplineSmoother>();
        roots = GetComponentsInChildren<ExampleTentacle>();
        interpolates = GetComponentsInChildren<InterpolateFromTransforms>();
        benders = GetComponentsInChildren<MeshBender>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        if ((player.transform.position - transform.position).magnitude > enableDistance)
        {
            foreach (var beatEventListener in listeners)
            {
                beatEventListener.ignore = true;
                beatEventListener.enabled = false;
            }

            enableAll(tilings, false);
            enableAll(smoothers, false);
            enableAll(roots, false);
            enableAll(interpolates, false);
            enableAll(benders, false);
        }
        else
        {
            foreach (var beatEventListener in listeners)
            {
                beatEventListener.ignore = false;
                beatEventListener.enabled = true;
            }
            
            enableAll(tilings, true);
            enableAll(smoothers, true);
            enableAll(roots, true);
            enableAll(interpolates, true);
            enableAll(benders, true);
        }
    }

    private void enableAll(MonoBehaviour[] scripts, bool enable)
    {
        foreach (var script in scripts)
        {
            script.enabled = enable;
        }
    }
}
