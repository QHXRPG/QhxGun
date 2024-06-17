using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMapManager : MonoBehaviour
{
    static public int userLevelAdvance;
    public Level level;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("userLevelAdvance"))
            userLevelAdvance = PlayerPrefs.GetInt("userLevelAdvance");
        else
            userLevelAdvance = 0;
    }
    private void Update()
    {
        RayCollsion();
    }
    public void RayCollsion()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if(hit.collider != null)
            {
                if (hit.collider.CompareTag("Level"))
                {
                    level = hit.collider.gameObject.GetComponent<Level>();
                    PlayerPrefs.SetInt("TargetScore", level.targetScore);
                    PlayerPrefs.SetInt("currentLevelID", level.LevelID);
                    SceneManager.LoadScene("Game");
                }
            }
        }
    }
}
