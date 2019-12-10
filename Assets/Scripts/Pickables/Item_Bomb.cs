using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Bomb : APickUpItems
{
    public override void OnInteractedWithPlayer(Item_Affectable interactedObj)
    {
        interactedObj.OnBombed();
        this.gameObject.SetActive(false);
    }
}
