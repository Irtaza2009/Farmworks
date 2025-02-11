using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]

public class Item : ScriptableObject
{


    [Header("Only Gameplay")]
    public TileBase tile;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;
    
    public CropData cropData;

}

public enum ItemType {
    BuildingBlock,
    Tool,
    Seed,
    Food,
    // Water, fertilser:
    Resource
}

public enum ActionType {
    Place,
    Use,
    Eat,
    // Water, fertilser:
    Apply
}