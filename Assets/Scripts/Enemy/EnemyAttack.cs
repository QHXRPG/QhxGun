using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<ITakenDamage>().isAttack)
        {
            ITakenDamage player = collision.gameObject.GetComponent<ITakenDamage>();
            player.TakenDamage(1);
        }
    }
}
