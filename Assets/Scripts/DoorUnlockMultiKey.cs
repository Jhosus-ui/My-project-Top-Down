using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlockMultiKey : MonoBehaviour
{
    public string[] requiredKeys;  // Identificadores de las 3 llaves necesarias
    private bool isLocked = true;  // Estado inicial de la puerta (bloqueada)
    private Collider2D doorCollider;  // Referencia al collider de la puerta
    public Sprite openDoorSprite;  // Sprite para la puerta abierta

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        doorCollider = GetComponent<Collider2D>();  // Obtén el collider de la puerta
        spriteRenderer = GetComponent<SpriteRenderer>();  // Obtén el SpriteRenderer para cambiar el sprite
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            // Comprobar si el jugador tiene todas las llaves necesarias
            if (player != null && HasAllKeys(player))
            {
                isLocked = false;  // Desbloquear la puerta si el jugador tiene todas las llaves
                Debug.Log("Puerta desbloqueada. Presiona E para abrir.");
            }
            else
            {
                Debug.Log("Necesitas todas las llaves para abrir esta puerta.");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            if (!isLocked)
            {
                OpenDoor();  // Desbloquear la puerta al presionar E si tienes todas las llaves
            }
        }
    }

    // Comprobar si el jugador tiene todas las llaves necesarias
    private bool HasAllKeys(PlayerController player)
    {
        foreach (string keyID in requiredKeys)
        {
            if (!player.HasKey(keyID))  // Si falta una llave, regresa false
            {
                return false;
            }
        }
        return true;  // Tiene todas las llaves necesarias
    }

    private void OpenDoor()
    {
        // Desactivar el collider para permitir el paso
        doorCollider.enabled = false;

        // Cambiar el sprite de la puerta a la versión abierta
        if (openDoorSprite != null)
        {
            spriteRenderer.sprite = openDoorSprite;
        }

        Debug.Log("La puerta se ha abierto. Puedes pasar.");
    }
}
