using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ITakenDamage 是一个接口，它定义了所有可以受到伤害的对象（如玩家和敌人）应该实现的行为。
/// 具体来说，它有一个 isAttack 属性，用于表示对象是否正在被攻击，
/// 以及一个 TakenDamage 方法，用于处理对象受到的伤害。
/// </summary>
public interface ITakenDamage
{
    // 定义一个bool类型的属性，用于表示是否正在被攻击
    bool isAttack { get; set; }

    // 定义一个方法，用于处理受到的伤害，参数为伤害值
    void TakenDamage(int _amount);
}
