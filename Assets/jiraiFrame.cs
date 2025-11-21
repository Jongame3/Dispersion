using UnityEngine;

public class jiraiFrame : MonoBehaviour
{
    public Animator myAnimator;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }
}
