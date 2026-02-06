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
    //public GameObject noticeMark;

    Rigidbody2D rigid;
    Vector2 vecDir;
    SpriteRenderer sprite;
    Animator animator;
    Vector2 randomDir; // 공중 몬스터 전용

    public float distance = 3; // 범위 안에 플레이어가 들어오면 enemy가 플레이어 쪽으로 이동하게 만드는, 그 범위를 담당하는 변수
    public float speed = 1; // 적을 발견하고 나서 사용되는 이동 변수
    public float randomMoveSpeed = 1; // 적을 발견하지 않을 때, 즉 랜덤으로 움직일 때 사용되는 이동 변수
    public float rayOffset = 1;
    public bool canMove = true; // 타 스크립트에서 제어할 수 있도록 하는, 움직임 전체를 통제하는 역할
    public bool switchFlipX = false; // 타 스크립트에서 flipX를 제어할 수 있도록 하는, flipX 전체를 통제하는 역할
    public bool moving = false; // 현재 몬스터가 이동중인지 아닌지~
    public bool OnDetectPlayer = false; // 적을 발견할 때 사용하는 변수

    //bool keepingNoticeMark = false;
    bool flyingMonster = false; // 이 몬스터가 지금 공중몹인지 아닌지~

    
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        //noticeMark.gameObject.SetActive(false);
        foreach (Transform child in transform)
    {
        if (child.CompareTag("Flying"))
        {
            flyingMonster = true;
            break;
        }
    }
        StartCoroutine("RandomMove");
    }

    void Update()
    {
        // 플레이어와의 거리 계산 로직
        if (distance >= Mathf.Abs(player.transform.position.x - transform.position.x))
        {
            if (flyingMonster) // 비행몬스터일 경우의 이동 로직
            {
                vecDir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized;
            }

            else // 非비행몬스터일 경우의 이동 로직
            {
                vecDir = new Vector2(player.transform.position.x - transform.position.x, 0).normalized;
            }
            
            if (canMove) OnDetectPlayer = true;
            OnDetected.Invoke();

            /*if (!keepingNoticeMark)
            {
                keepingNoticeMark = true;
                StartCoroutine("IsNoticeMark"); // 플레이어를 인식하면 말풍선 띄우기.
            }*/
        }
        else
        {
            OnDetectPlayer = false;
            //keepingNoticeMark = false;
            OnLost.Invoke();
        }
        //-------------------------------------------

        // 이동 애니메이션 재생 (비행몬스터가 아닌 경우)
        if (moving && !flyingMonster)
            animator.SetBool("Walk", true);
        else if (!moving && !flyingMonster)
            animator.SetBool("Walk", false);
        //-------------------------------------------

        // Enemy의 FlipX에 대한 코드
        if (switchFlipX == false)
        {
            if (OnDetectPlayer) // enemy가 플레이어를 추격중이라면 그때의 방향벡터로 flipX를 결정
            {
                sprite.flipX = vecDir.x < 0;
            }
            else // 추적 중이 아니고, 랜덤 이동중일 때는 case에 맞게 flipX 설정
            {
                if (randomDir.x > 0) sprite.flipX = false;
                else if (randomDir.x < 0) sprite.flipX = true;
            }
        }
        
        //-----------------------------------------------

        if (!flyingMonster && !OnDetectPlayer) // Ray를 이용하여 낭떠러지 유무 체크.
        {
            Vector2 rayOrigin = new Vector2(rigid.position.x + (randomDir.x * rayOffset), rigid.position.y - 1);
            Debug.DrawRay(rayOrigin, Vector2.down, Color.green);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 1.0f, LayerMask.GetMask("Floor"));

            if (hit.collider == null) // 낭떠러지 발견 시 반대쪽으로 움직임.
            {
                if (randomDir.x < 0) randomDir.x = 1;
                else if (randomDir.x > 0) randomDir.x = -1;
            }
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return; // 움직일 수 없다면 -> FixedUpdate를 아예 사용할 수 없게 함.

        if (OnDetectPlayer)
        {
            moving = true;
            rigid.linearVelocity = vecDir * speed;
        }

        // 랜덤 이동 (지상 몬스터)
        else if (!OnDetectPlayer) {
            /*switch (randomMoveNum)
                {
                    // -1 = 왼쪽 이동
                    case -1:
                        moving = true;
                        rigid.linearVelocity = Vector2.left * randomMoveSpeed;
                        break;
                    // 1 = 오른쪽 이동
                    case 1:
                        moving = true;
                        rigid.linearVelocity = Vector2.right * randomMoveSpeed;
                        break;
                    // 0 = 가만히 서있음
                    case 0:
                        moving = false;
                        rigid.linearVelocity = Vector2.zero;
                        break;
                    // 혹시 몰라 넣어둠. 다른 숫자를 받으면 가만히 서있게 하기.
                    default :
                        moving = false;
                        rigid.linearVelocity = Vector2.zero;
                        break;
                }*/
            rigid.linearVelocity = randomDir * randomMoveSpeed;
        }
    }

    IEnumerator RandomMove()
    {
        while (true)
        {
            if (!OnDetectPlayer)
            {
                if (flyingMonster)
                {
                    //randomMoveNum_Flying = Random.Range(-1, 2);
                    float x = Random.Range(-1, 2); 
                    float y = Random.Range(-1, 2);
                    randomDir = new Vector2(x, y).normalized;
                }

                else
                {
                    //randomMoveNum = Random.Range(-1, 2);
                    randomDir = new Vector2(Random.Range(-1, 2), 0).normalized;
                }
            }
            yield return new WaitForSeconds(2.5f);
        }
    }

    /*IEnumerator IsNoticeMark()
    {
        noticeMark.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        noticeMark.gameObject.SetActive(false);
    }*/
}
