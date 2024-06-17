using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
public class Enemy : MonoBehaviour, ITakenDamage
{
    private Rigidbody2D rb; // 敌人的刚体组件
    [SerializeField] private float moveSpeed; // 敌人的移动速度
    private Transform target; // 目标（玩家）的Transform组件
    [SerializeField] private int maxHp; // 敌人的最大生命值
    public int hp; // 敌人的当前生命值

    [HideInInspector] public bool isAttacked; // 敌人是否正在被攻击
    public bool isAttack { get { return isAttacked; } set { isAttacked = value; } } // isAttack属性，用于获取或设置isAttacked的值

    public GameObject bloodPrefab; // 血液预制体

    // 当敌人启用时调用
    private void OnEnable()
    {
        hp = maxHp; // 重置敌人的生命值
        isAttack = false; // 重置敌人的攻击状态

        // 随机生成敌人的位置
        float rangeX = Random.Range(-6.3f, 8.7f);
        float rangeY = Random.Range(-3.2f, 2.6f);
        transform.position = new Vector3(rangeX, rangeY);
    }

    // 在脚本启动时调用
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 获取敌人的刚体组件
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); // 获取目标（玩家）的Transform组件
        hp = maxHp; // 初始化敌人的生命值
    }

    // 在每帧更新时调用
    private void Update()
    {
        FollowPlayer(); // 敌人跟随玩家
        EnemyOverturn(); // 敌人根据玩家位置翻转
    }

    // 敌人跟随玩家
    public void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }

    // 敌人根据玩家位置翻转
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

    // 敌人受到伤害时调用
    public void TakenDamage(int _amount)
    {
        isAttack = true; // 设置敌人正在被攻击状态
        StartCoroutine(IsAttackCo()); // 启动被攻击协程
        hp -= _amount; // 减少敌人的生命值

        // 如果敌人的生命值小于等于0
        if (hp <= 0)
        {
            float dropChance = 0.1f; // 掉落血液的概率
            float randomValue = Random.value; // 生成一个随机值
            if (randomValue <= dropChance)
            {
                Instantiate(bloodPrefab, transform.position, Quaternion.identity); // 掉落血液
            }
            EnemyPool.instance.Remove(this.gameObject); // 将敌人移回对象池
            UIManager.instance.score++; // 增加分数
            UIManager.instance.UpdateScore(); // 更新分数显示
        }
    }

    // 被攻击协程
    private IEnumerator IsAttackCo()
    {
        yield return new WaitForSeconds(0.2f); // 等待0.2秒
        isAttack = false; // 设置敌人不再被攻击状态
    }
}
