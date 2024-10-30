using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSoundManager : MonoBehaviour
{
    public AudioClip doorSound;       // Sonido de la puerta
    public string sceneToLoad;        // Nombre de la escena a cargar
    public string spawnPointTag;      // Tag del punto de spawn en la nueva escena
    private AudioSource audioSource;  // Referencia al componente AudioSource

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Si no hay AudioSource, agregarlo automáticamente
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = doorSound;  // Asignar el sonido de la puerta
    }

    public void PlayDoorSoundAndChangeScene()
    {
        StartCoroutine(HandleDoorSoundAndSceneChange());
    }

    private IEnumerator HandleDoorSoundAndSceneChange()
    {
        if (audioSource != null && doorSound != null)
        {
            audioSource.Play(); // Reproducir el sonido de la puerta
            yield return new WaitForSeconds(doorSound.length); // Esperar hasta que termine el sonido
        }

        // Guardar el punto de spawn para la próxima escena
        PlayerPrefs.SetString("SpawnPoint", spawnPointTag);

        // Cargar la nueva escena
        SceneManager.LoadScene(sceneToLoad);
    }
}
