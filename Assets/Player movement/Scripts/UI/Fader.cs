using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    CanvasGroup canvasGroup;


    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator FadeOut(float time)
    {
        while (canvasGroup.alpha < 1f) {
            canvasGroup.alpha += Time.deltaTime / time;
            yield return null;
        }
    }

    public IEnumerator FadeIn(float time)
    {
        while (canvasGroup.alpha > 0f) {
            canvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }
    }

    IEnumerator FadeOutIn()
    {
        yield return FadeOut(3f);
        //Do something
        yield return FadeIn(3f);
    }
}
