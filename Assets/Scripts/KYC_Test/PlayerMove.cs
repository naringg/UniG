using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed;

    bool canMove = false;

    Rigidbody2D rigid;
    Vector2 playerVec;
    SpriteRenderer sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerVec.x > 0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }

    // 플레이어 이동
    void FixedUpdate()
    {
        if (canMove)
        {
            rigid.linearVelocity = new Vector2(playerVec.x * speed, 0);
        }
    }

    // 플레이어 이동 로직
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            canMove = true;
            playerVec = context.ReadValue<Vector2>();
        }
        else
        {
            canMove = false;
        }
    }
}
