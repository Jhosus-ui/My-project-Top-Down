using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door3 : MonoBehaviour
{
    public string spawnPointTag;  
    public string requiredKeyID;   
    private bool isLocked = true;   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null && player.HasKey(requiredKeyID))
            {
                
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
            if (!isLocked) 
            {
                
                PlayerPrefs.SetString("LastSpawnPoint", spawnPointTag);
                PlayerPrefs.SetString("SpawnPoint", spawnPointTag);

                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }
        }
    }
}
