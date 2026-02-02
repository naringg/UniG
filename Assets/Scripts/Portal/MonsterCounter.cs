using TMPro;
using UnityEngine;

public class MonsterCounter : MonoBehaviour
// {
//     public static MonsterCounter Instance { get; private set; }

//     public int AliveCount { get; private set; }

//     private void Awake()
//     {
//         if (Instance != null && Instance != this)
//         {
//             Destroy(gameObject);
//             return;
//         }
//         Instance = this;
//         AliveCount = 0;
//     }

//     public void Register()
//     {
//         AliveCount++;
//         // Debug.Log($"Monster + : {AliveCount}");
//     }

//     public void Unregister()
//     {
//         AliveCount = Mathf.Max(0, AliveCount - 1);
//         // Debug.Log($"Monster - : {AliveCount}");
//     }
// }

{
    [Header("TEST ONLY")]
    [Tooltip("인스펙터에서 직접 조절")]
    public int remainMonsterCount = 3;

    [Header("UI")]
    public TMP_Text countText;

    public bool IsClear => remainMonsterCount <= 0;

    private void Start()
    {
        UpdateText();
    }

    // ▶ 플레이 중 숫자 바꿀 때 바로 반영
    private void Update()
    {
        UpdateText();
    }

    // ▶ 인스펙터에서 값 바꿀 때 즉시 반영 (Play/Stop 상관없이)
    private void OnValidate()
    {
        UpdateText();
    }

    void UpdateText()
    {
        if (countText == null) return;
         countText.text = remainMonsterCount.ToString();
    }     
}
