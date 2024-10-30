using UnityEngine;

public class Door1 : MonoBehaviour
{
    public string spawnPointTag;      // El tag del punto de spawn en la nueva escena
    public string requiredKeyID;      // Identificador de la llave requerida
    private bool isLocked = true;     // Estado de la puerta, inicialmente bloqueada
    private DoorSoundManager doorSoundManager;  // Referencia al script DoorSoundManager

    private void Start()
    {
        doorSoundManager = GetComponent<DoorSoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null && player.HasKey(requiredKeyID))
            {
                isLocked = false; 
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
                if (doorSoundManager != null)
                {
                    doorSoundManager.spawnPointTag = spawnPointTag; 
                    doorSoundManager.PlayDoorSoundAndChangeScene(); 
                }
                else
                {
                    Debug.LogWarning("DoorSoundManager no asignado.");
                }
            }
        }
    }
}
