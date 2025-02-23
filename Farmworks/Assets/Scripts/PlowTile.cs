using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/ToolAction/PlowTile")]
public class PlowTile : ToolAction
{
    [SerializeField] List<TileBase> canPlow;

    public override bool OnApplyToTilemap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
    {
        TileBase tileToPlow = tileMapReadController.GetTileBase(gridPosition);

        if (!canPlow.Contains(tileToPlow))
        {
            return false;
        }
        Debug.Log("Plowing tile at " + gridPosition);
        tileMapReadController.cropsManager.Plow(gridPosition);
        return true;
    }

}
