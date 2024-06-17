using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    // 单例模式，方便其它类访问
    public static EnemyPool instance;

    // 用于生成敌人的预制体
    public GameObject Object;

    // 敌人的父物体，用于组织层次结构
    public Transform parent;

    // 用于存储敌人的对象池
    public Queue<GameObject> objectPool = new Queue<GameObject>();

    // 初始生成的敌人数量
    public int startCount = 20;

    // 对象池中允许的最大敌人数量
    public int maxCount = 100;

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
            // 实例化敌人预制体，并设置其父物体
            obj = Instantiate(Object, this.transform);
            obj.transform.parent = parent;
            // 将敌人添加到对象池中，并设置为不激活状态
            objectPool.Enqueue(obj);
            obj.SetActive(false);
        }
    }

    // 从对象池中获取一个敌人
    public GameObject Get()
    {
        GameObject tmp;
        if (objectPool.Count > 0)
        {
            // 如果对象池中有敌人，则取出一个并激活它
            tmp = objectPool.Dequeue();
            tmp.SetActive(true);
        }
        else
        {
            // 如果对象池为空，则实例化一个新的敌人
            tmp = Instantiate(Object, this.transform);
            tmp.transform.parent = parent;
        }
        return tmp;
    }

    // 将敌人移回对象池
    public void Remove(GameObject obj)
    {
        if (objectPool.Count < maxCount)
        {
            // 如果对象池未达到最大容量，且对象池中不包含该敌人，则将其放回对象池并设置为不激活状态
            if (!objectPool.Contains(obj))
            {
                objectPool.Enqueue(obj);
                obj.SetActive(false);
            }
        }
        else
        {
            // 如果对象池已满，则销毁该敌人
            Destroy(obj);
        }
    }
}
