using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakenDamage
{
    [SerializeField] private int maxHp; // 玩家的最大生命值
    public int hp; // 玩家的当前生命值

    [HideInInspector] public bool isAttacked; // 玩家是否正在被攻击
    public bool isAttack { get { return isAttacked; } set { isAttacked = value; } } // isAttack属性，用于获取或设置isAttacked的值

    private Animator anim; // 玩家的动画组件
    static public bool isWin; // 游戏是否胜利

    private void Start()
    {
        anim = GetComponent<Animator>();
        hp = maxHp; // 初始生命值为最大生命值
        isWin = true; // 初始状态为胜利
    }

    // 当玩家受到伤害时调用
    public void TakenDamage(int _amount)
    {
        // 如果玩家没有被攻击
        if (!isAttack) 
        {
            anim.SetTrigger("isHurt"); // 播放受伤动画
            isAttack = true; // 设置玩家为被攻击状态
            StartCoroutine(InvincibleCo()); // 启动无敌时间协程
            BoxCollider2D collider = GetComponent<BoxCollider2D>(); // 获取玩家的碰撞器
            collider.isTrigger = true; // 设置碰撞器为触发器
            hp--; // 生命值减1
            UIManager.instance.UpdateHp(hp); // 更新UI显示的生命值

            // 如果生命值为0或以下
            if (hp <= 0) 
            {
                isWin = false; // 游戏失败
            }
        }
    }

    // 无敌时间协程
    private IEnumerator InvincibleCo()
    {
        yield return new WaitForSeconds(2.0f); // 等待2秒
        BoxCollider2D collider = GetComponent<BoxCollider2D>(); // 获取玩家的碰撞器
        collider.isTrigger = false; // 设置碰撞器为非触发器
        isAttack = false; // 设置玩家为非被攻击状态
    }

    // 当玩家触发其它碰撞器时调用
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果触发的是血包
        if (collision.gameObject.CompareTag("Blood")) 
        {
            if (hp < 3) // 如果生命值小于3
            {
                hp++; // 生命值加1
                UIManager.instance.UpdateHp(hp); // 更新UI显示的生命值
                Destroy(collision.gameObject); // 销毁血液
            }
        }
    }
}
