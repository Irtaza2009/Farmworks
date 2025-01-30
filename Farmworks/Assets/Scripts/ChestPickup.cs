using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPickup : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PickupItem(0);
        }
    }

    
}
