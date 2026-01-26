using UnityEngine;
using System.Collections;
using UnityEditor.ShaderGraph;
using System;
using Unity.VisualScripting;

public class Enemy_Self_destruct : MonoBehaviour
{
    public GameObject explosionPrefab;
    public LayerMask playerLayer;

    Animator animator;
    SpriteRenderer spriteRenderer;
    Color normalColor = new Color32(255, 255, 255, 255);
    Color dangerColor = new Color32(255, 170, 170, 255);
    Color boomColor = new Color32(255, 75, 75, 255);
    EnemyMove enemyMove;

    bool onDetectedPlayer = false;
    bool startedBoom = false;

    [Header("적 스텟")]
    [SerializeField] int explosionDamage;
    [SerializeField] float explosionRadius = 3.0f;
    [SerializeField] int HP = 10;

    [Header("점멸 효과 설정")]
    [SerializeField] float flashTime = 2.0f;

    void Start()
    {
        enemyMove = GetComponentInParent<EnemyMove>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = normalColor; // 초기 색상
    }

    void Update()
    {
        if (onDetectedPlayer) // 적 추적 시, 몬스터의 색깔을 점멸시킴.
        {
            float blinkValue = Mathf.Abs(Mathf.Sin(Time.time));

            spriteRenderer.color = Color.Lerp(normalColor, dangerColor, blinkValue);
        }
        
        if (startedBoom)
        {
            float blinkValue = Mathf.Abs(Mathf.Sin(Time.time));

            spriteRenderer.color = Color.Lerp(dangerColor, boomColor, 0.1f * 2);
        }

        if (!onDetectedPlayer && spriteRenderer.color != normalColor) // 만약 적 추적이 끝났는데, 아직 색상이 평범한 색으로 돌아오지 않았을 경우, 부드럽게 원래 색상으로 돌아가게 함.
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, normalColor, 0.1f);
        }
    }

    public void OnDetectedPlayer()
    {
        onDetectedPlayer = true;
    }

    public void OnLeftPlayer()
    {
        onDetectedPlayer = false;
    }

    public void Self_Explosion()
    {
        enemyMove.canMove = false; // 폭발 시작 시 움직임 통제
        startedBoom = true;
        StartCoroutine("Explosion");
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3.0f); // 폭발까지 대기 시간 3초.

        Boom();
    }

    void Boom()
    {
        Collider2D[] playerCollider = Physics2D.OverlapCircleAll(transform.position, explosionRadius, playerLayer);
        GameObject explosionObj = Instantiate(explosionPrefab, transform.position, transform.rotation);
        foreach (Collider2D player in playerCollider)
        {
            player.GetComponent<Player>().OnDamaged(explosionDamage);
            Debug.Log("플레이어 데미지");
        }
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
