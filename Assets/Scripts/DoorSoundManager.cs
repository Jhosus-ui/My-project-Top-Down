using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSoundManager : MonoBehaviour
{
    public AudioClip doorOpenSound;    // Sonido de abrir la puerta
    public AudioClip doorLockedSound;  // Sonido de puerta bloqueada (cuando no se tiene la llave)
    public string sceneToLoad;         // Nombre de la escena a cargar
    public string spawnPointTag;       // Tag del punto de spawn en la nueva escena
    private AudioSource audioSource;   // Referencia al componente AudioSource

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Si no hay AudioSource, agregarlo automáticamente
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Reproducir el sonido de abrir la puerta y cambiar de escena
    public void PlayDoorSoundAndChangeScene()
    {
        StartCoroutine(HandleDoorSoundAndSceneChange());
    }

    private IEnumerator HandleDoorSoundAndSceneChange()
    {
        if (audioSource != null && doorOpenSound != null)
        {
            audioSource.clip = doorOpenSound;
            audioSource.Play(); // Reproducir el sonido de abrir la puerta
            yield return new WaitForSeconds(doorOpenSound.length); // Esperar hasta que termine el sonido
        }

        // Guardar el punto de spawn para la próxima escena
        PlayerPrefs.SetString("SpawnPoint", spawnPointTag);

        // Cargar la nueva escena
        SceneManager.LoadScene(sceneToLoad);
    }

    // Reproducir el sonido de puerta bloqueada
    public void PlayDoorLockedSound()
    {
        if (audioSource != null && doorLockedSound != null)
        {
            audioSource.clip = doorLockedSound;
            audioSource.Play(); // Reproducir el sonido de puerta bloqueada
        }
    }
}