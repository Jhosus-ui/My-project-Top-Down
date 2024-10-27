using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public string requiredKeyID;  // Identificador de la llave requerida
    private bool isLocked = true;  // Estado inicial de la puerta (bloqueada)
    private Collider2D doorCollider; // Referencia al collider de la puerta
    public Sprite openDoorSprite;  // Sprite para la puerta abierta

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        doorCollider = GetComponent<Collider2D>(); // Obtén el collider de la puerta
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtén el SpriteRenderer para cambiar el sprite
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null && player.HasKey(requiredKeyID))
            {
                isLocked = false; // Desbloquear la puerta si el jugador tiene la llave
                Debug.Log("Puerta desbloqueada. Presiona E para abrir.");
            }
            else
            {
                Debug.Log("Necesitas la llave correcta para abrir esta puerta.");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            if (!isLocked)
            {
                OpenDoor();  // Desbloquear la puerta al presionar E si tienes la llave
            }
        }
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
