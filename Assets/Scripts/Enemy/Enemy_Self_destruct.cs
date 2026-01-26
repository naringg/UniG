using UnityEngine;
using System.Collections;

public class Enemy_Self_destruct : MonoBehaviour
{
    public GameObject explosionPrefab;
    public LayerMask playerLayer;

    [Header("Enemy Stats")]
    [SerializeField] int explosionDamage;
    [SerializeField] float explosionRadius = 3.0f;
    [SerializeField] int HP = 10;

    EnemyMove enemyMove;

    void Start()
    {
        enemyMove = GetComponentInParent<EnemyMove>();
    }

    public void Self_Explosion()
    {
        enemyMove.canMove = false; // 폭발 시작 시 움직임 통제

        StartCoroutine("Explosion");
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3.0f); // 폭발까지 대기 시간, 3초.

        Boom();
        //GameObject explosionObj = Instantiate(explosionPrefab);
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
        Debug.Log("자폭병 사라짐!");
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
