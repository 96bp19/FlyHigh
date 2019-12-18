using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Bonus : APickUpItems
{
    public override void OnInteractedWithPlayer(Item_Affectable interactedObj)
    {
        interactedObj.OnBonusItemPicked();
        gameObject.SetActive(false);
    }
}
