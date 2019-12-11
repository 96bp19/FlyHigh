using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Invincible : APickUpItems
{
    public float invinclibleDuration= 5f;
    public override void OnInteractedWithPlayer(Item_Affectable interactedObj)
    {
        interactedObj.OnInvincible(invinclibleDuration);
        gameObject.SetActive(false);
       
        

    }

 
}
