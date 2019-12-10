using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Invincible : APickUpItems
{
    public override void OnInteractedWithPlayer(Item_Affectable interactedObj)
    {
        interactedObj.OnInvincible();
        this.gameObject.SetActive(false);
    }
}
