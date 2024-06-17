using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{ 
    // 单个敌人生成的间隔时间
    public float timeOne = 2;
    // 一组敌人生成的间隔时间
    public float timeGroup = 40;
    // 显示剩余时间的UI文本
    public Text timeRefresh;

    private void Start()
    {
        // 开始时立即调用生成敌人的协程，延迟3秒
        StartCoroutine(CreateEnemy(3f));
    }

    private void Update()
    {
        // 更新单个敌人生成的倒计时
        timeOne -= Time.deltaTime;
        if (timeOne <= 0)
        {
            // 当倒计时结束，从敌人池中获取一个敌人
            EnemyPool.instance.Get();
            // 重置倒计时时间
            timeOne = 2;
        }

        // 更新一组敌人生成的倒计时
        timeGroup -= Time.deltaTime;
        if (timeGroup <= 0)
        {
            // 当倒计时结束，重置倒计时时间并调用生成敌人的协程
            timeGroup = 40;
            StartCoroutine(CreateEnemy(0f));
        }
        else
        {
            // 更新UI文本，显示下一波敌人生成的剩余时间
            timeRefresh.text = "下一波怪物剩余：" + (int)timeGroup + "秒";
        }
    }

    // 协程用于生成一组敌人
    private IEnumerator CreateEnemy(float _time)
    {
        // 等待指定的时间
        yield return new WaitForSeconds(_time);

        // 遍历敌人池中的所有敌人，并从池中获取它们
        for (int i = 0; i < EnemyPool.instance.objectPool.Count; i++)
        {
            EnemyPool.instance.Get();
        }
    }
}
