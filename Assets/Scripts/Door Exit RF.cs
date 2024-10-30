using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExit : MonoBehaviour
{
    public string spawnPointTag;  // El tag del punto de spawn en la escena anterior
    public int sceneOffset;  // Offset de la escena a cargar (por defecto, retrocede una escena)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Guardar el tag del punto de spawn para la próxima escena
            PlayerPrefs.SetString("LastSpawnPoint", spawnPointTag);
            PlayerPrefs.SetString("SpawnPoint", spawnPointTag);

            // Cargar la escena anterior o siguiente según el offset
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - sceneOffset);
        }
    }
}
