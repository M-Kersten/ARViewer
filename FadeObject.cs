using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class with fade functionality
/// </summary>
public class FadeObject : MonoBehaviour {

    public float fadeInSeconds;
    public float fadeOutSeconds;
    public bool autoFadeIn;

    [SerializeField]
    private CanvasGroup canvasGroup;
    private GameObject localGO;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (autoFadeIn)
        {
            FadeIn();
        }
    }
    
    public void FadeIn(float seconds)
    {
        Fade(0, 1, seconds);
    }

    public void FadeOut(float seconds)
    {
        Fade(1, 0, seconds);        
    }

    public void FadeIn()
    {
        Fade(0, 1, fadeInSeconds);
    }

    public void FadeOut()
    {
        Fade(1, 0, fadeOutSeconds);
    }

    public void FadeOutAndDestroy(GameObject GO)
    {
        localGO = GO;
        Fade(1, 0, fadeOutSeconds);
    }

    public void Fade(float from, float to, float time)
    {
        Debug.Log(Time.realtimeSinceStartup + "\t\t Start fade " + name + " from " + from + " to " + to + " in " + time + " s", this);
        //set this gameobject as active
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        //but if the parent is disabled because this isn't needed - don't fade
        if (gameObject.activeInHierarchy && canvasGroup != null)
        {
            StopAllCoroutines();
            if (Mathf.Approximately(0f, time))
            {                
                SetAlphaDirectly(to);
            }
            else
            {                
                StartCoroutine(FadeRoutine(from, to, time));                
            }
        }
        else if (!gameObject.activeInHierarchy)
            Debug.LogWarning("UI Fade skipped because it is not active in hierarchy", this);
        else if (canvasGroup == null)
            Debug.LogError("Can't fade UI Fader because no canvasgroup is assigned", this);
    }

    void SetAlphaDirectly(float a)
    {
        canvasGroup.alpha = a;
    }

    private IEnumerator FadeRoutine(float from, float to, float time)
    {
        float t = 0f;

        canvasGroup.alpha = from;

        while (t <= time)
        {
            canvasGroup.alpha = from + (to - from) * (t / time);

            t += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
        if (canvasGroup.alpha < .05f && to == 0)
        {
            gameObject.SetActive(false);
            if (localGO != null)
            {
                Destroy(localGO);
            }
        }
    }
}
