using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{

    public float rotateSpeed =5;
    private void Update()
    {
        transform.Rotate(0, rotateSpeed *Time.deltaTime, 0);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
