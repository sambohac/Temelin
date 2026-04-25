using NUnit.Framework;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class GameLoopController : MonoBehaviour
{
    [SerializeField]
    Powerplant powerplant;
    [SerializeField]
    GameObject characterPrefab;
    [SerializeField]
    SpriteRenderer hurtScreen;

    [SerializeField]
    int winScene;
    [SerializeField]
    int endScene;

    [SerializeField]
    CharacterController[] characters;

    [SerializeField]
    EventGeneratorController eventGeneratorController;

    bool fading;

    string characterSource;

    System.Collections.Generic.List<Character> characterList;

    private void Awake()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].gameLoopController = this;
        }

        // start powerplant
        powerplant.gameLoopController = this;
        eventGeneratorController.gameLoopController = this;
        StartGame();
    }

    private void Update()
    {
        
    }

    public void StartGame()
    {
        powerplant.StartPowerplant();
    }

    public void EndGame()
    {
        SceneManager.LoadScene(endScene);
    }

    public void WinGame()
    {
        SceneManager.LoadScene(winScene);
    }

    internal void EventSolved(EventController ec)
    {
        eventGeneratorController.RemoveEvent(ec);
    }

    internal void HurtPowerPlant(int damage)
    {
        powerplant.TakeDamage(damage);
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        while (fading)
        {
            yield return new WaitForSeconds(0.05f);
        }

        fading = true;
        float alphaVal = hurtScreen.color.a;
        Color tmp = hurtScreen.color;

        while (hurtScreen.color.a > 0)
        {
            alphaVal -= 0.1f;
            tmp.a = alphaVal;
            hurtScreen.color = tmp;

            yield return new WaitForSeconds(0.05f); // update interval
        }
        fading = false;
    }

    private IEnumerator FadeIn()
    {

        while (fading)
        {
            yield return new WaitForSeconds(0.05f);
        }

        fading = true;
        float alphaVal = hurtScreen.color.a;
        Color tmp = hurtScreen.color;

        while (hurtScreen.color.a < 1)
        {
            alphaVal += 0.1f;
            tmp.a = alphaVal;
            hurtScreen.color = tmp;

            yield return new WaitForSeconds(0.05f); // update interval
        }
        fading= false;

        StartCoroutine(FadeOut());
    }

}
