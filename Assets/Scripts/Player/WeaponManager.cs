using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponManager : MonoBehaviour
{
    public Sprite[] weaponSprite; // 存储武器的Sprite数组
    private SpriteRenderer sp; // 武器的SpriteRenderer组件
    public bool isSword; // 是否是剑
    private Animator anim; // 武器的动画组件

    public GameObject[] weaponObj; // 存储武器对象的数组

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>(); // 获取武器的SpriteRenderer组件
        anim = GetComponent<Animator>(); // 获取武器的动画组件
    }

    private void Update()
    {
        // 检测Q键按下
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("切换武器"); // 输出调试信息

            // 如果当前武器是剑
            if (!isSword)
            {
                sp.sprite = weaponSprite[0]; // 切换武器Sprite
                isSword = true; // 设置为剑
                weaponObj[0].transform.localScale = new Vector3(0.6f, 0.6f, 0.6f); // 调整剑的大小
                weaponObj[1].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // 调整枪的大小
            }
            else // 如果当前武器是枪
            {
                sp.sprite = weaponSprite[1]; // 切换武器Sprite
                isSword = false; // 设置为枪
                weaponObj[0].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // 调整剑的大小
                weaponObj[1].transform.localScale = new Vector3(0.6f, 0.6f, 0.6f); // 调整枪的大小
            };
        }
    }

    // 结束剑攻击动画
    public void EndSword()
    {
        anim.enabled = false; // 停止动画
    }
}

