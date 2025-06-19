using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public float runSpeed;//奔跑速度
    public float jumpSpeed;//跳跃速度
    public float doubleJumpSpeed;//二段跳速度

    private Rigidbody2D myRigidbody;//人物碰撞实体
    private Animator myAnim;//动画
    private BoxCollider2D myFeet;//脚碰撞实体
    private bool isGround;//地面标记
    private bool canDoubleJump;//二段跳标记
    //private bool canAttack = true; // 攻击冷却状态标记
    //private float attackCooldown = 0.5f; // 攻击冷却时间（秒）
    //IEnumerator IEableHitBox()//延迟协程
    //{
    //    yield return new WaitForSeconds(0.2f);
    //}
    //IEnumerator AttackCooldown()//冷却协程
    //{
    //    canAttack = false; // 开始冷却，标记为不可攻击
    //    yield return new WaitForSeconds(attackCooldown); // 等待冷却时间
    //    canAttack = true; // 冷却结束，恢复攻击能力
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

    void CheckGrounded()//检测地面
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        Debug.Log(isGround);
    }
    void Flip()//转向
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
    void Run()//移动
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("Run", playerHasXAxisSpeed);

    }

    void Jump()//跳跃
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
                if(canDoubleJump)//二段跳
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

    void SwichiAnimation()//人物各动画切换
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
