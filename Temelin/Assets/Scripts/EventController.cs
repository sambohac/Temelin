using TMPro;
using UnityEngine;

class EventPowerplant
{
    internal Stats stat1;
    internal int value1;

    internal int damage;
    internal float timer;

    internal GameObject text;
    internal GameObject sprite;

    internal float timeToSolve;

    public EventPowerplant(Stats stat1, int value1, int damage, float timer, float timeToSolve)
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
        return $"{(int)(timer * 1000) / 1000.0} {damage} \n{stat1} {value1}";
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
            case Stats.stat1:
                if (character.stat1 >= value1)
                    return true;
                break;
            case Stats.stat2:
                if (character.stat2 >= value1)
                    return true;
                break;
            case Stats.stat3:
                if(character.stat3 >= value1)
                    return true;
                break;
        }
        return false;
    }
}

public class EventController : MonoBehaviour
{
    internal EventPowerplant thisEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
