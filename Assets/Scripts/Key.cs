using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyID; // Identificador 
    public AudioClip pickUpSound; // Sonido 
    private AudioSource audioSource; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

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
                player.AddKey(keyID); 
                PlayPickUpSound(); 
            }
        }
    }

    private void PlayPickUpSound()
    {
        if (pickUpSound != null)
        {
            audioSource.PlayOneShot(pickUpSound); 
            Destroy(gameObject, pickUpSound.length); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}
