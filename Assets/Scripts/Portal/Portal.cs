using UnityEngine;
using TMPro;

public class Portal : MonoBehaviour
{
    [Header("Destination")]
    public string targetSceneName;
    public Vector2 targetSpawnPosition;
    public KeyCode interactKey = KeyCode.UpArrow;

    [Header("Monster Condition")]
    public MonsterCounter monsterCounter;   // ✅ 씬의 MonsterCounter 연결

    [Header("UI")]
    public TMP_Text floatingText;

    private void Awake()
    {
        if (floatingText != null)
            floatingText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        UpdateText();
        floatingText?.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        floatingText?.gameObject.SetActive(false);
    }

    private void Update()
    {
        // 플레이어가 포탈 안에 있을 때만 계속 갱신하고 싶다면:
        // (항상 갱신해도 되지만, 깔끔하게 하려면 필요할 때만 갱신)
        if (floatingText != null && floatingText.gameObject.activeSelf)
            UpdateText();
    }

    void UpdateText()
    {
        if (floatingText == null) return;

        // 카운터가 없으면 안전하게 안내만
        if (monsterCounter == null)
        {
            floatingText.text = "카운터 없음";
            return;
        }

        if (monsterCounter.IsClear)
            floatingText.text = $"move : {targetSceneName}";
        else
            floatingText.text = $"monster Count : {monsterCounter.remainMonsterCount}";
    }

    public bool CanInteract(GameObject player)
    {
        // 카운터가 없으면 이동 허용(테스트용)
        if (monsterCounter == null) return true;

        // ✅ MonsterCounter의 상태를 그대로 사용
        return monsterCounter.IsClear;
    }
}
