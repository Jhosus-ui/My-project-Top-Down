using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public int sceneToLoad = 2; // Número de la escena del nivel inicial para reiniciar
    public GameObject virtualCamera; // Asigna la Virtual Camera en el Inspector

    // Método para reiniciar el juego al hacer clic en "Restart"
    public void RestartGame()
    {
        // Verificar si el PlayerController existe y luego restablecer su posición
        if (PlayerController.instance != null)
        {
            PlayerController.instance.ResetPlayerPosition();
        }

        // Reactivar la cámara virtual al reiniciar la partida
        if (virtualCamera != null && !virtualCamera.activeInHierarchy)
        {
            virtualCamera.SetActive(true);
        }

        // Cargar la escena inicial del nivel
        SceneManager.LoadScene(sceneToLoad);
    }

    // Método para salir del juego al hacer clic en "Salir"
    public void ExitGame()
    {
        PlayerPrefs.DeleteKey("InitialPositionSaved"); // Limpiar el estado de posición inicial
        Application.Quit();
    }

    // Método para volver al menú principal (escena 0)
    public void ReturnToMainMenu()
    {
        // Desactivar la cámara virtual y su UI asociada
        if (virtualCamera != null)
        {
            virtualCamera.SetActive(false);
        }

        // Destruir al personaje para evitar su persistencia en el menú
        if (PlayerController.instance != null)
        {
            Destroy(PlayerController.instance.gameObject);
        }

        // Cargar la escena del menú principal (escena 0)
        SceneManager.LoadScene(0);
    }
}
