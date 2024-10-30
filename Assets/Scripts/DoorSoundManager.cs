using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSoundManager : MonoBehaviour
{
    public AudioClip doorOpenSound;    
    public AudioClip doorLockedSound;  
    public string sceneToLoad;         
    public string spawnPointTag;       
    private AudioSource audioSource;   

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    
    public void PlayDoorSoundAndChangeScene()
    {
        StartCoroutine(HandleDoorSoundAndSceneChange());
    }

    private IEnumerator HandleDoorSoundAndSceneChange()
    {
        if (audioSource != null && doorOpenSound != null)
        {
            audioSource.clip = doorOpenSound;
            audioSource.Play(); 
            yield return new WaitForSeconds(doorOpenSound.length); 
        }

        // Guardar el punto de spawn para la próxima escena
        PlayerPrefs.SetString("SpawnPoint", spawnPointTag);

        
        SceneManager.LoadScene(sceneToLoad);
    }

    // Reproducir el sonido de puerta bloqueada
    public void PlayDoorLockedSound()
    {
        if (audioSource != null && doorLockedSound != null)
        {
            audioSource.clip = doorLockedSound;
            audioSource.Play(); 
        }
    }
}