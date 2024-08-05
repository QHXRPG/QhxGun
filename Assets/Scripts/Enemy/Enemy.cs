using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
public class Enemy : MonoBehaviour, ITakenDamage
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private Transform target;
    [SerializeField] private int maxHp;
    public int hp;

    [HideInInspector] public bool isAttacked;
    public bool isAttack { get { return isAttacked; } set { isAttacked = value; } }

    public GameObject bloodPrefab;


    private void OnEnable()
    {
        hp = maxHp;
        isAttack = false;


        float rangeX = Random.Range(-6.3f, 8.7f);
        float rangeY = Random.Range(-3.2f, 2.6f);
        transform.position = new Vector3(rangeX, rangeY);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        hp = maxHp;
    }


    private void Update()
    {
        FollowPlayer();
        EnemyOverturn();
    }


    public void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }


    public void EnemyOverturn()
    {
        if (transform.position.x < target.transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (transform.position.x > target.transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }


    public void TakenDamage(int _amount)
    {
        hp -= _amount;


        if (hp <= 0)
        {
            float dropChance = 0.1f;
            float randomValue = Random.value;
            if (randomValue <= dropChance)
            {
                Instantiate(bloodPrefab, transform.position, Quaternion.identity);
            }
            EnemyPool.instance.Remove(this.gameObject);
            UIManager.instance.score++;
            UIManager.instance.UpdateScore();
        }
    }

    private IEnumerator IsAttackCo()
    {
        yield return new WaitForSeconds(0.2f);
        isAttack = false;
    }
}
