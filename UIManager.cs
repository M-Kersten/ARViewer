using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LetinkDesign.Systems.Singleton;
using LetinkDesign.Systems.FaderSystem;

public class UIManager : Singleton<UIManager>
{
    public GameObject[] ARMenus;
    public GameObject[] nonARMenus;
    public GameObject AR;

    #region Init
    protected override void Awake()
    {

    }
    #endregion

    public void SetAR(bool ar)
    {
        Fader.AlphaFade(0, 1, 0.5f);
        StartCoroutine(WaitForFadeEnd(ar));
    }

    public void SetAR(bool ar, Collection collection)
    {
        Fader.AlphaFade(0, 1, 0.5f);
        StartCoroutine(WaitForFadeEnd(ar, collection));
    }

    private IEnumerator WaitForFadeEnd(bool ar)
    {
        while (Fader.IsFading)
            yield return null;
        Switch(ar);
        Fader.AlphaFade(1, 0, 0.5f);
    }

    private IEnumerator WaitForFadeEnd(bool ar, Collection collection)
    {
        while (Fader.IsFading)
            yield return null;
        Switch(ar);
        TextureSetter.Get().Set360Texture(collection);
        Fader.AlphaFade(1, 0, 0.5f);
    }    

    public void Switch(bool ar)
    {
        for (int i = 0; i < ARMenus.Length; i++)
        {
            ARMenus[i].SetActive(ar);
        }
        for (int i = 0; i < nonARMenus.Length; i++)
        {
            nonARMenus[i].SetActive(!ar);
        }
        AR.SetActive(ar);
    }
}
