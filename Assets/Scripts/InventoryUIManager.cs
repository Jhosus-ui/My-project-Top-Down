using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    // Referencia al panel del inventario
    public GameObject inventoryPanel;

    // Estado actual del inventario (abierto o cerrado)
    private bool isInventoryOpen = false;

    // Update is called once per frame
    void Update()
    {
        // Detectar si el jugador presiona la tecla "Tab" o "i" para abrir/cerrar el inventario
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I))
        {
            // Cambiar el estado del inventario
            isInventoryOpen = !isInventoryOpen;

            // Activar o desactivar el panel del inventario
            inventoryPanel.SetActive(isInventoryOpen);
        }
    }
}

