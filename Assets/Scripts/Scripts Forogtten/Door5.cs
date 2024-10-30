using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door5 : MonoBehaviour
{
    public string spawnPointTag;  // El tag del punto de spawn en la nueva escena
    public string requiredKeyID;   // Identificador de la llave requerida
    private bool isLocked = true;   // Estado de la puerta, inicialmente bloqueada

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
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            if (!isLocked) // Solo permite pasar si la puerta no está bloqueada
            {
                // Guardar el tag del punto de spawn para la próxima escena
                PlayerPrefs.SetString("LastSpawnPoint", spawnPointTag);
                PlayerPrefs.SetString("SpawnPoint", spawnPointTag);

                // Cargar la siguiente escena
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
            }
    }   }
}
