using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LetinkDesign.Systems.Singleton;

public class TextureSetter : Singleton<TextureSetter> {

    #region Init
    protected override void Awake()
    {
    }
    #endregion

    public Texture2D[] textures;
	
    public void Set360Texture(Collection collection)
    {
        GetComponent<Renderer>().material.mainTexture = textures[(int)collection];
    }

}
