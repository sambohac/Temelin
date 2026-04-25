using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroController : MonoBehaviour
{
    [SerializeField]
    int introScene;

    public void StartGame()
    {
        Debug.Log("Start game");
        SceneManager.LoadScene(introScene);
    }

    public void EndGame()
    {
        Application.Quit();
    }

}
