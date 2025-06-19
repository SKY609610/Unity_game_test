using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public float runSpeed;//�����ٶ�
    public float jumpSpeed;//��Ծ�ٶ�
    public float doubleJumpSpeed;//�������ٶ�

    private Rigidbody2D myRigidbody;//������ײʵ��
    private Animator myAnim;//����
    private BoxCollider2D myFeet;//����ײʵ��
    private bool isGround;//������
    private bool canDoubleJump;//���������
    //private bool canAttack = true; // ������ȴ״̬���
    //private float attackCooldown = 0.5f; // ������ȴʱ�䣨�룩
    //IEnumerator IEableHitBox()//�ӳ�Э��
    //{
    //    yield return new WaitForSeconds(0.2f);
    //}
    //IEnumerator AttackCooldown()//��ȴЭ��
    //{
    //    canAttack = false; // ��ʼ��ȴ�����Ϊ���ɹ���
    //    yield return new WaitForSeconds(attackCooldown); // �ȴ���ȴʱ��
    //    canAttack = true; // ��ȴ�������ָ���������
    //}

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        Run();
        Jump();
        CheckGrounded();
        SwichiAnimation();
        //Attack();
    }

    void CheckGrounded()//������
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        Debug.Log(isGround);
    }
    void Flip()//ת��
    {
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(playerHasXAxisSpeed)
        {
            if(myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    void Run()//�ƶ�
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("Run", playerHasXAxisSpeed);

    }

    void Jump()//��Ծ
    {
        if(Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                myAnim.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
                canDoubleJump = true;
            }
            else
            {
                if(canDoubleJump)//������
                {
                    myAnim.SetBool("DoubleJump", true);
                    Vector2 doubleJumpVel = new Vector2(0.0f, doubleJumpSpeed);
                    myRigidbody.velocity = Vector2.up * doubleJumpVel;
                    canDoubleJump = false;
                }
            }
        }
    }

    //void Attack()
    //{
    //    if (Input.GetButtonDown("Attack") && canAttack)
    //    {
    //        if (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
    //        {
    //            myAnim.SetTrigger("Attack");
    //            StartCoroutine(IEableHitBox());
    //            StartCoroutine(AttackCooldown());
    //        }
    //    }
    //}

    void SwichiAnimation()//����������л�
    {
        myAnim.SetBool("Stop", false);
        if(myAnim.GetBool ("Jump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Stop", true);
        }

        if (myAnim.GetBool("DoubleJump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("DoubleJump", false);
                myAnim.SetBool("DoubleFall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("DoubleFall", false);
            myAnim.SetBool("Stop", true);
        }
    }
}
