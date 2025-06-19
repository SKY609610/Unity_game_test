using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour//抽象类：敌人
{
    public int damage;//伤害
    public int health;//血量
    public float flashtime;//闪红时间

    private SpriteRenderer sr;
    private Color originalColor;//颜色

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
            Destroy(gameObject);//死亡
        }
    }
    public void TakeDamage(int damage)//受伤
    {
        health -= damage;
        FlashColor(flashtime);
    }
    void FlashColor(float time)//闪红
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);

    }
    void ResetColor()//复色
    {
        sr.color = originalColor;
    }
}
