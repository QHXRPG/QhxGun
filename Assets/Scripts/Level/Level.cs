using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public int LevelID;
    public int targetScore;
    private void Start()
    {
        if (LevelMapManager.userLevelAdvance >= LevelID - 1)
        {
            //this level is open
            GetComponent<BoxCollider2D>().enabled = true;
            transform.GetChild(0).GetComponent<Text>().text = LevelID.ToString();

            //set heartbeat animation to active, if this is the newest opened level.
            if (LevelMapManager.userLevelAdvance == LevelID - 1)
                GetComponent<HeartBeatAnimationEffect>().enabled = true;

        }
        else
        {
            //level is locked
            GetComponent<BoxCollider2D>().enabled = false;
            transform.GetChild(0).GetComponent<Text>().text = "x";

            //set heartbeat animation to inactive
            GetComponent<HeartBeatAnimationEffect>().enabled = false;
        }
    }
}
