using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para manejar UI

public class InventoryManagerUI : MonoBehaviour
{
    public static InventoryManagerUI instance; // Singleton para acceder a la instancia
    public GameObject inventoryPanel; // Panel del inventario
    public List<Image> itemSlots = new List<Image>(); // Lista de los slots del inventario
    public Sprite defaultSprite; // Imagen por defecto (vacío)

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

    void Start()
    {
        // Inicializa los slots con la imagen por defecto
        foreach (Image slot in itemSlots)
        {
            slot.sprite = defaultSprite; // Asigna la imagen vacía por defecto a cada slot
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

        if (isInventoryOpen)
        {
            UpdateInventoryUI(InventoryManager.instance.inventory); // Actualiza la UI cuando se abre
        }

        Debug.Log("Inventario abierto: " + isInventoryOpen);
    }

    // Método para actualizar la UI cuando un ítem es añadido
    public void UpdateInventoryUI(List<Item> inventory)
    {
        // Limpiar todos los slots primero
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (i < inventory.Count)
            {
                itemSlots[i].sprite = inventory[i].icon; // Actualiza el slot con la imagen del ítem
            }
            else
            {
                itemSlots[i].sprite = defaultSprite; // Si no hay ítem, asigna el sprite vacío
            }
        }
    }
}
