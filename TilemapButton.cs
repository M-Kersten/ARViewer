using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LetinkDesign.Systems.UIAnimationSystem;

public class TilemapButton : MonoBehaviour {

    public Collection collection;
    public Transform tileLocation;
    public GameObject outline;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowTile);
    }

    private void ShowTile()
    {
        button.enabled = false;
        TilemapManager.Get().ShowTile(new Vector3(tileLocation.position.x, tileLocation.position.y, tileLocation.position.z), collection, button);
    }

}
