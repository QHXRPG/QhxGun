using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    // µ¥ÀýÄ£Ê½£¬·½±ãÆäËüÀà·ÃÎÊ
    public static EnemyPool instance;

    // ÓÃÓÚÉú³ÉµÐÈËµÄÔ¤ÖÆÌå
    public GameObject Object;

    // µÐÈËµÄ¸¸ÎïÌå£¬ÓÃÓÚ×éÖ¯²ã´Î½á¹¹
    public Transform parent;

    // ÓÃÓÚ´æ´¢µÐÈËµÄ¶ÔÏó³Ø
    public Queue<GameObject> objectPool = new Queue<GameObject>();

    // ³õÊ¼Éú³ÉµÄµÐÈËÊýÁ¿
    public int startCount = 20;

    // ¶ÔÏó³ØÖÐÔÊÐíµÄ×î´óµÐÈËÊýÁ¿
    public int maxCount = 100;

    // ÔÚ½Å±¾ÊµÀý»¯Ê±µ÷ÓÃ£¬³õÊ¼»¯µ¥ÀýºÍ¶ÔÏó³Ø
    public void Awake()
    {
        instance = this;
        Init();
    }

    // ³õÊ¼»¯¶ÔÏó³Ø
    public void Init()
    {
        GameObject obj;
        for (int i = 0; i < startCount; i++)
        {
            // ÊµÀý»¯µÐÈËÔ¤ÖÆÌå£¬²¢ÉèÖÃÆä¸¸ÎïÌå
            obj = Instantiate(Object, this.transform);
            obj.transform.parent = parent;
            // ½«µÐÈËÌí¼Óµ½¶ÔÏó³ØÖÐ£¬²¢ÉèÖÃÎª²»¼¤»î×´Ì¬
            objectPool.Enqueue(obj);
            obj.SetActive(false);
        }
    }

    // ´Ó¶ÔÏó³ØÖÐ»ñÈ¡Ò»¸öµÐÈË
    public GameObject Get()
    {
        GameObject tmp;
        if (objectPool.Count > 0)
        {
            // Èç¹û¶ÔÏó³ØÖÐÓÐµÐÈË£¬ÔòÈ¡³öÒ»¸ö²¢¼¤»îËü
            tmp = objectPool.Dequeue();
            tmp.SetActive(true);
        }
        else
        {
            // Èç¹û¶ÔÏó³ØÎª¿Õ£¬ÔòÊµÀý»¯Ò»¸öÐÂµÄµÐÈË
            tmp = Instantiate(Object, this.transform);
            tmp.transform.parent = parent;
        }
        return tmp;
    }

    // ½«µÐÈËÒÆ»Ø¶ÔÏó³Ø
    public void Remove(GameObject obj)
    {
        if (objectPool.Count < maxCount)
        {
            // Èç¹û¶ÔÏó³ØÎ´´ïµ½×î´óÈÝÁ¿£¬ÇÒ¶ÔÏó³ØÖÐ²»°üº¬¸ÃµÐÈË£¬Ôò½«Æä·Å»Ø¶ÔÏó³Ø²¢ÉèÖÃÎª²»¼¤»î×´Ì¬
            if (!objectPool.Contains(obj))
            {
                objectPool.Enqueue(obj);
                obj.SetActive(false);
            }
        }
        else
        {
            // Èç¹û¶ÔÏó³ØÒÑÂú£¬ÔòÏú»Ù¸ÃµÐÈË
            Destroy(obj);
        }
    }
}
