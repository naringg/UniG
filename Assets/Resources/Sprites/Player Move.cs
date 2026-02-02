using UnityEngine;

public class PlayerMove : MonoBehaviour //플레이어 이동 담당 스크립트
{
    public float moveSpeed = 5f; //좌우 이동속도
    public float jumpForce = 10f; //점프 시 바닥을 발로 차는 힘

    public float dashspeed = 10f; //대시 거리
    public float dashDuration = 0.4f;
    public float doubleTapTime = 0.25f; //더블 클릭

    private Rigidbody2D rb; //플레이어의 몸통(물리)
    private Vector2 moveInput; 
    private bool isGrounded; //바닥 인식
    private float lastRightTapTime = -2f;
    private float lastLeftTapTime = -2f;

    private bool isDashing;
    private float dashTime;
    private int dashDirection;


    private bool isFacingRight = true; //플레이어가 오른쪽을 보는지?(좌우 반전을 위한 상태)

    void Start() //시작할 때 실행
    {
        rb = GetComponent<Rigidbody2D>(); //"Rigidbody"를 가져오기
    }

    void Update() //입력 처리
    {
        float moveX = 0f; //기본 상태(정지)

        if (Input.GetKey(KeyCode.LeftArrow))
            moveX = -1f;
        else if (Input.GetKey(KeyCode.RightArrow))
            moveX = 1f;

        moveInput = new Vector2(moveX, 0f); //x축만 이동(y는 점프에서 따로 처리함)

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) //스페이스바를 누른 순간 && 바닥에 있을때
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); //x의 속도는 그대로 y속도만 위로 (jumpForce)
            isGrounded = false; //공중 상태(바닥에 있지 않다는 뜻)
        }

        if (moveInput.x > 0 && !isFacingRight) //의도는 오른쪽이지만 보는 방향은 왼쪽
        {
            Flip(); //뒤집기
        }
        else if (moveInput.x < 0 && isFacingRight) //의도는 왼쪽이지만 보는 방향은 오른쪽
        
        {
            Flip(); //뒤집기
        }

        //대시 입력
        if(Input.GetKeyDown(KeyCode.LeftArrow)) //왼쪽 화살표 인식
        {
            if (lastLeftTapTime < 0f) //첫번째 인식
            {   
               lastLeftTapTime = Time.time;
               return;
            }

            if (Time.time - lastLeftTapTime <= doubleTapTime) //두번째 인식
            {   
                Debug.Log("대시요~");
            }

            lastLeftTapTime = -1f;
        }
        
        if(Input.GetKeyDown(KeyCode.RightArrow)) //오른쪽 화살표 인식
        {
            if (lastRightTapTime < 0f) //첫번째 인식
            {   
               lastRightTapTime = Time.time;
               return;
            }

            if (Time.time - lastRightTapTime <= doubleTapTime) //두번째 인식
            {   
                Debug.Log("대시요~");
            }

            lastRightTapTime = -1f;
        }

        // 왼쪽 더블탭
        if (Time.time - lastLeftTapTime <= doubleTapTime)
        {
            isDashing = true;
            dashTime = dashDuration;
            dashDirection = -1;
        }
        // 오른쪽 더블탭
        if (Time.time - lastRightTapTime <= doubleTapTime)
        {
            isDashing = true;
            dashTime = dashDuration;
            dashDirection = 1;
        }
    }

    //이동 시 반전
    void Flip()
    {
        isFacingRight = !isFacingRight; //지금 보고있는 방향을 반대로

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale; //"Strite"좌우 효과(물리/좌표는 건드리지 않고, 모양만 뒤집음)
    }

    //대시 구현
    void FixedUpdate() 
{
    if (isDashing)
    {
        rb.linearVelocity =
            new Vector2(dashDirection * dashspeed, rb.linearVelocity.y);

        dashTime -= Time.fixedDeltaTime;
        if (dashTime <= 0f)
            isDashing = false;
    }
    else
    {
        rb.linearVelocity =
            new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }
}

    // 바닥
    void OnCollisionEnter2D(Collision2D collision) //어떤 물체랑 충돌하는 순간 실행
    {
        if (collision.gameObject.CompareTag("Ground")) //바닥에 닿았다 판정
        {
            isGrounded = true;
        }
    }

}
