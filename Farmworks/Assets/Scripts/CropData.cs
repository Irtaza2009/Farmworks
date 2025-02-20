using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewCrop", menuName = "Farming/Crop")]
public class CropData : ScriptableObject
{
    public string cropName;
    public TileBase[] growthStages; // Different tiles for each stage
    public float timePerStage; // Time to transition between each stage
    public int maxGrowthStage => growthStages.Length - 1; // Number of growth stages

    public int yieldAmount; // Amount of crops to yield when harvested
    public Item yield; // Sprite of the crop to yield
}
