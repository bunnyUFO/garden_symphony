using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBouncer : MonoBehaviour
{
    [SerializeField] float launchStrength;
    List<HeroStateMachine> heroesOnMushroom = new List<HeroStateMachine>();


    public void Launch()
    {
        foreach (HeroStateMachine hero in heroesOnMushroom) {
            Vector3 launchForce = Vector3.up * launchStrength;
            
            // hero.ForceReceiver.Jump(hero.JumpForce);
            hero.SwitchState(new HeroJumpingState(hero));
            hero.ForceReceiver.AddForce(launchForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            heroesOnMushroom.Add(other.GetComponent<HeroStateMachine>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            heroesOnMushroom.Remove(other.GetComponent<HeroStateMachine>());
        }
    }
}
