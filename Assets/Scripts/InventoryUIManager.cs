using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager instance;
    public GameObject inventoryPanel;
    private bool isInventoryOpen = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        
        inventoryPanel.SetActive(isInventoryOpen);

        if (PlayerController.instance != null)
        {
            PlayerController.instance.TogglePlayerMovement(!isInventoryOpen);
        }

        Debug.Log("Inventario abierto: " + isInventoryOpen);
    }
}
