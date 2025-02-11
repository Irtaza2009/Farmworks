using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;

    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] float maxDistance = 1.5f;
    [SerializeField] CropsManager cropsManager;
    [SerializeField] TileData plowableTiles;
    [SerializeField] InventoryManager inventoryManager;

    Vector3Int selectedTilePosition;
    bool selectabel;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SelectTile();
        CanSelectCheck();
        Marker();

        if (Input.GetMouseButtonDown(0))
        {
            if (UseToolWorld() == true)
            {
                return;
            }
            UseToolGrid();
            
        }
    }

    void CanSelectCheck()
    {
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectabel = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
        markerManager.Show(selectabel);
    }

    private void SelectTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }

    private void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
    }

    private bool UseToolWorld()
    {
        Item selectedItem = inventoryManager.GetSelectedItem(false);
        if (selectedItem == null)
        {
            return false;
        }
        else if (selectedItem.type == ItemType.Tool && selectedItem.actionType == ActionType.Use && selectedItem.name == "Axe")
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, sizeOfInteractableArea);
            foreach (Collider2D collider in colliders)
            {
                ToolHit hit = collider.GetComponent<ToolHit>();
                if (hit != null)
                {
                    hit.Hit();
                    return true;
                }
            }
            
        }
        return false;
    }

    private void UseToolGrid()
    {
        if (!selectabel)
        { return; }

        TileBase tileBase = tileMapReadController.GetTileBase(selectedTilePosition);
        TileData tileData = tileMapReadController.GetTileData(tileBase);

        Item selectedItem = inventoryManager.GetSelectedItem(false);
        if (selectedItem == null)
        {
            return;
        }

        if (selectedItem.type == ItemType.Tool && selectedItem.actionType == ActionType.Use && selectedItem.name == "Pickaxe")
        {
            if (tileData == plowableTiles)
            {
                cropsManager.Plow(selectedTilePosition);
            }
        }
        else if (selectedItem.type == ItemType.Seed && selectedItem.actionType == ActionType.Place)
        {
            
            if (cropsManager.Check(selectedTilePosition))
            {
                CropData selectedCrop = selectedItem.cropData;
                cropsManager.Seed(selectedTilePosition, selectedCrop);
            }

        }   
        
    }
}
