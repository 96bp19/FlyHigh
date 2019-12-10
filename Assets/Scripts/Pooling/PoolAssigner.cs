using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolAssigner : MonoBehaviour
{
    [System.Serializable]

    public struct poolValue
    {
        
        public GameObject poolObject;
        public int poolAmount;
    }

    public poolValue[] pools;

    private void Awake()
    {
        this.gameObject.name = "All Pool";
        foreach (var item in pools)
        {

            GameObject obj = new GameObject(item.poolObject.name);
            obj.transform.SetParent(this.transform);
            obj.AddComponent<Pooling>();
            obj.GetComponent<Pooling>().amountTopool = item.poolAmount;
            obj.GetComponent<Pooling>().ProductToPool = item.poolObject;
            
        }

    }
}

