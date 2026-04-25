using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

internal class Character
{
    internal string pathToIcon;
    internal List<Skill> skills;

    public Character()
    { 
        skills = new List<Skill>();
    }

    public override string ToString()
    {
        string data = pathToIcon + "\n";
        for (int i = 0; i < skills.Count; i++)
        {
            data += skills[i].ToString() + "\n";
        }
        return data;
    }
}

public class CharacterController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Sprite icon;

    [SerializeField]
    GameObject skillsPanel;

    [SerializeField]
    GameObject textSkill;

    [SerializeField]
    Transform textEventParent;

    [SerializeField]
    GameObject[] slots;
    List<Skill> skills;
    List<GameObject> textSkills;

    internal Character character;
    internal ActionController actionController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        skills = new List<Skill>();
        for (int i = 0; i < slots.Length; i++)
        {
            Skill currSkill = new Skill();
            currSkill.stat1 = (Stats)(Random.value * 3);
            currSkill.value1 = (int)(Random.value * 10);
            currSkill.stat2 = (Stats)(Random.value * 3);
            currSkill.value2 = (int)(Random.value * 10);
            skills.Add(currSkill);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        actionController.ChangeActiveCharacter(this);
        Debug.Log("Clicked on me");
    }

    internal void ShowActions()
    {
        skillsPanel.SetActive(!skillsPanel.activeInHierarchy);
        textSkills = new List<GameObject>();
        for (int i = 0; i < slots.Length; i++)
        {
            var spawnPos = slots[i].transform.position;
            GameObject text = Instantiate(textSkill, spawnPos, new Quaternion(), textEventParent);
            Debug.Log(skills.Count);
            text.GetComponent<TMP_Text>().text = skills[i].ToString();

            textSkills.Add(text);
        }
    }

    internal void HideActions()
    {
        skillsPanel.SetActive(!skillsPanel.activeInHierarchy);
        for (int i = 0; i < slots.Length; i++)
        {
            Destroy(textSkills[i]);
        }
    }
}
