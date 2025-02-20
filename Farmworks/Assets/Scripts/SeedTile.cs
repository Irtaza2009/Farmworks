using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ToolAction/SeedTile")]
public class SeedTile : ToolAction
{
    public override bool OnApplyToTilemap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
    {
        Item selectedItem = GameManager.instance.inventoryManager.GetSelectedItem(false);
        if (tileMapReadController.cropsManager.Check(gridPosition) == false)
        {
            return false;
        }
        tileMapReadController.cropsManager.Seed(gridPosition, selectedItem.cropData);

        return true;
    }

    public override void OnItemUsed(Item usedItem, InventoryManager inventory)
    {
        inventory.Remove(usedItem);
    }
}
