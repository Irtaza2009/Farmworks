using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/ToolAction/PlowTile")]
public class PlowTile : ToolAction
{
    [SerializeField] List<TileBase> canPlow;

    [SerializeField] AudioClip plowSound;

    public override bool OnApplyToTilemap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
    {
        TileBase tileToPlow = tileMapReadController.GetTileBase(gridPosition);

        if (!canPlow.Contains(tileToPlow))
        {
            return false;
        }
        Debug.Log("Plowing tile at " + gridPosition);

        // Get the player object
        GameObject player = GameObject.FindGameObjectWithTag("Player"); 
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.StartCoroutine(playerController.PlowAnimationCoroutine());
            }
        }

        tileMapReadController.cropsManager.Plow(gridPosition);
        AudioManager.instance.Play(plowSound);
        return true;
    }

}
