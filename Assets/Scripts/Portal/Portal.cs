using UnityEngine;
using TMPro;

public class Portal : MonoBehaviour
{
    [Header("Destination")]
    public string targetSceneName;
    public Vector2 targetSpawnPosition;
    public KeyCode interactKey = KeyCode.UpArrow;

    [Header("UI")]
    public TMP_Text floatingText;     // 포탈 위 텍스트
    public string message;

    private void Awake()
    {
        if (floatingText != null)
        {
            floatingText.text = message;
            floatingText.gameObject.SetActive(false); // 기본은 숨김
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && floatingText != null)
            floatingText.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && floatingText != null)
            floatingText.gameObject.SetActive(false);
    }

    public bool CanInteract(GameObject player) => true;
}
