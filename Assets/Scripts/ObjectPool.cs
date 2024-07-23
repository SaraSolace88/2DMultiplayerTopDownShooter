using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public Queue<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    void Start()
    {
        pooledObjects = new Queue<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Enqueue(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        if (pooledObjects.Count != 0)
        {
            return pooledObjects.Dequeue();
        }else
        {
            return null;
        }
    }

    public void AddPooledObject(GameObject g)
    {
        pooledObjects.Enqueue(g);
    }
}