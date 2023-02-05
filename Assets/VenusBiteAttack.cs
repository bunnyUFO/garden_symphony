using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusBiteAttack : MonoBehaviour
{
    [SerializeField] float biteDuration;
    [SerializeField] GameObject biteDetector;


    //Animation Effect
    void BiteAttack()
    {
        StartCoroutine(EnableBiteDetector());
    }

    IEnumerator EnableBiteDetector()
    {
        biteDetector.SetActive(true);
        yield return new WaitForSeconds(biteDuration);
        biteDetector.SetActive(false);
    }
}
