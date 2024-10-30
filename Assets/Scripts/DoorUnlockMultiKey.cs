using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlockMultiKey : MonoBehaviour
{
    public string[] requiredKeys;  
    private bool isLocked = true;  
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
                isLocked = false;  
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
                OpenDoor();  
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
        
        doorCollider.enabled = false;

        
        if (openDoorSprite != null)
        {
            spriteRenderer.sprite = openDoorSprite;
        }

        Debug.Log("La puerta se ha abierto. Puedes pasar.");
    }
}
