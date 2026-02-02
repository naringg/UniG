using UnityEngine;

public class Portal : MonoBehaviour
{
    public string targetSceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 targetSpawnPosition;

    public KeyCode interactKey = KeyCode.UpArrow; //이동조작 해당키

    public bool CanInteract(GameObject player)
    {
        // 조건(퀘스트 완료, 레벨 등)
        return true;
    }
}
