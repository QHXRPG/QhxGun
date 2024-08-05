using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakenDamage
{
    [HideInInspector] public bool isAttacked;

    public bool isAttack { get { return isAttacked; } set { isAttacked = value; } }

    private Animator anim;
    static public bool isAlive;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isAlive = true;
    }

    public void TakenDamage(int _amount)
    {
        if (!isAttack)
        {
            anim.SetTrigger("isHurt");
            isAttack = true;
            StartCoroutine(InvincibleCo());
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.isTrigger = true;
            Globalx.PlayHp--;
            UIManager.instance.UpdateHp(Globalx.PlayHp);

            if (Globalx.PlayHp <= 0)
            {
                isAlive = false;
            }
        }
    }

    private IEnumerator InvincibleCo()
    {
        yield return new WaitForSeconds(2.0f);
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = false;
        isAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Blood"))
        {
            if (Globalx.PlayHp < Globalx.PlayHpMax)
            {
                Globalx.PlayHp++;
                UIManager.instance.UpdateHp(Globalx.PlayHp);
                Destroy(collision.gameObject);
            }
        }
    }
}
