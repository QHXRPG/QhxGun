using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// </summary>
public class Effect : MonoBehaviour
{
    public float bullectSpeed;
    private Transform player;
    private PlayerAttack playerAttack;

    [SerializeField] private int minAttack, maxAttack;
    public int attackDamage;

    public GameObject damageTextPrefab;
    public GameObject bulletPrefab;


    private void OnEnable()
    {
        StartCoroutine(DestoryBullect());


        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


        this.transform.position = player.transform.Find("Weapon").position;


        playerAttack = player.GetComponent<PlayerAttack>();
        transform.rotation = Quaternion.Euler(0, 0, playerAttack.rotZ);
    }

    private void Update()
    {

        transform.Translate(transform.right * bullectSpeed * Time.deltaTime, Space.World);
    }


    private IEnumerator DestoryBullect()
    {
        yield return new WaitForSeconds(5f);
        BullectPool.instance.Remove(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {

            attackDamage = Random.Range(minAttack, maxAttack);


            ITakenDamage enemy = collision.GetComponent<ITakenDamage>();


            enemy.TakenDamage(attackDamage);


            BullectPool.instance.Remove(this.gameObject);


            DamageNum damageNum = Instantiate(damageTextPrefab, collision.gameObject.transform.position, Quaternion.identity).GetComponent<DamageNum>();
            damageNum.ShowDamage(attackDamage);


            Instantiate(bulletPrefab, collision.gameObject.transform.position, Quaternion.identity);
        }
    }
}
