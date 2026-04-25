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
    CharacterController[] characters;

    [SerializeField]
    EventGeneratorController eventGeneratorController;

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

    public void StartGame()
    {
        powerplant.StartPowerplant();
    }

    public void EndGame()
    {
        SceneManager.LoadScene(2);
    }

    public void WinGame()
    {
        SceneManager.LoadScene(3);
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
        float alphaVal = hurtScreen.color.a;
        Color tmp = hurtScreen.color;

        while (hurtScreen.color.a > 0)
        {
            alphaVal -= 0.1f;
            tmp.a = alphaVal;
            hurtScreen.color = tmp;

            yield return new WaitForSeconds(0.05f); // update interval
        }
    }

    private IEnumerator FadeIn()
    {
        float alphaVal = hurtScreen.color.a;
        Color tmp = hurtScreen.color;

        while (hurtScreen.color.a < 1)
        {
            alphaVal += 0.1f;
            tmp.a = alphaVal;
            hurtScreen.color = tmp;

            yield return new WaitForSeconds(0.05f); // update interval
        }

        StartCoroutine(FadeOut());
    }

}
