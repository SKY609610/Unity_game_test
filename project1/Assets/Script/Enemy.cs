using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour//�����ࣺ����
{
    public int damage;//�˺�
    public int health;//Ѫ��
    public float flashtime;//����ʱ��

    private SpriteRenderer sr;
    private Color originalColor;//��ɫ

    // Start is called before the first frame update
    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }
    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);//����
        }
    }
    public void TakeDamage(int damage)//����
    {
        health -= damage;
        FlashColor(flashtime);
    }
    void FlashColor(float time)//����
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);

    }
    void ResetColor()//��ɫ
    {
        sr.color = originalColor;
    }
}
