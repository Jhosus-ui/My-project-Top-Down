using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public int sceneToLoad = 2; // N�mero de la escena del nivel inicial para reiniciar
    public GameObject virtualCamera; // Asigna la Virtual Camera en el Inspector

    // M�todo para reiniciar el juego al hacer clic en "Restart"
    public void RestartGame()
    {
        // Verificar si el PlayerController existe y luego restablecer su posici�n
        if (PlayerController.instance != null)
        {
            PlayerController.instance.ResetPlayerPosition();
        }

        // Reactivar la c�mara virtual al reiniciar la partida
        if (virtualCamera != null && !virtualCamera.activeInHierarchy)
        {
            virtualCamera.SetActive(true);
        }

        // Cargar la escena inicial del nivel
        SceneManager.LoadScene(sceneToLoad);
    }

    // M�todo para salir del juego al hacer clic en "Salir"
    public void ExitGame()
    {
        PlayerPrefs.DeleteKey("InitialPositionSaved"); // Limpiar el estado de posici�n inicial
        Application.Quit();
    }

    // M�todo para volver al men� principal (escena 0)
    public void ReturnToMainMenu()
    {
        // Desactivar la c�mara virtual y su UI asociada
        if (virtualCamera != null)
        {
            virtualCamera.SetActive(false);
        }

        // Destruir al personaje para evitar su persistencia en el men�
        if (PlayerController.instance != null)
        {
            Destroy(PlayerController.instance.gameObject);
        }

        // Cargar la escena del men� principal (escena 0)
        SceneManager.LoadScene(0);
    }
}
