using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
                                                     (int)(1 + Random.value * 10), 10 + Random.value * 5, 3 + Random.value * 3);
        currentlyActiveEvents.Add(evCurr);

        float x = Random.Range(startArea.transform.position.x, endArea.transform.position.x);
        float y = Random.Range(startArea.transform.position.y, endArea.transform.position.y);
        var spawnPos = new Vector3(x, y, 0);

        GameObject spriteEv = Instantiate(sprite, spawnPos, new Quaternion(), eventParent);
        GameObject textEv = Instantiate(text, spawnPos, new Quaternion(), textEventParent);
        textEv.GetComponent<TMP_Text>().text = evCurr.ToString();
        spriteEv.GetComponent<EventController>().thisEvent = evCurr;

        evCurr.sprite = spriteEv;
        evCurr.text = textEv;
    }

    internal void RemoveEvent(EventPowerplant collidedEvent)
    {
        int index = currentlyActiveEvents.IndexOf(collidedEvent);
        if (index >= 0)
        {
            Destroy(collidedEvent.sprite);
            Destroy(collidedEvent.text);

            currentlyActiveEvents.Remove(collidedEvent);
        }
    }
}
