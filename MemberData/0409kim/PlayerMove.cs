using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //フィールド
    //-----------------------------------------------
    [Header("動き")]
    [SerializeField] float moveSpeed = 10f;     //移動速度
    [SerializeField] float airMoveSpeed = 30f;  //空中移動速度
    [SerializeField] float xDirectionalInput;   //X軸(横)方向値
    [SerializeField] bool isFacingRight = true; //右方向向き判定
    [SerializeField] bool isMoving;             //動き判定

    [Header("ジャンプ")]
    [SerializeField] float jumpForce = 15f;         //ジャンプ力
    [SerializeField] LayerMask groundLayer;         //地面レイヤ
    [SerializeField] Transform groundCheckPoint;    //地面接触チェックのためオブジェクトの位置
    [SerializeField] Vector2 groundCheckSize;       //上記オブジェクトのサイズ
    [SerializeField] bool isGrounded;               //地面接触判定
    [SerializeField] bool isCanJump;                  //ジャンプ可能の判定

    [Header("壁すり")]
    [SerializeField] float wallSlideSpeed;          //壁すりの速度
    [SerializeField] LayerMask wallLayer;           //壁レイヤ
    [SerializeField] Transform wallCheckPoint;      //壁接触チェックのためオブジェクトの位置
    [SerializeField] Vector2 wallCheckSize;         //上記オブジェクトのサイズ
    [SerializeField] bool isTouchingWall;           //壁接触判定
    [SerializeField] bool isWallSliding;            //壁すり状態の判定

    [Header("壁ジャンプ")]
    [SerializeField] float wallJumpforce;               //壁ジャンプ力
    [SerializeField] Vector2 wallJumpAngle;             //壁ジャンプの角度
    [SerializeField] float wallJumpDirection = -1;      //壁ジャンプの方向(+が右、-が左)
    [SerializeField] float preWallJumpDirection = -1;   //前の壁ジャンプ方向保存用
    [SerializeField] int wallJumpCount = 3;             //壁ジャンプ回数カウント

    [Header("その他")]
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    //-----------------------------------------------

    //メソッド
    //-----------------------------------------------
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Inputs();
        CheckWorld();
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
        WallSlide();
        WallJump();
    }

    //入力
    void Inputs()
    {
        xDirectionalInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && !(!isGrounded && (!isWallSliding || !isTouchingWall)))  //スペースキー入力＋空中ではないかを判定
        {
            isCanJump = true;
        }
    }

    //地面と壁への接触判定
    void CheckWorld()
    {
        isGrounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
        isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer);
    }

    //動き関連
    void Movement()
    {
        //アニメーション関連
        if (xDirectionalInput != 0) //横移動のチェック
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        //動き関連
        if (isGrounded) //地面接触の時の処理
        {
            rb.velocity = new Vector2(xDirectionalInput * moveSpeed, rb.velocity.y);
            wallJumpCount = 3;    //壁ジャンプの回数の初期化
        }
        //空中での移動のチェック
        else if (!isGrounded && (!isWallSliding || !isTouchingWall) && xDirectionalInput != 0)
        //地面に接触していないか＋(壁すりをしていないか、もしくは壁接触をしていないか)＋横移動をしているか
        {
            rb.AddForce(new Vector2(airMoveSpeed * xDirectionalInput, 0));
            //空中移動の速度を地面からの移動より速くしない
            if (Mathf.Abs(rb.velocity.x) > moveSpeed) //横移動の速度の絶対値 > 移動速度　のチェック
            {
                rb.velocity = new Vector2(xDirectionalInput * moveSpeed, rb.velocity.y);
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

    //左右表示関連
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
            isCanJump = false;
        }
    }

    //壁すり
    void WallSlide()
    {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0) 
            //壁接触判定＋地面接触してないかの判定＋ジャンプによる上昇中ではないかの判定
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding && xDirectionalInput != 0)  //壁すり状態の判定＋横移動をしているか
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);  //徐々に落ちるので-の値
        }
    }

    //壁ジャンプ
    void WallJump()
    {
        if ((isWallSliding || isTouchingWall) && isCanJump && wallJumpCount > 0)   //(壁すりしているか、もしくは壁と接触しているか)＋ジャンプ可能の判定＋壁ジャンプカウントのチェック
        {
            if (preWallJumpDirection == wallJumpDirection)   //前の壁ジャンプ方向と現在の壁ジャンプ方向が同じだったら
            {
                wallJumpCount--;    //壁ジャンプカウントを1減らす
            }
            rb.velocity = new Vector2(wallJumpforce * wallJumpAngle.x * wallJumpDirection, wallJumpforce * wallJumpAngle.y);
            preWallJumpDirection = wallJumpDirection;   //前の壁ジャンプ方向保存
            Flip();
            isCanJump = false;
        }
    }

    //地面接触判定範囲と、壁接触判定範囲を可視化するためのギズモ
    private void OnDrawGizmosSelected()
    {
        //地面接触判定範囲は青
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);

        //壁接触判定範囲は緑
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);
    }
}
