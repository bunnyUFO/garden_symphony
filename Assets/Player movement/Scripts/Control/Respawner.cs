using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    private Vector3 spawnPosition;
    private Vector3 spawnRotation;


    private void Start()
    {
        spawnPosition = gameObject.transform.position;
        spawnRotation = gameObject.transform.eulerAngles;
    }

    private void Respawn()
    {
        GetComponent<HeroStateMachine>().Controller.enabled = false;
        gameObject.transform.position = spawnPosition;
        gameObject.transform.eulerAngles = spawnRotation;
        GetComponent<HeroStateMachine>().Controller.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Checkpoint checkpoint)) {
            //Debug.Log($"Checkpoint!");
            spawnPosition = checkpoint.SpawnPoint.position;
            spawnRotation = checkpoint.SpawnPoint.eulerAngles;
        } else if (other.gameObject.CompareTag("Respawn")) {
            //Debug.Log($"Respawn!");
            Respawn();
        }
    }
}
