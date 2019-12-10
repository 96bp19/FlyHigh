using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Affectable : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void OnFreezed()
    {
        Debug.Log("freezed");
        InputHandler.EnableInput(false);
    }

    public void OnBombed()
    {
        Debug.Log("bombed");
        InputHandler.EnableInput(false);
    }

    public void OnInvincible()
    {
        Debug.Log("Invincible");
      
    }

    public void OnScoreItemPicked()
    {
        Debug.Log("score item picked");
    }
}
