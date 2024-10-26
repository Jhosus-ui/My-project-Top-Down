using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName; // Nombre del ítem (ej. "Llave")
    public Sprite icon; // Icono del ítem que se mostrará en el inventario

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ShowPickupPrompt(itemName); // Mostrar un mensaje al recoger
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            // Añadir el ítem al inventario
            InventoryManager.instance.AddItem(this);

            // Actualizar la UI del inventario
            InventoryUIManager.instance.UpdateInventoryUI(this);

            // Destruir el objeto del suelo después de recogerlo
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.HidePickupPrompt(); // Ocultar el mensaje al salir del área
            }
        }
    }
}
