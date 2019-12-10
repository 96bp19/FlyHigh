using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Freeze : APickUpItems
{
    public override void OnInteractedWithPlayer(Item_Affectable interactedObj)
    {
        interactedObj.OnFreezed();
        this.gameObject.SetActive(false);
    }
}
