using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RagdollController : MonoBehaviour
{

    [Tooltip("These are the colliders that needs to be enabled when not using ragdoll")]
    public Collider[] colliderToEnable;

    [Tooltip("These are the rigidbodies that are attached to the no ragdoll colliders")]
    public Rigidbody[] rb;

    Collider[] allCollider;

    public Animator anim;

    Item_Affectable item_Affectable;

    private void Awake()
    {
        
        allCollider = GetComponentsInChildren<Collider>(true);
        EnableRagdoll(false);
    }

    void Start()
    {
        item_Affectable = GetComponent<Item_Affectable>();
        if (item_Affectable)
        {
            item_Affectable.onBombedListeners += OnBombed;
        }
    }

    void OnBombed()
    {
        EnableRagdoll(true);
    }
   
    public void EnableRagdoll(bool enableRagdoll)
    {
        anim.enabled = !enableRagdoll;
        foreach (Collider item in allCollider)
        {
            item.enabled = enableRagdoll;
            item.GetComponent<Rigidbody>().useGravity = enableRagdoll;
            item.GetComponent<Rigidbody>().isKinematic = !enableRagdoll;
  
        }

        foreach (Collider item in colliderToEnable)
        {
            item.enabled = !enableRagdoll;
        }

        foreach (Rigidbody item in rb)
        {

            item.useGravity = !enableRagdoll;
            item.isKinematic = enableRagdoll;
            if (enableRagdoll ==true)
            {
                Destroy(item);
            }
            
            
        }
        if (enableRagdoll)
        {
            gameObject.tag = "Finish";

        }
    }
}
