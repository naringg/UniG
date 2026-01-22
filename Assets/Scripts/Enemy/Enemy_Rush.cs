using UnityEngine;
using System.Collections;

public class Enemy_Rush : MonoBehaviour
{
    public PlayerMove player;

    Animator animator;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    EnemyMove enemyMove;

    [SerializeField] private float rushPower;

    bool coroutineRoop;
    bool OnDetectPlayer = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyMove = GetComponent<EnemyMove>();
    }

    public void OnDetectedPlayer() 
    {
        if (!coroutineRoop) // 코루틴이 중첩되지 않도록 bool타입으로 통제.
        {
            coroutineRoop = true;
            spriteRenderer.flipX = player.transform.position.x < transform.position.x; // 플레이어의 X값이 몬스터의 X값보다 작으면 왼쪽(true), 크면 오른쪽(false)으로 강제 설정
            enemyMove.switchFlipX = true; // 몬스터의 방향을 정했다면 EnemyMove의 방향을 작동하지 못하게 잠궈버림
            OnDetectPlayer = true;
            enemyMove.canMove = false;
            StartCoroutine("RushWaitingState");
        }     
    }

    public void OnLeftPlayer()
    {
        if (!OnDetectPlayer)
            animator.SetBool("OnDetected", false);
    }

    IEnumerator RushWaitingState()
    {
        rigid.linearVelocity = Vector2.zero;
        animator.SetBool("OnDetected", true);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("Rush");
    }

    IEnumerator Rush()
    {
        animator.SetTrigger("Rush");
        animator.SetBool("OnDetected", false);
        if (spriteRenderer.flipX)
            rigid.AddForce(Vector2.left * rushPower, ForceMode2D.Impulse);
        else 
            rigid.AddForce(Vector2.right * rushPower, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("RushToIdle");
        enemyMove.canMove = true; // 돌진이 아닌 일반적인 움직임을 다시 작동시킴.
        enemyMove.switchFlipX = false;

        yield return new WaitForSeconds(2.0f); // 돌진 이후 바로 연속적인 돌진을 못하게끔 2.5초라는 시간을 걸어둠.

        OnDetectPlayer = false;
        coroutineRoop = false;
    }
}
