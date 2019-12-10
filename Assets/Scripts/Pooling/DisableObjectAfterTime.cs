using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjectAfterTime : MonoBehaviour
{
    [Tooltip("Object will be inactive after given time passes , count will start when the object is active for the first time")]
    public float activeTime =2f;

    
    public float currentTime;


    public bool shouldDestroy;

    [HideInInspector]
    public Pooling pool;

    private void Awake()
    {
        currentTime = activeTime;
      
        
    }


    private void Update()
    {
       
        currentTime -= Time.deltaTime;
        if (currentTime <=0)
        {

            if (this.gameObject != null && pool)
            {

                retunObjToPool();

            }
            else if (!shouldDestroy)
            {
                this.gameObject.SetActive(false);
                currentTime = activeTime;

            }
            else
            {
                Destroy(this.gameObject);
            }

            
          
        }
    }
   

    void retunObjToPool()
    {
        currentTime =activeTime;
       
        this.gameObject.SetActive(false);
        if (this.gameObject != null)
        {
            pool.returnToPool(this.gameObject);

        }
    }
}
