using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Score : APickUpItems
{
    public override void OnInteractedWithPlayer(Item_Affectable interactedObj)
    {
        interactedObj.OnScoreItemPicked();
        this.gameObject.SetActive(false);
    }
}
    

