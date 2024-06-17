using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
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
        Attack();
        HandWeapon();
    }
    public void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            WeaponManager weaponManager = Weapon.GetComponent<WeaponManager>();
            Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            if (sp.sprite.name == weaponManager.weaponSprite[0].name && isSwordAttack)
            {
                transform.GetChild(1).gameObject.SetActive(true);
                isSwordAttack = false;
                anim.enabled = true;
                playSfx(swordClip);
            }
            if(sp.sprite.name == weaponManager.weaponSprite[1].name)
            {
                BullectPool.instance.Get();
                playSfx(bulletClip);
            }
        }
    }
    public void HandWeapon()
    {
        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float HandWeaponRotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Weapon.transform.rotation = Quaternion.Euler(0, 0, HandWeaponRotZ-45);
    }
    public void playSfx(AudioClip _clip)
    {

        GetComponent<AudioSource>().clip = _clip;
        GetComponent<AudioSource>().Play();

    }
}
