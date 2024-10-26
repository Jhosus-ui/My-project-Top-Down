using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName; // Nombre del �tem (ej. "Llave")
    public Sprite icon; // Icono del �tem que se mostrar� en el inventario

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
            // A�adir el �tem al inventario
            InventoryManager.instance.AddItem(this);

            // Actualizar la UI del inventario
            InventoryUIManager.instance.UpdateInventoryUI(this);

            // Destruir el objeto del suelo despu�s de recogerlo
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
                playerController.HidePickupPrompt(); // Ocultar el mensaje al salir del �rea
            }
        }
    }
}
