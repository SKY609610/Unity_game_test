using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;//�˺�
    public float time;//�ӳ�ʱ��

    private Animator anim;
    private PolygonCollider2D collider2D;
    private bool canAttack = true; // ������ȴ״̬���
    private float attackCooldown = 0.5f; // ������ȴʱ�䣨�룩

    IEnumerator IEableHitBox()//�ӳ�Э��
    {
        yield return new WaitForSeconds(0.2f);
    }
    IEnumerator AttackCooldown()//��ȴЭ��
    {
        canAttack = false; // ��ʼ��ȴ�����Ϊ���ɹ���
        yield return new WaitForSeconds(attackCooldown); // �ȴ���ȴʱ��
        canAttack = true; // ��ȴ�������ָ���������
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

    void Attack()//����
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
