using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// DamageNum 类的主要功能是在启动时设置销毁时间，使伤害数字在屏幕上缓慢上升，并显示传入的伤害数值。
/// </summary>
public class DamageNum : MonoBehaviour
{
    public Text damageText; // 显示伤害数值的文本组件
    public float lifeTime; // 伤害数字的生存时间
    public float moveSpeed; // 伤害数字的移动速度

    // 在脚本启动时调用
    private void Start()
    {
        // 在指定的生存时间后销毁该对象
        Destroy(gameObject, lifeTime);
    }

    // 每帧更新时调用
    private void Update()
    {
        // 使伤害数字在Y轴上移动
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }

    // 显示伤害数值
    public void ShowDamage(int _amount)
    {
        // 设置文本组件显示的文字为传入的伤害数值
        damageText.text = _amount.ToString();
    }
}
