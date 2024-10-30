using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoor : MonoBehaviour
{
    public string finalSceneName; 
    private Collider2D doorCollider;

    private void Start()
    {
        doorCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Has alcanzado la puerta final. Cargando escena final...");
            SceneManager.LoadScene(finalSceneName);  
            Destroy(other.gameObject);  
        }
    }
}