﻿using System.Collections;
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
        InputHandler.EnableInput(false);

    }

    public void OnLevelFailed()
    {

        // do stuff when level fails
        UnSubscribeFromEvent();
     
        

    }

    void UnSubscribeFromEvent()
    {
        LevelManager.Instance.levelCompleteEvent.RemoveListener(OnLevelComplete);
        LevelManager.Instance.levelFailedEvent.RemoveListener(OnLevelFailed);
    }
    void SubscribeToEvents()
    {
        LevelManager.Instance.levelCompleteEvent.AddListener(OnLevelComplete);
        LevelManager.Instance.levelFailedEvent.AddListener(OnLevelFailed);

    }


    public void OnlevelRestarted()
    {
        SubscribeToEvents();
       
    }

    GameObject lastCollidedObject = null;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Ground"))
        {
            if (!itemAffectable.IsPlayerInvincible())
            {
                Debug.Log("collided with : " + collision.gameObject.name);
                lastCollidedObject = collision.gameObject;
                Invoke("CheckForCollisionObject", 0.1f);

            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") )
        {
            lastCollidedObject = null;
        }
    }

    void CheckForCollisionObject()
    {
        if (lastCollidedObject != null && lastCollidedObject.activeInHierarchy)
        {
            LevelManager.Instance.OnPlayerDied();
            GetComponent<RagdollController>().EnableRagdoll(true);

        }
    }
}
