using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
     Animator anim;
    public float flipAnimDelay=0.5f;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void OnPlayerLaunched()
    {
        Invoke("setFlipAnimation", flipAnimDelay);
    }

    void setFlipAnimation()
    {
        anim.SetInteger("flipIndex", Random.Range(0, 4));
        Invoke("ResetAnimationFlipIndex", 0.1f);
    }


    void ResetAnimationFlipIndex()
    {
       anim.SetInteger("flipIndex", 5);
    }
}
