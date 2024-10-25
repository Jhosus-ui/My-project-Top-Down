using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName; // Nombre del item (por ejemplo, "Llave")
    public Sprite icon; // Icono del item
    public GameObject itemPrefab; // Prefab del item para instanciar en el inventario

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobar si el jugador entra en el �rea de la llave
        if (other.CompareTag("Player"))
        {
            // Escuchar la tecla E para recoger la llave
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ShowPickupPrompt(itemName); // Muestra un mensaje al recoger
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Comprobar si el jugador est� dentro del �rea de la llave
        if (other.CompareTag("Player"))
        {
            // Comprobar si el jugador presiona E
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Llamar al m�todo para a�adir el item al inventario
                InventoryManager.instance.AddItem(this);

                // Destruir el objeto de la llave despu�s de recogerlo
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) // Cambiado de collision a other
    {
        // Comprobar si el jugador sale del �rea de la llave
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.HidePickupPrompt(); // Ocultar el mensaje al salir
            }
        }
    }
}
