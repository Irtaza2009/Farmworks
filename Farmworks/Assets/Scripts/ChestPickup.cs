using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPickup : MonoBehaviour
{
    InventoryManager inventoryManager;

    private Animator animator;
    public Item[] itemsToPickup;

    public GameObject storagePanel;

    [SerializeField] AudioClip onOpenSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
        inventoryManager = GameManager.instance.inventoryManager;



        for (int i = 0; i < itemsToPickup.Length; i++)
        {
            PickupItem(i);
        }
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
            AudioManager.instance.Play(onOpenSound);
            animator.Play("Chest_Open");

            OpenStorage();
        }



    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.Play("Chest_Close");
            CloseStorage();
        }
    }
    
    void OpenStorage()
    {
        storagePanel.SetActive(true);
    }

    void CloseStorage()
    {
        storagePanel.SetActive(false);
    }

    
}
