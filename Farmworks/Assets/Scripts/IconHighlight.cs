using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IconHighlight : MonoBehaviour
{
    public Vector3Int cellPosition;
    Vector3 targetPosition;
    [SerializeField] Tilemap targetTilemap;

    SpriteRenderer spriteRenderer;

    private void Update()
    {
        targetPosition = targetTilemap.CellToWorld(cellPosition);
        transform.position = targetPosition + targetTilemap.cellSize / 2;
    }
    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }

    internal void Set(Sprite icon)
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = icon;
    }
}
