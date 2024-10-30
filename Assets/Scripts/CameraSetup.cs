using Cinemachine;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    private void Start()
    {
        // Buscar la CinemachineVirtualCamera en la escena
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            // Asignar el jugador como el objetivo a seguir
            virtualCamera.Follow = transform;
        }
    }
}
