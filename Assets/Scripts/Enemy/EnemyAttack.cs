using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // 当有其他碰撞体进入触发器时调用
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞体是否是玩家，并且玩家是否不在被攻击状态
        if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<ITakenDamage>().isAttack)
        {
            // 获取玩家的ITakenDamage接口
            ITakenDamage player = collision.gameObject.GetComponent<ITakenDamage>();

            // 对玩家造成伤害，伤害值为1
            player.TakenDamage(1);
        }
    }
}
