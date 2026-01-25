using UnityEngine;
using System.Collections;

public class Enemy_Self_destruct : MonoBehaviour
{
    public GameObject explosionPrefab;

    [Header("Explosion Damage")]
    [SerializeField] float explosionDamage;

    EnemyMove enemyMove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyMove = GetComponentInParent<EnemyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Self_Explosion()
    {
        enemyMove.canMove = false; // 폭발 시작 시 움직임 통제

        StartCoroutine("Explosion");
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3.0f);

        GameObject explosionObj = Instantiate(explosionPrefab);
    }
}
