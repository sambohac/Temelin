using System;
using TMPro;
using UnityEngine;

class PowerplantEvent
{
    internal Stats stat1;
    internal int value1;

    internal int damage;
    internal float timer;
    internal float timeToSolve;

    internal GameObject text;
    internal GameObject sprite;

    public PowerplantEvent(Stats stat1, int value1, int damage, float timer, float timeToSolve)
    {
        this.stat1 = stat1;
        this.value1 = value1;
        this.damage = damage;
        this.timer = timer;
        this.timeToSolve = timeToSolve;
    }

    public void UpdateTime(float deltaTime)
    {
        timer -= deltaTime;
        text.GetComponent<TMP_Text>().text = ToString();
    }

    public override string ToString()
    {
        return $"{damage} {(int)(timer * 1000) / 1000.0}"; // \n{stat1} {value1}";
    }

    /// <summary>
    /// true or false if event is solvable by the character
    /// </summary>
    /// <param name="skill"></param>
    /// <returns></returns>
    internal bool CanCharacterSolve(Character character)
    {
        switch (stat1)
        {
            case Stats.strength:
                if (character.stat1 >= value1)
                    return true;
                break;
            case Stats.intelect:
                if (character.stat2 >= value1)
                    return true;
                break;
            case Stats.handyness:
                if(character.stat3 >= value1)
                    return true;
                break;
        }
        return false;
    }
}

public class EventController : MonoBehaviour
{
    internal PowerplantEvent thisEvent;
    internal SpawnpointController spawnpoint;

    [SerializeField]
    Stats stat;

    [SerializeField]
    int value;

    [SerializeField]
    internal int damage;
    
    [SerializeField]
    internal float timer;
    
    [SerializeField]
    internal float timeToSolve;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void CreateEvent(GameObject textEv, GameObject spawnedEvent)
    {
        thisEvent = new PowerplantEvent(stat, value, damage, timer, timeToSolve);
        thisEvent.text = textEv;
        thisEvent.sprite = spawnedEvent;
    }
}
