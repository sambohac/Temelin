using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroController : MonoBehaviour
{
    [SerializeField]
    int gameScene;

    VideoPlayer video;

    public void StartGame()
    {
        Debug.Log("Start game");
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += LoadGame;
    }

    private void LoadGame(VideoPlayer vp)
    {
        SceneManager.LoadScene(gameScene);
    }

    public void EndGame()
    {
        Application.Quit();
    }

}
