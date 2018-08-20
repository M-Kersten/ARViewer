using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LetinkDesign.Systems.FaderSystem;

public class InfoTileFunctionality : MonoBehaviour {

    public Collection collection;
    public Text title;
    public Text text;
    [HideInInspector]
    public Button buttonLink;
    
    public void ToAR()
    {
        UIManager.Get().SetAR(true, collection);
        Close();
    }

    private IEnumerator WaitForFadeEnd()
    {
        while (Fader.IsFading)
            yield return null;
        
        Fader.AlphaFade(1, 0, 0.5f);
    }

    public void Close()
    {
        buttonLink.enabled = true;
        GetComponent<FadeObject>().FadeOutAndDestroy(gameObject);
        StartCoroutine(WaitForDestruction());
    }

    private IEnumerator WaitForDestruction()
    {
        float seconds = GetComponent<FadeObject>().fadeOutSeconds;
        Debug.Log("destroying: " + gameObject.name + " in " + seconds + " seconds");
        yield return new WaitForSeconds(seconds);        
        Destroy(gameObject);
    }

}
