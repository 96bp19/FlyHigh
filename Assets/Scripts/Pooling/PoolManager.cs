using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
    
    static Dictionary<System.Type, List<Pooling>> oldPooledTypes = new Dictionary<System.Type, List<Pooling>>();
    static Dictionary<GameObject, List<Pooling>> newPooledTypes = new Dictionary<GameObject, List<Pooling>>();

    public static void Add(Pooling pool)
    {
        GameObject key = pool.ProductToPool.gameObject;

        if (!newPooledTypes.ContainsKey(key) || newPooledTypes[key] == null)
        {
            newPooledTypes[key] = new List<Pooling>();
        }
        if (!newPooledTypes[key].Contains(pool))
        {
            newPooledTypes[key].Add(pool);
        }
    }

    public static void Remove(Pooling pool)
    {
        GameObject key = pool.ProductToPool.gameObject;
        if (newPooledTypes.ContainsKey(key) || newPooledTypes[key] == null)
        {
            newPooledTypes[key] = new List<Pooling>();
            return;
        }
        if (newPooledTypes[key].Contains(pool))
        {
            newPooledTypes[key].Remove(pool);
        }
    }

  

    public static Pooling getPoolofType(GameObject product)
    {
        GameObject key = product;

        if (!newPooledTypes.ContainsKey(key) || newPooledTypes[key] == null)
        {
            Debug.Log("no object of given type found in the dictionary");
            return null;
        }
        return newPooledTypes[key][0];
    }
}
