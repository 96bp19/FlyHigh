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

        SubscribeToEvents();
        LevelManager.LevelRestartEventListeners += OnlevelRestarted;
    }

    public void OnLevelComplete()
    {

        UnSubscribeFromEvent();

        // do stuff when level completes
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        MobileInputHandler.EnableInput(false);

    }

    public void OnLevelFailed()
    {

        // do stuff when level fails
        UnSubscribeFromEvent();
        MobileInputHandler.EnableInput(false);
        

    }

    void UnSubscribeFromEvent()
    {
        LevelManager.Instance.levelCompleteEventListener.RemoveListener(OnLevelComplete);
        LevelManager.Instance.levelFailedEventListener.RemoveListener(OnLevelFailed);
    }
    void SubscribeToEvents()
    {
        LevelManager.Instance.levelCompleteEventListener.AddListener(OnLevelComplete);
        LevelManager.Instance.levelFailedEventListener.AddListener(OnLevelFailed);

    }


    public void OnlevelRestarted()
    {
        SubscribeToEvents();
       
    }

 
    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            if (!itemAffectable.IsPlayerInvincible())
            {
                Debug.Log("collided with : " + collision.gameObject.name);
                // Invoke("CheckForCollisionObject", 0.01f);
                if (collision.gameObject.GetComponent<Collider>().enabled)
                {
                    LevelManager.Instance.OnPlayerDied();
                    GetComponent<RagdollController>().EnableRagdoll(true);

                }

            }
        }
       
    }

   

    void CheckForCollisionObject()
    {
//         if (lastCollidedObject != null && lastCollidedObject.activeInHierarchy)
//         {
            Debug.Log("mission fail invoked");
            LevelManager.Instance.OnPlayerDied();
            GetComponent<RagdollController>().EnableRagdoll(true);

 //       }
    }
}
