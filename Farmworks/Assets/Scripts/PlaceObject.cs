using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ToolAction/Place Object")]
public class PlaceObject : ToolAction
{
    public override bool OnApplyToTilemap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
    {
        Item selectedItem = GameManager.instance.inventoryManager.GetSelectedItem(false);
        tileMapReadController.objectsManager.Place(selectedItem, gridPosition);
        return true;
    }
}
