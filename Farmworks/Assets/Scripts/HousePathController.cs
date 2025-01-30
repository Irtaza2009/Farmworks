using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class HousePathController : MonoBehaviour
{
    public GameObject tilemap;
     

    private void Start()
    {
        
            
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tilemap.SetActive(false);
            Debug.Log("Player entered house");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tilemap.SetActive(true);
            Debug.Log("Player exited house");
        }
    }

    
}
