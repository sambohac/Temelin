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

    string characterSource;

    System.Collections.Generic.List<Character> characterList;

    private void Start()
    {
        // load characters from file - there will be three
        // LoadCharacters(characterSource);

        // start powerplant
        powerplant.gameLoopController = this;
        StartGame();
    }

    private void LoadCharacters(string characterSource)
    {
        characterList = new System.Collections.Generic.List<Character>();
        StreamReader sr = new StreamReader(characterSource);
        string line = sr.ReadLine();
        Character character = new Character();

        while (line != null)
        {
            character.pathToIcon = line;
            line = LoadSkills(sr, character);
            characterList.Add(character);
            character = new Character();
            line = sr.ReadLine();
        }

        //close the file
        sr.Close();

        Debug.Log("Loaded");
        for (int i = 0; i < characterList.Count; i++)
        {
            Debug.Log(characterList[i]);
        }
    }

    private string LoadSkills(StreamReader sr, Character character)
    {
        string line = sr.ReadLine();
        while (line != null && line.Trim().Length != 0)
        {
            Skill skill = new Skill();

            string[] statInfo = line.Split(' ');
            skill.stat1 = Enum.Parse<Stats>(statInfo[0]);
            skill.value1 = int.Parse(statInfo[1]);
            line = sr.ReadLine();

            statInfo = line.Split(' ');
            skill.stat2 = Enum.Parse<Stats>(statInfo[0]);
            skill.value2 = int.Parse(statInfo[1]);

            character.skills.Add(skill);

            line = sr.ReadLine();
        }

        return line;
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

}
