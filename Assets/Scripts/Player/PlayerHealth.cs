using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour , ITakenDamage
{
    [SerializeField] private int maxHp;
    public int hp;

    [HideInInspector] public bool isAttacked;
    public bool isAttack { get { return isAttacked; } set { isAttacked = value; } }

    private Animator anim;
    static public bool isWin;
    private void Start()
    {
        anim = GetComponent<Animator>();
        hp = maxHp;
        isWin = true;
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
            hp--;
            UIManager.instance.UpdateHp(hp);

            if (hp <= 0)
            {
                isWin = false;
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
            if (hp < 3)
            {
                hp++;
                UIManager.instance.UpdateHp(hp);
                Destroy(collision.gameObject);
            }
        }
    }
}
