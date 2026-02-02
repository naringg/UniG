using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPortalInteractor : MonoBehaviour
{
    private Portal currentPortal;

    private void Update()
    {
        if (currentPortal == null) return;

        if (Input.GetKeyDown(currentPortal.interactKey))
        {
            if (!currentPortal.CanInteract(gameObject)) return;

            // 씬 이동 + 스폰 위치 전달
            PortalSpawnData.TargetSpawnPosition = currentPortal.targetSpawnPosition;
            PortalSpawnData.HasPendingSpawn = true;

            SceneManager.LoadScene(currentPortal.targetSceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Portal portal))
        {
            currentPortal = portal;
            // 여기서 "포탈 위 화살표 UI" 같은거 켜도 됨
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Portal portal))
        {
            if (currentPortal == portal)
                currentPortal = null;
            // 여기서 안내 UI 끄기
        }
    }
}
