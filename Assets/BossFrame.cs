using UnityEngine;

public class BossFrame : MonoBehaviour
{
    public Animator myAnimator;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }
}
