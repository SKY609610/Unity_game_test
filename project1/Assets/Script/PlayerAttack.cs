using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;//伤害
    public float time;//延迟时间

    private Animator anim;
    private PolygonCollider2D collider2D;
    private bool canAttack = true; // 攻击冷却状态标记
    private float attackCooldown = 0.5f; // 攻击冷却时间（秒）

    IEnumerator IEableHitBox()//延迟协程
    {
        yield return new WaitForSeconds(0.2f);
    }
    IEnumerator AttackCooldown()//冷却协程
    {
        canAttack = false; // 开始冷却，标记为不可攻击
        yield return new WaitForSeconds(attackCooldown); // 等待冷却时间
        canAttack = true; // 冷却结束，恢复攻击能力
    }
    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        collider2D.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        collider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()//攻击
    {
        if (Input.GetButtonDown("Attack") && canAttack)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                collider2D.enabled = true;
                anim.SetTrigger("Attack");
                StartCoroutine(disableHitBox());
                StartCoroutine(IEableHitBox());
                StartCoroutine(AttackCooldown());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
