using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    [SerializeField]
    int gameScene;

    public void StartGame()
    {
        Debug.Log("Start game");
        SceneManager.LoadScene(gameScene);
    }

    public void EndGame()
    {
        Application.Quit();
    }

}
