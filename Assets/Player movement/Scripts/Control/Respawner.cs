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
        yield return fader.FadeOut(fadeTime);
        stateMachine.Controller.enabled = false;
        gameObject.transform.position = spawnPosition;
        gameObject.transform.eulerAngles = spawnRotation;
        stateMachine.SwitchState(new HeroFreeLookState(stateMachine));
        yield return new WaitForSeconds(fadeTime);
        yield return fader.FadeIn(fadeTime);
        stateMachine.Controller.enabled = true;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Checkpoint checkpoint)) {
            //Debug.Log($"Checkpoint!");
            spawnPosition = checkpoint.SpawnPoint.position;
            spawnRotation = checkpoint.SpawnPoint.eulerAngles;
        } else if (other.gameObject.CompareTag("Respawn")) {
            //Debug.Log($"Respawn!");
            StartCoroutine(Respawn());
        }
    }
}
