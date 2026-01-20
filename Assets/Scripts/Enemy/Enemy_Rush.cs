using UnityEngine;
using System.Collections;

public class Enemy_Rush : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    [SerializeField] private float rushPower;

    bool coroutineRoop;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDetectedPlayer() 
    {
        if (!coroutineRoop) // 코루틴이 중첩되지 않도록 bool타입으로 통제.
        {
            coroutineRoop = true;
            StartCoroutine("RushWaitingState");
        }
            
    }

    public void OnLeftPlayer()
    {
        animator.SetBool("OnDetected", false);
    }

    IEnumerator RushWaitingState()
    {
        animator.SetBool("OnDetected", true);
        rigid.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(1.5f);
        Rush();
    }

    void Rush()
    {
        animator.SetTrigger("Rush");
        if (spriteRenderer.flipX)
            rigid.AddForce(Vector2.left * rushPower, ForceMode2D.Impulse);
        else 
            rigid.AddForce(Vector2.right * rushPower, ForceMode2D.Impulse);

        coroutineRoop = false;
    }
}
