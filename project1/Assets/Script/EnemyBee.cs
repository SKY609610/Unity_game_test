using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBee : Enemy
{
    public float speed;//�ٶ�
    public float startWaitTime;
    private float waitTime;//�ȴ�ʱ��

    public Transform movePos;//��һ���ƶ�������λ��
    public Transform leftDownPos;//���½ǣ����Χ��
    public Transform rightUpPos;//���Ͻǣ����Χ��

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
