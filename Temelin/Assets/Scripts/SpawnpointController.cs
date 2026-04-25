using TMPro;
using UnityEngine;

public class SpawnpointController : MonoBehaviour
{
    [SerializeField]
    GameObject[] availibleEvents;

    [SerializeField]
    GameObject text;
    [SerializeField]
    Transform textEventParent;

    internal bool occupied;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveEvent()
    {
        occupied = false;
    }

    public EventController SpawnRandomEvent()
    {
        int index = Random.Range(0, availibleEvents.Length);
        GameObject spawnedEvent = Instantiate(availibleEvents[index], this.transform);
        GameObject textEv = Instantiate(text, transform.position - new Vector3(0, 1f, 0), new Quaternion(), textEventParent);

        EventController eventController = spawnedEvent.GetComponent<EventController>();
        eventController.CreateEvent(textEv, spawnedEvent);
        textEv.GetComponent<TMP_Text>().text = eventController.thisEvent.ToString();
        eventController.spawnpoint = this;

        occupied = true;
        return eventController;
    }
}
