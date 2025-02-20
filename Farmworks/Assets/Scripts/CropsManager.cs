using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crops
{
    public int growthStage;
    public CropData cropData;
    public Vector3Int position;
    public Coroutine growthCoroutine;
    


    public bool Complete
    {
        get
        {
            if (cropData == null) { return false; }
            return growthStage == cropData.maxGrowthStage;
        }
    }

    internal void Harvested()
    {
        growthStage = 0;
        cropData = null;
        position = Vector3Int.zero;

        if (growthCoroutine != null)
        {
            // Stop growth coroutine to prevent errors
            GameManager.instance.StopCoroutine(growthCoroutine);
            growthCoroutine = null;
        }
    }
}

public class CropsManager : MonoBehaviour
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase[] seeded;
    [SerializeField] Tilemap targetTilemap;

    Dictionary<Vector2Int, Crops> crops;

    private void Start()
    {
        crops = new Dictionary<Vector2Int, Crops>();
    }

    public bool Check(Vector3Int position)
    {
        return crops.ContainsKey((Vector2Int)position);
    }

    public void Plow(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))
        {
            return;
        }

        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position, CropData cropData)
    {
        if (!crops.ContainsKey((Vector2Int)position))
            return;

        Crops crop = new Crops
        {
            growthStage = 0,
            cropData = cropData,
            position = position
        };

        crops[(Vector2Int)position] = crop;

        targetTilemap.SetTile(position, crop.cropData.growthStages[0]);
        crop.growthCoroutine = StartCoroutine(GrowCrop(crop));
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        Crops crop = new Crops();
        crops.Add((Vector2Int)position, crop);

        targetTilemap.SetTile(position, plowed);
    }
    
    private IEnumerator GrowCrop(Crops crop)
    {
        while (crop.growthStage < crop.cropData.maxGrowthStage)
        {
            yield return new WaitForSeconds(crop.cropData.timePerStage);
            crop.growthStage++;
            targetTilemap.SetTile(crop.position, crop.cropData.growthStages[crop.growthStage]);
        }
    }

    public void PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int)gridPosition;

        if (!crops.ContainsKey(position)) return;

        Crops cropTile = crops[position];

        if (cropTile.Complete)
        {
            ItemSpawnManager.instance.SpawnItem(targetTilemap.CellToWorld(gridPosition), cropTile.cropData.yield, 1);
            
            // Remove crop from the dictionary and reset tile to plowed
            crops.Remove(position);
            targetTilemap.SetTile(gridPosition, null);

            Debug.Log("Harvested");
        }
    }
}
