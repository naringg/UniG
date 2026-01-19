using UnityEngine;

public class PlayerSpawnOnLoad : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (!PortalSpawnData.HasPendingSpawn) return;

        // 물리 캐릭터면 rb.position이 더 안정적
        if (rb != null)
        {
            rb.position = PortalSpawnData.TargetSpawnPosition;
            rb.linearVelocity = Vector2.zero; // 이동 중이던 속도 초기화
        }
        else
        {
            transform.position = PortalSpawnData.TargetSpawnPosition;
        }

        PortalSpawnData.HasPendingSpawn = false;
    }
}
