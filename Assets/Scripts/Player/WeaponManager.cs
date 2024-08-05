using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponManager : MonoBehaviour
{
    public Sprite[] weaponSprite;
    private SpriteRenderer sp;
    public bool isSword;
    private Animator anim;

    public GameObject[] weaponObj;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("ÇÐ»»ÎäÆ÷");


            if (!isSword)
            {
                sp.sprite = weaponSprite[0];
                isSword = true;
                weaponObj[0].transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                weaponObj[1].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else
            {
                sp.sprite = weaponSprite[1];
                isSword = false;
                weaponObj[0].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                weaponObj[1].transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            };
        }
    }
    public void EndSword()
    {
        anim.enabled = false;
    }
}

