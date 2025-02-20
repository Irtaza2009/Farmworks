using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAction : ScriptableObject
{
    public virtual bool OnApply(Vector2 worldPoint)
    {
        return true;
    }

    public virtual bool OnApplyToTilemap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
    {
        return true;
    }

    public virtual void OnItemUsed(Item usedItem, InventoryManager inventory)
    {
        
    }
}
