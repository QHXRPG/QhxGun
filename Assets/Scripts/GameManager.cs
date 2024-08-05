using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject loseObj;
    public GameObject player;
    public GameObject enemyManager;

    public int targetScore;

    public Text targetText;

    private void Awake()
    {
        targetScore = 9999;
    }

    private void Start()
    {
        targetText.text = "Ä¿±ê·ÖÊý£º" + targetScore.ToString();
        loseObj.SetActive(false);
    }

    private void Update()
    {
        int score = UIManager.instance.score;
        if (!PlayerHealth.isAlive)
        {
            Debug.Log("ÓÎÏ·Ê§°Ü£¡");
            loseObj.SetActive(true);
            player.SetActive(false);
            enemyManager.SetActive(false);
        }
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
