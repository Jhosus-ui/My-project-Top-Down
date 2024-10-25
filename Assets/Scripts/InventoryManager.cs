using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; // Singleton para acceder a la instancia
    public List<Item> inventory = new List<Item>(); // Lista de �tems en el inventario

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

    // M�todo para a�adir un �tem al inventario
    public void AddItem(Item item)
    {
        inventory.Add(item);
        Debug.Log("�tem a�adido: " + item.itemName);
    }
}
