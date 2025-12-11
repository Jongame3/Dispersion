using UnityEngine;

public class FoxFrame : MonoBehaviour
{
    public Animator myAnimator;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }
}
