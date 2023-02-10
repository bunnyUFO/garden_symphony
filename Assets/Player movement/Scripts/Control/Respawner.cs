using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] float fadeTime = 1f;

    private Vector3 spawnPosition;
    private Vector3 spawnRotation;

    Fader fader;
    HeroStateMachine stateMachine;
    Checkpoint currentCheckpoint;


    private void Awake()
    {
        fader = GameObject.FindObjectOfType<Fader>();
        stateMachine = GetComponent<HeroStateMachine>();
    }

    private void Start()
    {
        spawnPosition = gameObject.transform.position;
        spawnRotation = gameObject.transform.eulerAngles;
    }

    IEnumerator Respawn()
    {
        stateMachine.SwitchState(new HeroRespawningState(stateMachine));
        yield return new WaitForSeconds(fadeTime);
        yield return fader.FadeOut(fadeTime);
        stateMachine.InputReader.enabled = false;
        gameObject.transform.position = spawnPosition;
        gameObject.transform.eulerAngles = spawnRotation;
        stateMachine.SwitchState(new HeroFreeLookState(stateMachine));
        yield return new WaitForSeconds(fadeTime);
        yield return fader.FadeIn(fadeTime);
        stateMachine.InputReader.enabled = true;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Checkpoint checkpoint)) {
            if (currentCheckpoint == checkpoint) return;
            
            if (currentCheckpoint != null) {
                currentCheckpoint.EnableVisual(false);
            }

            checkpoint.EnableVisual(true);
            currentCheckpoint = checkpoint;

            spawnPosition = checkpoint.SpawnPoint.position;
            spawnRotation = checkpoint.SpawnPoint.eulerAngles;
        } else if (other.gameObject.CompareTag("Respawn")) {
            //Debug.Log($"Respawn!");
            StartCoroutine(Respawn());
        }
    }
}
