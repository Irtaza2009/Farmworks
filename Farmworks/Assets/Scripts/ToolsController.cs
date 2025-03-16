using System;
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
    //[SerializeField] CropsManager cropsManager;
    //[SerializeField] TileData plowableTiles;
    [SerializeField] InventoryManager inventoryManager;

    [SerializeField] ToolAction onTilePickUp;

    [SerializeField] IconHighlight iconHighlight;

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
                StartCoroutine(FindObjectOfType<PlayerController>().CutAnimationCoroutine());
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
        if (inventoryManager.GetSelectedItem(false) == null) { return; }
        if (inventoryManager.GetSelectedItem(false).itemHighlight)
        {
            iconHighlight.Show(selectabel);
            iconHighlight.Set(inventoryManager.GetSelectedItem(false).icon);
        }
        if (inventoryManager.GetSelectedItem(false).itemHighlight == false)
        {
            iconHighlight.Show(false);
        }
    }

    private void SelectTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }

    private void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
        iconHighlight.cellPosition = selectedTilePosition;
    }

    private bool UseToolWorld()
    {
        Item selectedItem = inventoryManager.GetSelectedItem(false);
        if (selectedItem == null)
        {
            return false;
        }
        if (selectedItem.onAction == null)
        {
            return false;
        }
        bool complete = selectedItem.onAction.OnApply(transform.position);
        if (complete)
        {
            selectedItem.onItemUsed?.OnItemUsed(selectedItem, inventoryManager);
        }
        return complete;
    }

    private void UseToolGrid()
    {
        if (!selectabel)
        { return; }

        TileBase tileBase = tileMapReadController.GetTileBase(selectedTilePosition);


        Item selectedItem = inventoryManager.GetSelectedItem(false);
        if (selectedItem == null)
        {
            PickUpTile();
            return;
        }
        if (selectedItem.onTilemapAction == null)
        {
            PickUpTile();
            return;
        }
        bool complete = selectedItem.onTilemapAction.OnApplyToTilemap(selectedTilePosition, tileMapReadController);

        if (complete)
        {
            selectedItem.onItemUsed?.OnItemUsed(selectedItem, inventoryManager);
        }


        /*
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
        */

    }

    private void PickUpTile()
    {
        if (onTilePickUp == null)
        {
            return;
        }
       onTilePickUp.OnApplyToTilemap(selectedTilePosition, tileMapReadController);
    }
}
