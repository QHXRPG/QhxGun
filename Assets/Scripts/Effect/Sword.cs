using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sword 类的主要功能是在剑启用时初始化其旋转角度，
/// 在碰撞到敌人时对敌人造成伤害并显示伤害数值，
/// 同时生成爆炸效果并稍微击退敌人
/// </summary>
public class Sword : MonoBehaviour
{
    [SerializeField] private int minAttack, maxAttack; // 最小和最大攻击力
    public int attackDamage; // 当前攻击力

    public GameObject damageTextPrefab; // 伤害文本预制体

    private PlayerAttack playerAttack; // 玩家攻击组件
    public GameObject boomPrefab; // 爆炸效果预制体

    // 当对象启用时调用
    private void OnEnable()
    {
        // 获取父物体的PlayerAttack组件
        playerAttack = GetComponentInParent<PlayerAttack>();
        // 设置剑的旋转角度为玩家攻击的旋转角度
        transform.rotation = Quaternion.Euler(0, 0, playerAttack.rotZ);
    }

    // 结束攻击时调用
    public void EndAttack()
    {
        gameObject.SetActive(false); // 将剑对象设置为不激活状态
        playerAttack.isSwordAttack = true; // 重置玩家的剑攻击状态
    }

    // 当剑触发碰撞时调用
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果碰撞对象是敌人
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 随机生成攻击力
            attackDamage = Random.Range(minAttack, maxAttack);

            // 获取敌人的ITakenDamage接口
            ITakenDamage enemy = collision.GetComponent<ITakenDamage>();

            // 对敌人造成伤害
            enemy.TakenDamage(attackDamage);

            // 显示伤害数值
            DamageNum damageNum = Instantiate(damageTextPrefab, collision.gameObject.transform.position, Quaternion.identity).GetComponent<DamageNum>();
            damageNum.ShowDamage(attackDamage);

            // 计算敌人被击退的方向
            Vector2 difference = collision.transform.position - transform.position;
            difference.Normalize();

            // 将敌人位置稍微移动，模拟击退效果
            collision.transform.position = new Vector2(collision.transform.position.x + difference.x / 2,
                                                        collision.transform.position.y + difference.y / 2);

            // 在敌人位置生成爆炸效果
            Instantiate(boomPrefab, collision.gameObject.transform.position, Quaternion.identity);
        }
    }
}

