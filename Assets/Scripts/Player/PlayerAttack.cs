using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{

    private Coroutine attackRoutine;

    public float rotZ;
    public GameObject Weapon;
    private SpriteRenderer sp;
    private Animator anim;
    public bool isSwordAttack;

    public AudioClip bulletClip, swordClip;

    private void Start()
    {
        sp = Weapon.GetComponent<SpriteRenderer>();
        anim = transform.GetChild(0).GetComponent<Animator>();

        isSwordAttack = true;
    }

    private void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (attackRoutine == null)
            {
                attackRoutine = StartCoroutine(AttackCoroutine());
            }
        }
        else
        {
            if (attackRoutine != null)
            {
                StopCoroutine(attackRoutine);
                attackRoutine = null;
            }
        }
        HandWeapon();
    }

    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            Attack();

            float waitTime = 1.0f / Globalx.GunSpeed;
            Debug.Log("waitTime:" + waitTime + " GunSpeed:" + Globalx.GunSpeed);
            yield return new WaitForSeconds(waitTime);
        }
    }


    public void Attack()
    {
        WeaponManager weaponManager = Weapon.GetComponent<WeaponManager>();

        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (sp.sprite.name == weaponManager.weaponSprite[0].name && isSwordAttack)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            anim.enabled = true;
            anim.speed = Globalx.SwordSpeed;
            playSfx(swordClip);
        }

        if (sp.sprite.name == weaponManager.weaponSprite[1].name)
        {
            BullectPool.instance.Get();
            playSfx(bulletClip);
        }
    }

    public void HandWeapon()
    {
        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float HandWeaponRotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Weapon.transform.rotation = Quaternion.Euler(0, 0, HandWeaponRotZ - 45);
    }

    public void playSfx(AudioClip _clip)
    {
        StartCoroutine(PlaySfxCoroutine(_clip, 0.3f));
    }

    private IEnumerator PlaySfxCoroutine(AudioClip clip, float duration)
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
            yield return new WaitForSeconds(duration);
            audioSource.Stop();
        }
        else
        {
            Debug.LogWarning("AudioSource component missing on the GameObject.");
        }
    }
    public void SetAttackSpeed(float newAttackSpeed)
    {
        Globalx.GunSpeed = newAttackSpeed;
    }
}

