using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Affectable : MonoBehaviour
{
    public Animator anim;
    Rigidbody rb;

    public Rigidbody ragdoll_rb;
    private bool Invincible = false;

    private IEnumerator functionRoutine;

    public delegate void _OnBombed();
    public _OnBombed onBombedListeners;




    public void OnlevelRestarted()
    {
       
        GetComponent<RagdollController>().EnableRagdoll(false);
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().isKinematic = false;

        }
        MobileInputHandler.EnableInput(true);
        anim.speed = 1;
        Invincible = false;

    }

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
        LevelManager.LevelRestartEventListeners += OnlevelRestarted;
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
        MobileInputHandler.EnableInput(false);
        rb.isKinematic = false;
    }

    public void OnBombed()
    {
        if (IsPlayerInvincible())
        {
            ApplyInvincibleStateAbility();
            return;
        }
        
        MobileInputHandler.EnableInput(false);
        onBombedListeners?.Invoke();
        LevelManager.Instance.OnPlayerDied();
      
        ragdoll_rb.AddExplosionForce(30000,transform.position,50);

      
       
    }

    public void OnInvincible(float invincibleTime)
    {
        Time.timeScale = 1.2f;
        setInvincible(true);
        MobileInputHandler.EnableInput(true);

        System.Action functionToCall = () => ResetInvincibleState();
        MyMath.StopCalledFunction(this,functionRoutine);
        MyMath.RunFunctionAfter(functionToCall, this, invincibleTime,out functionRoutine);
              
    }

    public void OnScoreItemPicked()
    {
        
    }

    public void OnBonusItemPicked()
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
