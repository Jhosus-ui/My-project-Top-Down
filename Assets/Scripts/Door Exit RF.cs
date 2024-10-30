using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExit : MonoBehaviour
{
    public string spawnPointTag;  // El tag del punto de spawn 
    public int sceneOffset;  // Offset de la escena a cargar 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Guardar el tag 
            PlayerPrefs.SetString("LastSpawnPoint", spawnPointTag);
            PlayerPrefs.SetString("SpawnPoint", spawnPointTag);

            // Cargar la escena 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - sceneOffset);
        }
    }
}
