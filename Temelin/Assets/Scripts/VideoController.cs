using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using static UnityEngine.ParticleSystem;

public class VideoController : MonoBehaviour
{
    [SerializeField]
    VideoPlayer video;

    [SerializeField]
    int gameScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        video = GetComponent<VideoPlayer>();

        // Each time the video reaches the end, call this function. 
        video.loopPointReached += OnLoopPointReached;

        video.Play();
    }

    private void OnLoopPointReached(VideoPlayer source)
    {
        Debug.Log("Start game");
        SceneManager.LoadScene(gameScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
