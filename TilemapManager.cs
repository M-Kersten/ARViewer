using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LetinkDesign.Systems.Singleton;

/// <summary>
/// Enum to store a link to one of the Fuse tile collections
/// </summary>
public enum Collection
{
    FuseOne,
    FuseTwo,
    FuseThree
}

/// <summary>
/// A class to represent a Fuse tile collection
/// </summary>
[System.Serializable]
public class CollectionInfo
{
    public string name;
    public string title;
    public string text;
    public Collection collection;
}

public class TilemapManager : Singleton<TilemapManager> {
    
    public GameObject FuseTilePrefab;
    public Transform infoTilesTransform;
    public CollectionInfo[] collections;
    
    #region Init
    protected override void Awake()
    {

    }
    #endregion
    
    /// <summary>
    /// Spawn a tile next to the button
    /// </summary>
    /// <param name="t">location to instantiate tile at</param>
    /// <param name="c">collection data to show on tile</param>
    /// <param name="buttonToLink"></param>
    public void ShowTile(Vector3 t, Collection c, Button buttonToLink)
    {
        int element = GetElementByCollection(c);
        InfoTileFunctionality setInfo = FuseTilePrefab.GetComponent<InfoTileFunctionality>();
        setInfo.collection = c;
        setInfo.title.text = collections[element].title;
        setInfo.text.text = collections[element].text;
        ClearTiles();
        GameObject tile = Instantiate(FuseTilePrefab, t, Quaternion.identity, infoTilesTransform);
        tile.GetComponent<InfoTileFunctionality>().buttonLink = buttonToLink;
        tile.GetComponent<FadeObject>().FadeIn();
    }

    /// <summary>
    /// look for int that matches an element in collections array
    /// </summary>
    /// <param name="c"></param>
    /// <returns>index of collection you are looking for</returns>
    private int GetElementByCollection(Collection c)
    {
        int result = -1;
        for (int i = 0; i < collections.Length; i++)
        {
            if (collections[i].collection == c)
            {
                result = i;
            }
        }
        return result;
    }

    /// <summary>
    /// closes all active tiles
    /// </summary>
    private void ClearTiles()
    {
        foreach (Transform child in infoTilesTransform.transform)
        {
            child.GetComponent<InfoTileFunctionality>().Close();
        }
    }
}