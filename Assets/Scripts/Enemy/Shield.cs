using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    [Header("Shield Position Settings")]
    [SerializeField] private Vector3 backPosition;
    [SerializeField] private Quaternion backRotation;
    [SerializeField] private int backSortingOrder = -1;

    [SerializeField] private Vector3 handPosition;
    [SerializeField] private Quaternion handRotation;
    [SerializeField] private int handSortingOrder = 1;

    [SerializeField] private float moveSpeed = 5f;

    private SpriteRenderer shieldRenderer, parentMove;
    private Coroutine moveRoutine;
    
    void Start()
    {
        shieldRenderer = GetComponent<SpriteRenderer>();
        parentMove = transform.parent.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        shieldRenderer.flipX = parentMove.flipX;
    }

    public void ShieldToFront()
    {
        Debug.Log("shield to front hand");
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(MoveShield(handPosition, handRotation, handSortingOrder));
    }

    IEnumerator MoveShield(Vector3 targetPos, Quaternion targetRot, int targetOrder)
    {
        if (shieldRenderer.sortingOrder == 1) shieldRenderer.sortingOrder = targetOrder;

        while (true)
        {   
            float flipMultiplier = parentMove.flipX ? 1f : -1f;
            Vector3 realTargetPos = new Vector3(targetPos.x * flipMultiplier, targetPos.y, targetPos.z);
            
            float targetZAngle = targetRot.eulerAngles.z * flipMultiplier;
            Quaternion realTargetRot = Quaternion.Euler(0, 0, targetZAngle);

            transform.localPosition = Vector3.Lerp(transform.localPosition, realTargetPos, Time.deltaTime * moveSpeed);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, realTargetRot, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.localPosition, realTargetPos) < 0.01f && 
                Quaternion.Angle(transform.localRotation, targetRot) < 0.1f) 
            {
                if (shieldRenderer.sortingOrder == -1) shieldRenderer.sortingOrder = targetOrder;
                break;
            }

            yield return null;
        }

        if (shieldRenderer.sortingOrder == -1) shieldRenderer.sortingOrder = targetOrder;
        //transform.localPosition = targetPos;
        //transform.localRotation = targetRot;
        moveRoutine = null;        
    }   

    public void ShieldToBack()
    {
        Debug.Log("shield to back");
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(MoveShield(backPosition, backRotation, backSortingOrder));
    }
}
