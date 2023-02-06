using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBouncer : MonoBehaviour
{
    [SerializeField] bool bounceOnBeat;
    [SerializeField] float launchStrength;
    List<HeroStateMachine> heroesOnMushroom = new List<HeroStateMachine>();


    public void Launch()
    {
        if (! bounceOnBeat) return;

        foreach (HeroStateMachine hero in heroesOnMushroom) {         
            BounceHero(hero);
        }
    }

    private void BounceHero(HeroStateMachine hero)
    {
        Vector3 launchForce = transform.forward * launchStrength;
        hero.SwitchState(new HeroJumpingState(hero));
        hero.ForceReceiver.AddForce(launchForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            if (bounceOnBeat) {
                heroesOnMushroom.Add(other.GetComponent<HeroStateMachine>());
            } else {
                BounceHero(other.GetComponent<HeroStateMachine>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && bounceOnBeat) {
            heroesOnMushroom.Remove(other.GetComponent<HeroStateMachine>());
        }
    }
}
