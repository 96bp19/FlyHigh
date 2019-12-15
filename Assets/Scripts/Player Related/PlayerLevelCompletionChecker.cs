using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelCompletionChecker : MonoBehaviour
{
    // Start is called before the first frame update

    Item_Affectable itemAffectable;
    void Start()
    {
        itemAffectable = GetComponent<Item_Affectable>();
        LevelManager.levelCompleteEventListeners += OnLevelComplete;
        LevelManager.levelFailedEventListeners += OnLevelFailed;
    }

    public void OnLevelComplete()
    {
        // unsubscribe from the events first
        LevelManager.levelCompleteEventListeners -= OnLevelComplete;
        LevelManager.levelFailedEventListeners -= OnLevelFailed;

        // do stuff when level completes
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        InputHandler.EnableInput(false);

    }

    public void OnLevelFailed()
    {
        // unsubscribe from the event first
        LevelManager.levelCompleteEventListeners -= OnLevelComplete;
        LevelManager.levelFailedEventListeners -= OnLevelFailed;

        // do stuff when level fails
     
        

    }

    GameObject lastCollidedObject = null;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!itemAffectable.IsPlayerInvincible())
            {
                Debug.Log("collided with : " + collision.gameObject.name);
                lastCollidedObject = collision.gameObject;
                Invoke("CheckForCollisionObject", 1);

            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            lastCollidedObject = null;
              

            
        }
    }

    void CheckForCollisionObject()
    {
        if (lastCollidedObject != null && lastCollidedObject.activeInHierarchy)
        {
            LevelManager.OnPlayerDied();
            GetComponent<RagdollController>().EnableRagdoll(true);

        }
    }
}
