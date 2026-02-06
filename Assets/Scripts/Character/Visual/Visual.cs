using UnityEngine;

public class Visual : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Animator animator;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        //스프라이트 가져오기
    }

    public void AnimationCall(int Index)
    {
        
        animator.SetTrigger("Skill1");
        animator.SetTrigger("Skill2");
        animator.SetTrigger("Skill3");
    }

  
}
