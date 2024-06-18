using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb; // 玩家的刚体组件
    [HideInInspector] public float moveH, moveV; // 玩家的水平和垂直移动速度

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 获取玩家的刚体组件
    }

    private void Update()
    {
        // 获取玩家的输入，计算出玩家的移动速度
        moveH = Input.GetAxis("Horizontal") * Globalx.moveSpeed;
        moveV = Input.GetAxis("Vertical") * Globalx.moveSpeed;

        // 根据玩家的位置和鼠标的位置，翻转玩家的朝向
        PlayerOverturn();
    }

    private void FixedUpdate()
    {
        // 使用刚体的速度属性，实现玩家的移动
        rb.velocity = new Vector2(moveH, moveV);
    }

    public void PlayerOverturn()
    {
        // 如果玩家的位置在鼠标的左边，则玩家朝向右边
        if (transform.position.x < Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        // 如果玩家的位置在鼠标的右边，则玩家朝向左边
        if (transform.position.x > Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}

