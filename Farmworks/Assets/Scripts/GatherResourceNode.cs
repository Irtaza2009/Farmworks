using System.Collections.Generic;
using UnityEngine;

public enum ResourceNodeType
{
    Undefined,
    Tree,
    Stone,
    Plant
}



[CreateAssetMenu(menuName = "Data/ToolAction/GatherResourceNode")]
public class GatherResourceNode : ToolAction
{
    [SerializeField] List<ResourceNodeType> canHitNodesOfType;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    public override bool OnApply(Vector2 worldPoint)
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);
        foreach (Collider2D collider in colliders)
        {
            ToolHit hit = collider.GetComponent<ToolHit>();
            if (hit != null)
            {
                if (hit.canBeHit(canHitNodesOfType) == true)
                {
                    hit.Hit();
                    return true;
                }
            }
        }
        return false;
    }
}
