using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

class EventPowerplant
{
    internal Stats stat1;
    internal int value1;

    internal Stats stat2;
    internal int value2;

    internal int damage;
    internal float timer;

    internal GameObject text;
    internal GameObject sprite;

    public EventPowerplant(Stats stat1, int value1, Stats stat2, int value2, int damage, float timer)
    {
        this.stat1 = stat1;
        this.value1 = value1;
        this.stat2 = stat2;
        this.value2 = value2;
        this.damage = damage;
        this.timer = timer;
    }

    public void UpdateTime(float deltaTime)
    {
        timer -= deltaTime;
        text.GetComponent<TMP_Text>().text = ToString();
    }

    public override string ToString()
    {
        return $"{(int)(timer*1000) / 1000.0} {damage} \n{stat1} {value1} \n{stat2} {value2}";
    }
}

public class EventGeneratorController : MonoBehaviour
{
    [SerializeField]
    GameObject startArea;
    [SerializeField]
    GameObject endArea;

    [SerializeField]
    Powerplant powerplant;

    [SerializeField]
    GameObject sprite;
    [SerializeField]
    GameObject text;

    [SerializeField]
    Transform eventParent;
    [SerializeField]
    Transform textEventParent;

    List<EventPowerplant> currentlyActiveEvents;

    float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentlyActiveEvents = new List<EventPowerplant>();
        timer = Random.value * 5;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            GenerateNewEvent();
            timer = Random.value * 5;
        }

        for (int i = 0; i < currentlyActiveEvents.Count; ++i)
        {
            EventPowerplant currEvent = currentlyActiveEvents[i];
            currEvent.UpdateTime(Time.deltaTime);
            if (currEvent.timer <= 0)
            {
                powerplant.TakeDamage(currEvent.damage);
                Destroy(currEvent.sprite);
                Destroy(currEvent.text);
                currentlyActiveEvents.Remove(currEvent);
            }
        }
    }

    private void GenerateNewEvent()
    {
        EventPowerplant evCurr = new EventPowerplant((Stats)(Random.value * 3), (int)(Random.value * 10),
                                                     (Stats)(Random.value * 3), (int)(Random.value * 10),
                                                     (int)(1 + Random.value * 10), 3 + Random.value * 5);
        currentlyActiveEvents.Add(evCurr);

        float x = Random.Range(startArea.transform.position.x, endArea.transform.position.x);
        float y = Random.Range(startArea.transform.position.y, endArea.transform.position.y);
        var spawnPos = new Vector3(x, y, 0);

        GameObject spriteEv = Instantiate(sprite, spawnPos, new Quaternion(), eventParent);
        GameObject textEv = Instantiate(text, spawnPos, new Quaternion(), textEventParent);
        textEv.GetComponent<TMP_Text>().text = evCurr.ToString();

        evCurr.sprite = spriteEv;
        evCurr.text = textEv;
    }
}
