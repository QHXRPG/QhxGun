﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image[] hpImages;

    public Sprite blockHpImage;

    public Sprite redHpImage;

    public int score;

    public Text textCurrent;

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
    private void Start()
    {
        score = 0;
        UpdateHp(Globalx.PlayHpMax);
    }

    public void UpdateHp(int _hp)
    {
        for (int i = 0; i < Globalx.PlayHpMax; i++)
        {
            if (i < _hp)
            {
                hpImages[i].sprite = redHpImage;
                hpImages[i].gameObject.SetActive(true);
            }
            else
            {
                hpImages[i].sprite = blockHpImage;
            }
        }
    }

    public void UpdateScore()
    {
        textCurrent.text = "µ±Ç°·ÖÊý£º" + score;
    }
}
