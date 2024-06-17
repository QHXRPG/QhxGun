using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullectPool : MonoBehaviour
{
    public static BullectPool instance; // 单例模式，方便其它类访问

    public GameObject Object; // 用于生成子弹的预制体
    public Transform parent; // 子弹的父物体，用于组织层次结构

    // 用于存储子弹的对象池
    public Queue<GameObject> objectPool = new Queue<GameObject>();

    // 初始生成的子弹数量
    public int startCount = 16;

    // 对象池中允许的最大子弹数量
    public int maxCount = 25;

    // 在脚本实例化时调用，初始化单例和对象池
    public void Awake()
    {
        instance = this;
        Init();
    }

    // 初始化对象池
    public void Init()
    {
        GameObject obj;
        for (int i = 0; i < startCount; i++)
        {
            // 实例化子弹预制体，并设置其父物体
            obj = Instantiate(Object, this.transform);
            obj.transform.parent = parent;

            // 将子弹添加到对象池中，并设置为不激活状态
            objectPool.Enqueue(obj);
            obj.SetActive(false);
        }
    }

    // 从对象池中获取一个子弹
    public GameObject Get()
    {
        GameObject tmp;
        if (objectPool.Count > 0)
        {
            // 如果对象池中有子弹，则取出一个并激活它
            tmp = objectPool.Dequeue();
            tmp.SetActive(true);
        }
        else
        {
            // 如果对象池为空，则实例化一个新的子弹
            tmp = Instantiate(Object, this.transform);
            tmp.transform.parent = parent;
        }
        return tmp;
    }

    // 将子弹移回对象池
    public void Remove(GameObject obj)
    {
        if (objectPool.Count < maxCount)
        {
            // 如果对象池未达到最大容量，且对象池中不包含该子弹，则将其放回对象池并设置为不激活状态
            if (!objectPool.Contains(obj))
            {
                objectPool.Enqueue(obj);
                obj.SetActive(false);
            }
        }
        else
        {
            // 如果对象池已满，则销毁该子弹
            Destroy(obj);
        }
    }
}
