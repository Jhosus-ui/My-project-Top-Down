using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyID; // Identificador único para la llave

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Presiona E para recoger la llave.");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.AddKey(keyID); // Añadir la llave al inventario del jugador
                Destroy(gameObject); // Destruir la llave después de recogerla
                Debug.Log("Llave recogida: " + keyID);
            }
        }
    }
}
