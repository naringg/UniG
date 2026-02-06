using UnityEngine;

public class FlyingMonster : MonoBehaviour
{
    EnemyMove flyingEnemy;
    Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flyingEnemy = GetComponent<EnemyMove>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flyingEnemy.OnDetectPlayer)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }
}
