using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuIncial2 : MonoBehaviour
{

    [SerializeField] private int ChangeScene;
    [SerializeField] private int RestartGame;
    [SerializeField] private int MenuInitial;
    // Start is called before the first frame update

    public void BackGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - ChangeScene);
    }

    public void Exit()
    {
        Debug.Log("Exit.....");
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - RestartGame);
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - MenuInitial);
    }
}