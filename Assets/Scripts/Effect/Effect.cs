using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Effect 类的主要功能是在子弹启用时初始化其位置和旋转角度，
/// 更新其位置以实现移动，并在与敌人碰撞时对敌人造成伤害并显示伤害数值。
/// </summary>
public class Effect : MonoBehaviour
{
    public float bullectSpeed; // 子弹的速度
    private Transform player; // 玩家Transform组件
    private PlayerAttack playerAttack; // 玩家攻击组件

    [SerializeField] private int minAttack, maxAttack; // 最小和最大攻击力
    public int attackDamage; // 当前攻击力

    public GameObject damageTextPrefab; // 伤害文本预制体
    public GameObject bulletPrefab; // 子弹预制体

    // 当对象启用时调用
    private void OnEnable()
    {
        StartCoroutine(DestoryBullect()); // 启动销毁子弹的协程

        // 获取玩家的Transform组件
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // 将子弹位置设置为玩家武器的位置
        this.transform.position = player.transform.Find("Weapon").position;

        // 获取玩家攻击组件并设置子弹的旋转角度
        playerAttack = player.GetComponent<PlayerAttack>();
        transform.rotation = Quaternion.Euler(0, 0, playerAttack.rotZ);
    }

    // 每帧更新时调用
    private void Update()
    {
        // 移动子弹
        transform.Translate(transform.right * bullectSpeed * Time.deltaTime, Space.World);
    }

    // 销毁子弹的协程
    private IEnumerator DestoryBullect()
    {
        yield return new WaitForSeconds(5f); // 等待5秒后销毁子弹
        BullectPool.instance.Remove(this.gameObject); // 将子弹移回对象池
    }

    // 当子弹触发碰撞时调用
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果碰撞的对象是敌人
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 随机生成攻击力
            attackDamage = Random.Range(minAttack, maxAttack);

            // 获取敌人的ITakenDamage接口
            ITakenDamage enemy = collision.GetComponent<ITakenDamage>();
            
            // 对敌人造成伤害
            enemy.TakenDamage(attackDamage);

            // 将子弹移回对象池
            BullectPool.instance.Remove(this.gameObject);

            // 显示伤害数值
            DamageNum damageNum = Instantiate(damageTextPrefab, collision.gameObject.transform.position, Quaternion.identity).GetComponent<DamageNum>();
            damageNum.ShowDamage(attackDamage);

            // 在敌人位置生成子弹效果
            Instantiate(bulletPrefab, collision.gameObject.transform.position, Quaternion.identity);
        }
    }
}
