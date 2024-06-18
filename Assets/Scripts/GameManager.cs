using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject loseObj; // 失败对象
    public GameObject player; // 玩家对象
    public GameObject enemyManager; // 敌人管理对象

    public int targetScore; // 目标分数

    public Text targetText; // 显示目标分数的文本

    private void Awake()
    {
        targetScore = 9999;
    }

    private void Start()
    {
        // 在游戏开始时设置目标分数的文本
        targetText.text = "目标分数：" + targetScore.ToString();
        // 初始化时隐藏胜利和失败对象
        loseObj.SetActive(false);
    }

    private void Update()
    {
        int score = UIManager.instance.score; // 获取当前得分
        // 如果玩家已死亡，处理游戏失败逻辑
        if (!PlayerHealth.isAlive)
        {
            Debug.Log("游戏失败！");
            loseObj.SetActive(true); // 显示失败对象
            player.SetActive(false); // 隐藏玩家对象
            enemyManager.SetActive(false); // 停止敌人管理
        }
    }

    // 重启当前游戏场景
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
