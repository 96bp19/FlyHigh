using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APickUpItems : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Item_Affectable affectable_object = other.GetComponent<Item_Affectable>();
            if (affectable_object)
            {
                OnInteractedWithPlayer(affectable_object);

            }
        }
    }

    public abstract void OnInteractedWithPlayer(Item_Affectable interactedObj);


   
}
