using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
    // 플레이어를 찾았을 때 실행될 이벤트 보관함
    [Header("UnityEvents")]
    public UnityEvent OnDetected; 
    public UnityEvent OnLost;

    [Header("Public Components")]
    public GameObject player;
    public GameObject noticeMark;

    Rigidbody2D rigid;
    Vector2 vecDir;
    SpriteRenderer sprite;

    public float distance = 3; // 범위 안에 플레이어가 들어오면 enemy가 플레이어 쪽으로 이동하게 만드는, 그 범위를 담당하는 변수
    public float speed = 1; // 적을 발견하고 나서 사용되는 이동 변수
    public float randomMoveSpeed = 1; // 적을 발견하지 않을 때, 즉 랜덤으로 움직일 때 사용되는 이동 변수
    public float rayOffset = 1;
    public bool canMove = true; // 타 스크립트에서 제어할 수 있도록 하는, 움직임 전체를 통제하는 역할

    [SerializeField]bool isMoving = false; // 적을 발견할 때 사용하는 이동 변수
    bool keepingNoticeMark = false;
    int randomMoveNum;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        noticeMark.gameObject.SetActive(false);
        randomMoveNum = 0;
        StartCoroutine("RandomMove");
    }

    void Update()
    {
        // 플레이어와의 거리 계산 로직
        if (distance >= Mathf.Abs(player.transform.position.x - transform.position.x))
        {
            vecDir = new Vector2(player.transform.position.x - transform.position.x, 0).normalized;
            if (canMove) isMoving = true;
            OnDetected.Invoke();

            if (!keepingNoticeMark)
            {
                keepingNoticeMark = true;
                StartCoroutine("IsNoticeMark"); // 플레이어를 인식하면 말풍선 띄우기.
            }
        }
        else
        {
            isMoving = false;
            keepingNoticeMark = false;
            OnLost.Invoke();
        }
        //-------------------------------------------

        // Enemy의 FlipX에 대한 코드
        if (isMoving) // enemy가 플레이어를 추격중이라면 그때의 방향벡터로 flipX를 결정
        {
            sprite.flipX = vecDir.x < 0;
        }
        else // 추적 중이 아니고, 랜덤 이동중일 때는 case에 맞게 flipX 설정
        {
            if (randomMoveNum == 1) sprite.flipX = false;
            else if (randomMoveNum == -1) sprite.flipX = true;
        }
        //-----------------------------------------------

        if (!isMoving) // Ray를 이용하여 낭떠러지 유무 체크.
        {
            Vector2 rayOrigin = new Vector2(rigid.position.x + (randomMoveNum * rayOffset), rigid.position.y - 1);
            Debug.DrawRay(rayOrigin, Vector2.down, Color.green);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 1.0f, LayerMask.GetMask("Floor"));

            if (hit.collider == null) // 낭떠러지 발견 시 반대쪽으로 움직임.
            {
                if (randomMoveNum == -1) randomMoveNum = 1;
                else if (randomMoveNum == 1) randomMoveNum = -1;
            }
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        if (isMoving)
        {
            rigid.linearVelocity = vecDir * speed;
        }

        // 랜덤 이동
        else {
            switch (randomMoveNum)
                {
                    // -1 = 왼쪽 이동
                    case -1:
                        rigid.linearVelocity = Vector2.left * randomMoveSpeed;
                        break;
                    // 1 = 오른쪽 이동
                    case 1:
                        rigid.linearVelocity = Vector2.right * randomMoveSpeed;
                        break;
                    // 0 = 가만히 서있음
                    case 0:
                        rigid.linearVelocity = Vector2.zero;
                        break;
                    // 혹시 몰라 넣어둠. 다른 숫자를 받으면 가만히 서있게 하기.
                    default :
                        rigid.linearVelocity = Vector2.zero;
                        break;
                }
        }
    }

    IEnumerator RandomMove()
    {
        while (true)
        {
            if (!isMoving)
            {
                randomMoveNum = Random.Range(-1, 2);
            }
            yield return new WaitForSeconds(2.5f);
        }
    }

    IEnumerator IsNoticeMark()
    {
        noticeMark.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        noticeMark.gameObject.SetActive(false);
    }
}
