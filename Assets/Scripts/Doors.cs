using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBase : MonoBehaviour
{
    public string spawnPointTag;     
    public string requiredKeyID;      
    public string sceneToLoad;        
    private bool isLocked = true;    
    private DoorSoundManager doorSoundManager;  

    private void Start()
    {
        doorSoundManager = GetComponent<DoorSoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null && player.HasKey(requiredKeyID))
            {
                isLocked = false; // Desbloquear la puerta si el jugador tiene la llave
                Debug.Log("Puerta desbloqueada. Presiona E para entrar.");
            }
            else
            {
                Debug.Log("Necesitas la llave correcta para abrir esta puerta.");
                if (doorSoundManager != null)
                {
                    doorSoundManager.PlayDoorLockedSound(); 
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            if (!isLocked) 
            {
                if (doorSoundManager != null)
                {
                    doorSoundManager.spawnPointTag = spawnPointTag; 
                    doorSoundManager.sceneToLoad = sceneToLoad;      
                    doorSoundManager.PlayDoorSoundAndChangeScene(); 
                }
                else
                {
                    Debug.LogWarning("DoorSoundManager no asignado.");
                }
            }
        }
    }
}