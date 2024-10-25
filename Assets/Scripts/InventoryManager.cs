using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; // Singleton para acceder a la instancia
    public List<Item> inventory = new List<Item>(); // Lista de ítems en el inventario

    private void Awake()
    {
        // Comprobar si ya existe una instancia del InventoryManager
        if (instance == null)
        {
            instance = this; // Establecer esta instancia
            DontDestroyOnLoad(gameObject); // No destruir al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Destruir duplicados
        }
    }

    // Método para añadir un ítem al inventario
    public void AddItem(Item item)
    {
        inventory.Add(item);
        Debug.Log("Ítem añadido: " + item.itemName);
    }
}
