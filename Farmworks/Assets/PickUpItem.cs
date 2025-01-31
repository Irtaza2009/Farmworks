using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] float ttl = 20f;
    
    InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    private void Start()
    {
        inventoryManager = GameManager.instance.inventoryManager;
    }
    private void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PickupItem(0);
            Destroy(gameObject);
        }
    }


    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result)
        {
            Debug.Log("Picked up item: " + itemsToPickup[id].name);
        } else
        {
            Debug.Log("Inventory is full");
        }
        
    }
     
}
