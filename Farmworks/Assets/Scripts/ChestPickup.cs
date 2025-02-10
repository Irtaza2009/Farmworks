using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPickup : MonoBehaviour
{
    public InventoryManager inventoryManager;

    private Animator animator;
    public Item[] itemsToPickup;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result)
        {
            Debug.Log("Picked up item: " + itemsToPickup[id].name);
        }
        else
        {
            Debug.Log("Inventory is full");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.Play("Chest_Open");
            PickupItem(0);
            PickupItem(1);
            PickupItem(2);
            PickupItem(3);
        }

        

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.Play("Chest_Close");
        }
    }

    
}
