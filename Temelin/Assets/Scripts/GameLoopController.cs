using NUnit.Framework;
using System;
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

    internal void EventSolved(EventPowerplant solvedEvent)
    {
        eventGeneratorController.RemoveEvent(solvedEvent);
    }

    internal void HurtPowerPlant(int damage)
    {
        powerplant.TakeDamage(damage);
    }
}
