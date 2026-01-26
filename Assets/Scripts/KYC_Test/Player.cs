using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    int HP = 100;

    public void OnDamaged(int damage)
    {
        HP -= damage;

        Debug.Log("현재 HP : " + HP);
        if (HP <= 0) Die();
    }

    void Die()
    {
        Debug.Log("뒈졋습니다.");
        gameObject.SetActive(false);
    }
}