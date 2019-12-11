using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Affectable : MonoBehaviour
{
    public Animator anim;
    Rigidbody rb;
    private bool Invincible = false;

    private IEnumerator functionRoutine;

    public bool IsPlayerInvincible()
    {
        return Invincible;
    }

    public void setInvincible(bool condition)
    {
        
        Invincible = condition;
    }

    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void OnFreezed()
    {

        if (IsPlayerInvincible())
        {
            ApplyInvincibleStateAbility();
            return;
        }
        rb.isKinematic = true;
        anim.speed = 0;
        InputHandler.EnableInput(false);
        rb.isKinematic = false;
    }

    public void OnBombed()
    {
        if (IsPlayerInvincible())
        {
            ApplyInvincibleStateAbility();
            return;
        }
        InputHandler.EnableInput(false);
    }

    public void OnInvincible(float invincibleTime)
    {
        Time.timeScale = 1.2f;
        setInvincible(true);
        InputHandler.EnableInput(true);

        System.Action functionToCall = () => ResetInvincibleState();
        MyMath.StopCalledFunction(this,functionRoutine);
        MyMath.RunFunctionAfter(functionToCall, this, invincibleTime,out functionRoutine);
              
    }

    public void OnScoreItemPicked()
    {
        
    }

    void  ApplyInvincibleStateAbility()
    {
        // this functon is called when player lands on bombs or any obstacle when being invincible

    }

    void ResetInvincibleState()
    {
       
        setInvincible(false);
        functionRoutine = null;

    }
}
