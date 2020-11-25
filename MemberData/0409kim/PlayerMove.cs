using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤ挙動
/// </summary>
public class PlayerMove : MonoBehaviour
{
    /// <summary>
    /// フィールド
    /// </summary>

    //-----------------------------------------------
    [Header("動き")]
    [SerializeField] float moveSpeed = 10f;     //移動速度
    [SerializeField] float airMoveSpeed = 30f;  //空中移動速度
    [SerializeField] float xDirectionalInput;   //X軸(横)方向値
    [SerializeField] bool isFacingRight = true; //右方向向き判定
    [SerializeField] bool isMoving = false;     //動き判定

    [Header("ジャンプ")]
    [SerializeField] float jumpForce = 15f;         //ジャンプ力
    [SerializeField] bool isCanJump;                //ジャンプ可能の判定
    [SerializeField] bool isJump;                   //ジャンプ中なのか判定

    [Header("壁すり")]
    [SerializeField] float wallSlideSpeed = 2f;         //壁すりの速度
    [SerializeField] float wallSlideTime;               //現在壁すりの時間
    [SerializeField] float defaultWallSlideTime = 2f;   //壁すりの時間
    [SerializeField] LayerMask wallLayer = default;     //壁レイヤ
    [SerializeField] Transform wallCheckPoint = default;//壁接触チェックのためオブジェクトの位置
    [SerializeField] Vector2 wallCheckSize = default;   //上記オブジェクトのサイズ
    [SerializeField] bool isTouchingWall;               //壁接触判定
    [SerializeField] bool isWallSliding;                //壁すり状態の判定


    [Header("壁ジャンプ")]
    [SerializeField] float wallJumpforce = 10f;         //壁ジャンプ力
    [SerializeField] Vector2 wallJumpAngle = default;     //壁ジャンプの角度
    [SerializeField] float wallJumpDirection = -1;      //壁ジャンプの方向(+が右、-が左)
    [SerializeField] float preWallJumpDirection = -1;   //前の壁ジャンプ方向保存用
    [SerializeField] int wallJumpCount;                 //現在壁ジャンプ回数カウント
    [SerializeField] int defaultWallJumpCount = 3;      //壁ジャンプ回数カウント

    [Header("地面")]
    [SerializeField] LayerMask groundLayer = default;         //地面レイヤ
    [SerializeField] Transform groundCheckPoint = default;    //地面接触チェックのためオブジェクトの位置
    [SerializeField] Vector2 groundCheckSize = default;       //足場判定オブジェクトのサイズ指定
    [SerializeField] bool isGrounded;                         //地面接触判定

    [Header("坂")]
    [SerializeField] LayerMask slopeLayer = default;   //坂レイヤ
    [SerializeField] bool isSlope;                     //坂判定

    [Header("頭")]
    [SerializeField] LayerMask headLayer = default;       //頭チェック用レイヤ(TileMap用の全レイヤー)
    [SerializeField] Transform headCheckPoint = default;  //地面接触チェックのためオブジェクトの位置
    [SerializeField] Vector2 headCheckSize = default;     //上記オブジェクトのサイズ
    [SerializeField] bool isHeadCheck;                    //頭判定

    [Header("その他")]
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    //-----------------------------------------------

    /// <summary>
    /// メソッド
    /// </summary>

    //-----------------------------------------------
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Inputs();
        CheckWorld();
        AnimationControl();
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
        WallSlide();
        WallJump();
        HeadCollision(isHeadCheck);
    }

    /// <summary>
    /// 入力 
    /// </summary>
    void Inputs()
    {
        xDirectionalInput = InputManager.Instance.input.AxisHorizontal();

        if (InputManager.Instance.input.JumpKeyDown() && !(!isGrounded && (!isWallSliding || !isTouchingWall)))  //ジャンプキー入力＋空中ではないかを判定
        {
            isCanJump = true;
        }
    }

    /// <summary>
    /// 接触判定
    /// </summary>
    void CheckWorld()
    {
        if (rb.velocity.y <= 0.1) //上昇中でない時、足場接触判定を行う
        {
            isGrounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);//地面
        }

        if (!isGrounded)    //地面接触判定がない時に処理を行う
        {
            isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer);//壁
        }


        //1回ぶつかったら地面に着くまでtrueの状態(移動操作できないするため)
        if (Physics2D.OverlapBox(headCheckPoint.position, headCheckSize, 0, headLayer)) //頭
        {
            isHeadCheck = true;
        }
        if (Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, slopeLayer))  //坂
        {
            isSlope = true;
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    void Movement()
    {
        //走るアニメーション関連
        if (isGrounded)  //地面との接触判定している時に判定
        {
            if (xDirectionalInput != 0) //横移動のチェック
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }

        //動き関連
        if (isGrounded) //地面接触の時の処理
        {
            rb.velocity = new Vector2(xDirectionalInput * moveSpeed, rb.velocity.y);
            wallJumpCount = defaultWallJumpCount;    //壁ジャンプの回数の初期化
            wallSlideTime = defaultWallSlideTime;   //壁すりの時間の初期化
            isHeadCheck = false;    //頭判定初期化
            isSlope = false;        //坂判定初期化
            isTouchingWall = false; //壁接触判定初期化
            isJump = false;         //ジャンプ状態初期化
        }
        //空中での移動のチェック
        else if (!isGrounded && (!isWallSliding || !isTouchingWall) && xDirectionalInput != 0)
        //地面に接触していないか＋(壁すりをしていないか、もしくは壁接触をしていないか)＋横移動をしているか
        {
            //坂接触+頭接触判定(どちらか接触していたら操作できないようにするため)
            if (!isSlope && !isHeadCheck)
            {
                rb.AddForce(new Vector2(airMoveSpeed * xDirectionalInput, 0));
                //空中移動の速度を地面からの移動より速くしない
                if (Mathf.Abs(rb.velocity.x) > moveSpeed) //横移動の速度の絶対値 > 移動速度　のチェック
                {
                    rb.velocity = new Vector2(xDirectionalInput * moveSpeed, rb.velocity.y);
                }
            }
        }


        //左右表示関連
        if (xDirectionalInput < 0 && isFacingRight)
        {
            Flip();
        }
        else if (xDirectionalInput > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    /// <summary>
    /// 左右表示関連
    /// </summary>
    void Flip()
    {
        wallJumpDirection *= -1;        //壁ジャンプの方向決め
        //左右反転処理                                
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    //ジャンプ
    void Jump()
    {
        if (isCanJump && isGrounded)    //ジャンプ可能の判定＋地面接触判定
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            preWallJumpDirection = wallJumpDirection;   //前の壁ジャンプ方向保存
            isGrounded = false;
            isCanJump = false;
            isMoving = false;
            isJump = true;
        }
        if (!isGrounded && rb.velocity.y <= 0) //地面の接触判定＋落下中かの判定
        {
            isJump = false;
        }
    }

    /// <summary>
    /// 壁すり
    /// </summary>
    void WallSlide()
    {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        //壁接触判定＋地面接触してないかの判定＋ジャンプによる上昇中ではないかの判定
        {
            isJump = false;
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding && xDirectionalInput != 0 && wallSlideTime > 0)  //壁すり状態の判定＋横移動をしているか
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);  //徐々に落ちるので-の値
            wallSlideTime -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 壁ジャンプ
    /// </summary>
    void WallJump()
    {
        if ((isWallSliding) && isCanJump && wallJumpCount > 0)   //壁すりしているか＋ジャンプ可能の判定＋壁ジャンプカウントのチェック
        {
            if (preWallJumpDirection == wallJumpDirection)   //前の壁ジャンプ方向と現在の壁ジャンプ方向が同じだったら
            {
                wallJumpCount--;    //壁ジャンプカウントを1減らす
            }
            rb.velocity = new Vector2(wallJumpforce * wallJumpAngle.x * wallJumpDirection, wallJumpforce * wallJumpAngle.y);
            preWallJumpDirection = wallJumpDirection;   //前の壁ジャンプ方向保存
            Flip();
            isJump = true;
            isCanJump = false;
        }
    }

    /// <summary>
    /// アニメーションパラメータ設定
    /// </summary>
    void AnimationControl()
    {
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isJump", isJump);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    /// <summary>
    /// 頭衝突
    /// </summary>
    /// <param name="check_">頭衝突判定</param>
    void HeadCollision(bool headcheck_)
    {
        //判定を行っていないなら終了
        if (!headcheck_)
        {
            return;
        }

        StartCoroutine(SpriteFlashing(headcheck_));
    }

    /// <summary>
    /// 点滅
    /// </summary>
    /// <param name="check_">任意の判定</param>
    /// <returns></returns>
    IEnumerator SpriteFlashing(bool check_)
    {
        //無敵時間中の点滅
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }

    /// <summary>
    /// 地面接触判定範囲と、壁接触判定範囲を可視化するためのギズモ
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        //地面接触判定範囲は青
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);

        //壁接触判定範囲は緑
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);

        //頭接触判定範囲は赤
        Gizmos.color = Color.red;
        Gizmos.DrawCube(headCheckPoint.position, headCheckSize);
    }
}
