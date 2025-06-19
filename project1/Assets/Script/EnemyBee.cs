using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBee : Enemy
{
    public float speed;//速度
    public float startWaitTime;
    private float waitTime;//等待时间

    public Transform movePos;//下一次移动的坐标位置
    public Transform leftDownPos;//左下角（活动范围）
    public Transform rightUpPos;//右上角（活动范围）

    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        waitTime = startWaitTime;
        movePos.position = GetRandomPos();
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();

        transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed*Time.deltaTime);

        if(Vector2.Distance(transform.position,movePos.position)<0.1f)
        {
            if(waitTime <=0)
            {
                movePos.position = GetRandomPos();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    Vector2 GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
        return rndPos;
    }
}
