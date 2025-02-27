using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] float ttl = 20f;
    
    InventoryManager inventoryManager;
    public Item item;
    public int count;

    private void Start()
    {
        inventoryManager = GameManager.instance.inventoryManager;
    }
    private void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl <= 0)
        {
           // Destroy(gameObject);
        }
        
    }

    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = item.icon;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PickupItem();
            Destroy(gameObject);
        }
    }


    public void PickupItem()
    {
        for (int i = 0; i < count; i++)
        {

            bool result = inventoryManager.AddItem(item);
            if (result)
            {
                Debug.Log("Picked up item: " + item.name);
            }
            else
            {
                Debug.Log("Inventory is full");
            }
        }
        
    }
     
}
