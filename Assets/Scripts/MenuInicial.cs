using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuInicial : MonoBehaviour
{

    [SerializeField] private int ChangeSceneStart;
    [SerializeField] private int ChangeSceneInstru;
    // Start is called before the first frame update

    public void StartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + ChangeSceneStart);
    }

    public void Instructions()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + ChangeSceneInstru);
    }

    public void Exit()
    {
        Debug.Log("Exit.....");
            Application.Quit();
    }
}