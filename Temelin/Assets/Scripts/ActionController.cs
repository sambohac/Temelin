using UnityEngine;

public class ActionController : MonoBehaviour
{

    internal CharacterController activeCharacter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform child in gameObject.transform)
        {
            try
            {
                child.GetComponent<CharacterController>().actionController = this;
            }
            catch
            {

            }
        }
    }

    public void ChangeActiveCharacter(CharacterController newCharacter)
    {
        if (activeCharacter != null)
            activeCharacter.HideActions();

        if (activeCharacter == newCharacter)
        {
            activeCharacter = null;
            return;
        }

        activeCharacter = newCharacter;
        newCharacter.ShowActions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
