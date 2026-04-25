using UnityEngine;

public class AnimationAutodestroy : MonoBehaviour
{
    public float delay = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
