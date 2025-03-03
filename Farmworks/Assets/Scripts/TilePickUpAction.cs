using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/ToolAction/Harvest")]
public class TilePickUpAction : ToolAction
{
    public override bool OnApplyToTilemap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
    {
        tileMapReadController.cropsManager.PickUp(gridPosition);
        tileMapReadController.objectsManager.PickUp(gridPosition);
        return true;
    }
}
