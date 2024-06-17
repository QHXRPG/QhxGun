using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    public float rotZ; // 武器的旋转角度
    public GameObject Weapon; // 武器对象
    private SpriteRenderer sp; // 武器的SpriteRenderer组件
    private Animator anim; // 玩家手臂的动画组件
    public bool isSwordAttack; // 是否正在进行剑攻击

    public AudioClip bulletClip, swordClip; // 子弹和剑的音效

    private void Start()
    {
        sp = Weapon.GetComponent<SpriteRenderer>(); // 获取武器的SpriteRenderer组件
        anim = transform.GetChild(0).GetComponent<Animator>(); // 获取玩家手臂的动画组件

        isSwordAttack = true; // 初始状态为可以进行剑攻击
    }

    private void Update()
    {
        Attack(); // 检测和处理攻击输入
        HandWeapon(); // 处理武器的朝向
    }

    // 处理攻击逻辑
    public void Attack()
    {
        if (Input.GetMouseButtonDown(0)) // 检测鼠标左键按下
        {
            WeaponManager weaponManager = Weapon.GetComponent<WeaponManager>(); // 获取武器管理器组件

            // 计算鼠标位置与玩家位置的差向量
            Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            // 计算武器的旋转角度
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; 

            // 如果当前武器是剑且可以进行剑攻击
            if (sp.sprite.name == weaponManager.weaponSprite[0].name && isSwordAttack)
            {
                transform.GetChild(1).gameObject.SetActive(true); // 激活剑攻击对象
                isSwordAttack = false; // 设置为不可以进行剑攻击
                anim.enabled = true; // 启用动画
                playSfx(swordClip); // 播放剑攻击音效
            }

            // 如果当前武器是枪
            if (sp.sprite.name == weaponManager.weaponSprite[1].name)
            {
                BullectPool.instance.Get(); // 从子弹池中获取子弹
                playSfx(bulletClip); // 播放子弹音效
            }
        }
    }

    // 处理武器的朝向
    public void HandWeapon()
    {
        // 计算鼠标位置与玩家位置的差向量
        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; 
        float HandWeaponRotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // 计算武器的旋转角度
        Weapon.transform.rotation = Quaternion.Euler(0, 0, HandWeaponRotZ - 45); // 设置武器的旋转角度
    }

    // 播放音效
    public void playSfx(AudioClip _clip)
    {
        GetComponent<AudioSource>().clip = _clip; // 设置音效剪辑
        GetComponent<AudioSource>().Play(); // 播放音效
    }
}

