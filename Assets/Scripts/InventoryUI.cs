using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para manejar UI

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance; // Singleton para acceder a la instancia de InventoryUI

    public List<Image> itemSlots = new List<Image>(); // Lista de los slots del inventario
    public Sprite defaultSprite; // Imagen por defecto (vac�o)

    private void Awake()
    {
        // Asegurarse de que solo haya una instancia de InventoryUI
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Destruir duplicados si ya existe una instancia
        }
    }

    void Start()
    {
        // Inicializa los slots con la imagen por defecto
        foreach (Image slot in itemSlots)
        {
            slot.sprite = defaultSprite; // Asigna la imagen vac�a por defecto a cada slot
        }
    }

    // M�todo para actualizar la UI cuando un �tem es a�adido
    public void UpdateInventoryUI(List<Item> inventory)
    {
        // Limpiar todos los slots primero
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (i < inventory.Count)
            {
                itemSlots[i].sprite = inventory[i].icon; // Usar icon
            }
            else
            {
                itemSlots[i].sprite = defaultSprite; // Si no hay �tem, asigna el sprite vac�o
            }
        }
    }
}
