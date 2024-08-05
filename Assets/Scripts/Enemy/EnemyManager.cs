using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public float timeOne = 2;
    public float timeGroup = 40;
    public Text timeRefresh;

    private void Start()
    {
        StartCoroutine(CreateEnemy(3f));
    }

    private void Update()
    {

        timeOne -= Time.deltaTime;
        if (timeOne <= 0)
        {
            EnemyPool.instance.Get();
            timeOne = 2;
        }

        timeGroup -= Time.deltaTime;
        if (timeGroup <= 0)
        {
            timeGroup = 40;
            StartCoroutine(CreateEnemy(0f));
        }
        else
        {
            timeRefresh.text = "ÏÂÒ»²¨¹ÖÎïÊ£Óà£º" + (int)timeGroup + "Ãë";
        }
    }

    private IEnumerator CreateEnemy(float _time)
    {
        yield return new WaitForSeconds(_time);

        for (int i = 0; i < EnemyPool.instance.objectPool.Count; i++)
        {
            EnemyPool.instance.Get();
        }
    }
}
