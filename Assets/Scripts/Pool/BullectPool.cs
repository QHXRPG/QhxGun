using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullectPool : MonoBehaviour
{
    public static BullectPool instance;

    public GameObject Object;
    public Transform parent;

    public Queue<GameObject> objectPool = new Queue<GameObject>();
    public int startCount = 16;
    public int maxCount = 25;

    public void Awake()
    {
        instance = this;
        Init();
    }
    public void Init()
    {
        GameObject obj;
        for (int i = 0; i < startCount; i++)
        {
            obj = Instantiate(Object, this.transform);
            obj.transform.parent = parent;
            objectPool.Enqueue(obj);
            obj.SetActive(false);
        }
    }
    public GameObject Get()
    {
        GameObject tmp;
        if(objectPool.Count > 0)
        {
            tmp = objectPool.Dequeue();
            tmp.SetActive(true);
        }
        else
        {
            tmp = Instantiate(Object, this.transform);
            tmp.transform.parent = parent;
        }
        return tmp;
    }
    public void Remove(GameObject obj)
    {
        if(objectPool.Count < maxCount)
        {
            if (!objectPool.Contains(obj))
            {
                objectPool.Enqueue(obj);
                obj.SetActive(false);
            }
        }
        else
        {
            Destroy(obj);
        }
    }
}
