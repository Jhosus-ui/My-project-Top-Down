using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public string requiredKeyID;  
    private bool isLocked = true;  
    private Collider2D doorCollider; 
    public Sprite openDoorSprite; 

    private SpriteRenderer spriteRenderer;
    public AudioClip doorOpenSound;    
    public AudioClip doorLockedSound;  
    private AudioSource audioSource;   


    private void Start()
    {
        doorCollider = GetComponent<Collider2D>(); 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            
            audioSource = gameObject.AddComponent<AudioSource>();
        }
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

        // Cambiar el sprite 
        if (openDoorSprite != null)
        {
            spriteRenderer.sprite = openDoorSprite;
        }

        Debug.Log("La puerta se ha abierto. Puedes pasar.");
    }


        // Reproducir el sonido de abrir la puerta
    public void PlayDoorOpenSound()
    {
            if (audioSource != null && doorOpenSound != null)
            {
                audioSource.clip = doorOpenSound;
                audioSource.Play(); 
            }
    }

        // Reproducir el sonido de puerta bloqueada
    public void PlayDoorLockedSound()
    { 
            if (audioSource != null && doorLockedSound != null)
            {
                audioSource.clip = doorLockedSound;
                audioSource.Play(); 
            }
    }
}
