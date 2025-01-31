using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCuttable : ToolHit
{
    [SerializeField] GameObject pickupDrop;
    [SerializeField] int dropAmount = 1;
    [SerializeField] float spread = 0.7f;
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
}
