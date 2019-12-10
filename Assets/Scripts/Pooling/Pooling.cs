using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public float amountTopool;

    public GameObject ProductToPool;

    GameObject objToPool;
   
   [HideInInspector] 
   public List<GameObject> pool = new List<GameObject>();
   

    private void Start()
    {
        // add this pool to the list of pool manager
        PoolManager.Add(this);
        objToPool = ProductToPool.gameObject;
      

        for (int i = 0; i < amountTopool; i++)
        {
            GameObject poolObj = Instantiate(objToPool, transform);
            
            poolObj.SetActive(false);
            poolObj.GetComponent<DisableObjectAfterTime>().pool = this;
            pool.Add(poolObj);

        }
    }


    public GameObject getFromThePool(bool setActive = false)
    {
        if (pool.Count>0)
        {

            GameObject obj = pool[0];
            obj.SetActive(setActive);
           
            pool.RemoveAt(0);
            return obj;

        }
        GameObject newPooledObj = Instantiate(objToPool, transform);
        newPooledObj.SetActive(setActive);
       
        newPooledObj.GetComponent<DisableObjectAfterTime>().pool = this;
        
        return newPooledObj;
       
    }
    public GameObject getFromThePool(Transform Parent ,bool setActive = false )
    {
        if (pool.Count > 0)
        {

            pool[0].SetActive(setActive);
            pool[0].transform.SetParent(Parent);
            pool.RemoveAt(0);
            return pool[0];

        }
        GameObject newPooledObj = Instantiate(objToPool, transform);
        newPooledObj.SetActive(setActive);
        newPooledObj.transform.SetParent(Parent);
        newPooledObj.GetComponent<DisableObjectAfterTime>().pool = this;

        return newPooledObj;

    }

    public void returnToPool(GameObject _objTopool)
    {
       _objTopool.SetActive(false);
        pool.Add(_objTopool);
    }


  

  


}
