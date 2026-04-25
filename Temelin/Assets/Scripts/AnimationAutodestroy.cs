using UnityEngine;

public class AnimationAutodestroy : MonoBehaviour
{
    public float delay = 0f;
    internal float multiplier = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length * multiplier + delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
