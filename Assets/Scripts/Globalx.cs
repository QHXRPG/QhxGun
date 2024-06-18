using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Globalx : MonoBehaviour
{
    // 持有类的唯一实例
    private static Globalx _instance;
    static public int PlayHpMax;
    static public int PlayHp;
    static public float moveSpeed;
    static public float GunSpeed;
    static public float SwordSpeed;

    // 私有构造函数，防止外部实例化
    private Globalx() { }

    // 公共静态方法，提供访问唯一实例的方法
    public static Globalx Instance
    {
        get
        {
            // 如果实例不存在，则创建一个新的实例
            if (_instance == null)
            {
                _instance = new Globalx();
            }
            return _instance;
        }
    }
    private void Start()
    {
        PlayHpMax = 6;
        PlayHp = PlayHpMax;
        moveSpeed = 3;
        GunSpeed = 100.0f;
        SwordSpeed = 6.0f;
    }

    private void Update()
    {
        
    }
}