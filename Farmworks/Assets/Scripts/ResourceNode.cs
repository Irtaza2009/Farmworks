using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ResourceNode : ToolHit
{
    [SerializeField] GameObject pickupDrop;
    [SerializeField] int dropAmount = 1;
    [SerializeField] float spread = 0.2f;
    [SerializeField] ResourceNodeType nodeType;
    public override void Hit()
    {
        while (dropAmount > 0)
        {
            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;
            Instantiate(pickupDrop, position, Quaternion.identity);
            dropAmount--;
        }
        Destroy(gameObject);
    }

    public override bool canBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }
}
