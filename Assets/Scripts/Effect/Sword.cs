using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// </summary>
public class Sword : MonoBehaviour
{
    [SerializeField] private int minAttack, maxAttack;
    public int attackDamage;

    public GameObject damageTextPrefab;

    private PlayerAttack playerAttack;
    public GameObject boomPrefab;


    private void OnEnable()
    {

        playerAttack = GetComponentInParent<PlayerAttack>();

        transform.rotation = Quaternion.Euler(0, 0, playerAttack.rotZ);
    }


    public void EndAttack()
    {
        gameObject.SetActive(false);
        playerAttack.isSwordAttack = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {

            attackDamage = Random.Range(minAttack, maxAttack);


            ITakenDamage enemy = collision.GetComponent<ITakenDamage>();


            enemy.TakenDamage(attackDamage);


            DamageNum damageNum = Instantiate(damageTextPrefab, collision.gameObject.transform.position, Quaternion.identity).GetComponent<DamageNum>();
            damageNum.ShowDamage(attackDamage);


            Vector2 difference = collision.transform.position - transform.position;
            difference.Normalize();


            collision.transform.position = new Vector2(collision.transform.position.x + difference.x / 2,
                                                        collision.transform.position.y + difference.y / 2);


            Instantiate(boomPrefab, collision.gameObject.transform.position, Quaternion.identity);
        }
    }
}

