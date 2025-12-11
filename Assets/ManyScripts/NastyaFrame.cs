using UnityEngine;

public class NastyaFrame : MonoBehaviour
{
    public Animator myAnimator;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }
}
