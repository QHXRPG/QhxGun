using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // 单例模式，方便其它类访问
    public static UIManager instance;

    // 用于显示HP的图像数组
    public Image[] hpImages;

    // 用于表示空HP的图像
    public Sprite blockHpImage;

    // 用于表示满HP的图像
    public Sprite redHpImage;

    // 当前的分数
    public int score;

    // 显示当前分数的UI文本
    public Text textCurrent;

    // 在脚本实例化时调用，初始化单例
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 在脚本启动时调用，初始化分数
    private void Start()
    {
        score = 0;
        UpdateHp(Globalx.PlayHpMax);
    }

    // 更新HP显示
    public void UpdateHp(int _hp)
    {
        for (int i = 0; i < Globalx.PlayHpMax; i++)
        {
            if (i < _hp)
            {
                // 如果当前索引小于HP值，则将图像设置为红色并激活
                hpImages[i].sprite = redHpImage;
                hpImages[i].gameObject.SetActive(true);
            }
            else
            {
                // 如果当前索引大于等于HP值，则将图像设置为空
                hpImages[i].sprite = blockHpImage;
            }
        }
    }

    // 更新分数显示
    public void UpdateScore()
    {
        textCurrent.text = "当前分数：" + score;
    }
}
