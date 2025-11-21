using UnityEngine;

public class PoisonFrame : MonoBehaviour
{

    public Animator myAnimator;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

}
