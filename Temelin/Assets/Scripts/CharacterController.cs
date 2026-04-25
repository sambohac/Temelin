using UnityEngine;
using UnityEngine.EventSystems;

internal class Character
{
    internal int stat1;
    internal int stat2;
    internal int stat3;

    public Character(int stat1, int stat2, int stat3)
    { 
        this.stat1 = stat1; this.stat2 = stat2; this.stat3 = stat3;
    }

    public override string ToString()
    {
        string data = $"stat1: {stat1} \nstat2: {stat2} \nstat3: {stat3}";
        return data;
    }
}

public class CharacterController : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    GameObject icon;
    [SerializeField]
    GameObject inactiveIcon;
    [SerializeField]
    GameObject hurtIcon;

    [SerializeField]
    GameObject skillsPanel;

    [SerializeField]
    GameObject textSkill;

    [SerializeField]
    Transform textEventParent;

    [SerializeField]
    GameObject representation;

    [SerializeField]
    string characterName;

    [SerializeField]
    int strength;
    [SerializeField]
    int intelect;
    [SerializeField]
    int handyness;

    bool inUse;
    float cooldown;

    internal Character character;
    internal ActionController actionController;
    internal GameLoopController gameLoopController; // set during awake

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        character = new Character(strength, intelect, handyness);

        DragAndCollide dragger = representation.GetComponent<DragAndCollide>();
        dragger.startPos = representation.transform.position;
        dragger.originatingCharacter = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (inUse)
        {
            cooldown -= Time.deltaTime;
        }

        if (cooldown <= 0 && inUse)
        {
            inUse = false;
            inactiveIcon.SetActive(false);
            hurtIcon.SetActive(false);
            icon.SetActive(true);
            Debug.Log(characterName + ": Im ready!");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // actionController.ChangeActiveCharacter(this);
        //Debug.Log(name + ": Clicked on me!");
    }

    internal void ShowActions()
    {
        Debug.Log(characterName + ": " + character.ToString());
    }

    internal void HideActions()
    {

    }

    internal void UseCharacter(PowerplantEvent eventToSolve, EventController ec)
    {
        if (inUse)
        {
            Debug.Log(characterName + ": Im on cooldown");
            return;
        }

        if (!eventToSolve.CanCharacterSolve(character))
        {
            Debug.Log(characterName + ": OUCH!!");
            icon.SetActive(false);
            inactiveIcon.SetActive(false);
            hurtIcon.SetActive(true);
            gameLoopController.HurtPowerPlant(eventToSolve.damage);
        }
        else
        {
            Debug.Log(characterName + ": Working on it!");
            icon.SetActive(false);
            hurtIcon.SetActive(false);
            inactiveIcon.SetActive(true);
        }

        gameLoopController.EventSolved(ec);
        cooldown = eventToSolve.timeToSolve;
        inUse = true;
    }

    internal void MoveRepresentation(Vector3 startPos)
    {
        representation.transform.position = startPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        representation.GetComponent<DragAndCollide>().OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        representation.GetComponent<DragAndCollide>().OnEndDrag(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(characterName + ": what now?");
        representation.GetComponent<DragAndCollide>().OnBeginDrag(eventData);
    }
}
