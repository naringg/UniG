using UnityEngine;

public class Silhum : MonoBehaviour
{
    public float speed = 5f; //변수 = 정보 담아놓는 박스
    Rigidbody2D rb; //물리법칙을 구현해주는 친구(힘을 가해서 미끄러지기, 중력 적용해주기)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        start();
    }

    void FixedUpdate()
    {
    }

    public void start()
    {
        rb.MovePosition(new Vector2(0,0));
    }

}
