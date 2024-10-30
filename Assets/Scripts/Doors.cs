using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBase : MonoBehaviour
{
    public string spawnPointTag;      // El tag del punto de spawn en la nueva escena
    public string requiredKeyID;      // Identificador de la llave requerida
    public string sceneToLoad;        // Nombre de la escena a cargar
    private bool isLocked = true;     // Estado de la puerta, inicialmente bloqueada
    private DoorSoundManager doorSoundManager;  // Referencia al script DoorSoundManager

    private void Start()
    {
        doorSoundManager = GetComponent<DoorSoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Verificar si el jugador tiene la llave
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
                    doorSoundManager.PlayDoorLockedSound(); // Reproducir el sonido de puerta bloqueada
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            if (!isLocked) // Solo permite pasar si la puerta no está bloqueada
            {
                if (doorSoundManager != null)
                {
                    doorSoundManager.spawnPointTag = spawnPointTag; // Pasar el punto de spawn al DoorSoundManager
                    doorSoundManager.sceneToLoad = sceneToLoad;      // Pasar la escena a cargar al DoorSoundManager
                    doorSoundManager.PlayDoorSoundAndChangeScene(); // Reproducir sonido y cambiar de escena
                }
                else
                {
                    Debug.LogWarning("DoorSoundManager no asignado.");
                }
            }
        }
    }
}