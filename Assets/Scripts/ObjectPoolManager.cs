using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField]
    private string objectTag;

    private Queue<GameObject> objectQueue;
        
    public void InitializePool()
    {
        objectQueue = new Queue<GameObject>();

        if (transform.childCount > 0)
        {
            foreach (Transform child in gameObject.transform)
            {
                objectQueue.Enqueue(child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(objectTag);
            foreach(GameObject obj in objects)
            {
                objectQueue.Enqueue(obj);
                obj.SetActive(false);
            }
        }

    }

    public GameObject GetObjectFromQueue()
    {
        return objectQueue.Dequeue();
    }

    public void AddObjectToQueue(GameObject obj)
    {
        objectQueue.Enqueue(obj);        
    }
}
