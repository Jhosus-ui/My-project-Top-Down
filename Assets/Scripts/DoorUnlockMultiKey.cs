using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlockMultiKey : MonoBehaviour
{
    public string[] requiredKeys;
    private Collider2D doorCollider;
    public Sprite openDoorSprite;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null && HasAllKeys(player))
            {
                OpenDoor();
                Debug.Log("Puerta desbloqueada. Puedes pasar.");
            }
            else
            {
                Debug.Log("Necesitas todas las llaves para abrir esta puerta.");
            }
        }
    }

    private bool HasAllKeys(PlayerController player)
    {
        foreach (string keyID in requiredKeys)
        {
            if (!player.HasKey(keyID))
            {
                return false;
            }
        }
        return true;
    }

    private void OpenDoor()
    {
        // Desactivar el collider de la puerta para permitir el paso
        doorCollider.enabled = false;

        // Cambiar el sprite de la puerta a la versión abierta
        if (openDoorSprite != null)
        {
            spriteRenderer.sprite = openDoorSprite;
        }
    }
}
