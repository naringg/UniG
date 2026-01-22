using UnityEngine;
using System.Collections;

public class Enemy_Rush : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    EnemyMove enemyMove;

    [SerializeField] private float rushPower;

    bool coroutineRoop;

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
            enemyMove.canMove = false;
            StartCoroutine("RushWaitingState");
        }
            
    }

    public void OnLeftPlayer()
    {
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
        if (spriteRenderer.flipX)
            rigid.AddForce(Vector2.left * rushPower, ForceMode2D.Impulse);
        else 
            rigid.AddForce(Vector2.right * rushPower, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f);

        coroutineRoop = false;
        enemyMove.canMove = true; // 돌진이 아닌 일반적인 움직임을 다시 작동시킴.
    }
}
