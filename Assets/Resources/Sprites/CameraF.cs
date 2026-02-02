using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // 따라갈 대상 (Player)
    public float smoothSpeed = 5f; // 따라가는 속도
    public Vector3 offset;         // 카메라 위치 보정

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.position = smoothPosition;
    }
}