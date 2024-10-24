using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door5 : MonoBehaviour
{
    public string spawnPointTag;  // El tag del punto de spawn en la nueva escena

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Guardar el tag del punto de spawn para la próxima escena
            PlayerPrefs.SetString("LastSpawnPoint", spawnPointTag);
            PlayerPrefs.SetString("SpawnPoint", spawnPointTag);

            // Cargar la siguiente escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
        }
    }
}
