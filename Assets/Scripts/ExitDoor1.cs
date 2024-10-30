using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor1 : MonoBehaviour
{
    public string spawnPointTag;  // El tag del punto de spawn 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Guardar el tag del punto de spawn
            PlayerPrefs.SetString("LastSpawnPoint", spawnPointTag);
            PlayerPrefs.SetString("SpawnPoint", spawnPointTag);

            // Cargar la escena anterior
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
