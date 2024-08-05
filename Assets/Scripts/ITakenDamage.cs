using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// </summary>
public interface ITakenDamage
{
    bool isAttack { get; set; }

    void TakenDamage(int _amount);
}
