using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [field: SerializeField] public Transform SpawnPoint { get; private set; }

    [SerializeField] private ParticleSystem activeVisual;

    public void EnableVisual(bool isEnabled)
    {
        if (activeVisual == null) return;

        if (isEnabled) {
            activeVisual.Play();
        } else {
            activeVisual.Stop();
        }
    }
}
