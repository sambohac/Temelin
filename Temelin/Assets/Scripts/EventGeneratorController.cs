using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class EventGeneratorController : MonoBehaviour
{
    [SerializeField]
    GameObject startArea;
    [SerializeField]
    GameObject endArea;

    [SerializeField]
    Powerplant powerplant;
    
    [SerializeField]
    Transform spawnPointParent;

    [SerializeField]
    GameObject sprite;

    [SerializeField]
    Transform eventParent;

    List<EventController> currentlyActiveEvents;
    List<Transform> spawnPoints;

    float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoints = new List<Transform>();
        foreach (Transform child in spawnPointParent.transform)
        {
            Debug.Log(child.name);
            if (child.tag == "spawnpoint")
            {
                spawnPoints.Add(child);
            }
        }

        currentlyActiveEvents = new List<EventController>();
        timer = Random.value * 5;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            GenerateNewEvent();
            timer = 2  + Random.value * 5;
        }

        for (int i = 0; i < currentlyActiveEvents.Count; ++i)
        {
            PowerplantEvent currEvent = currentlyActiveEvents[i].thisEvent;
            currEvent.UpdateTime(Time.deltaTime);
            if (currEvent.timer <= 0)
            {
                powerplant.TakeDamage(currEvent.damage);
                Destroy(currEvent.sprite);
                Destroy(currEvent.text);

                currentlyActiveEvents[i].spawnpoint.RemoveEvent();
                currentlyActiveEvents.Remove(currentlyActiveEvents[i]);
            }
        }
    }

    private void GenerateNewEvent()
    {
        int index = Random.Range(0, spawnPoints.Count);
        int maxTries = 50;
        int its = 0;
        while (spawnPoints[index].GetComponent<SpawnpointController>().occupied && its < maxTries)
        {
            index = Random.Range(0, spawnPoints.Count);
            its++;
        }

        if (spawnPoints[index].GetComponent<SpawnpointController>().occupied)
            return;
        
        Debug.Log("Spawning event on " + index);
        EventController ec = spawnPoints[index].GetComponent<SpawnpointController>().SpawnRandomEvent();
        currentlyActiveEvents.Add(ec);   
    }

    internal void RemoveEvent(EventController ec)
    {
        int index = currentlyActiveEvents.IndexOf(ec);
        ec.spawnpoint.RemoveEvent();
        if (index >= 0)
        {
            // die
            Destroy(ec.thisEvent.text);
            Destroy(ec.transform.gameObject);
            currentlyActiveEvents.Remove(ec);
        }
    }
}
