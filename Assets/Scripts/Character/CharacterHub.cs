using UnityEngine;

public class CharacterHub : MonoBehaviour
{
    CharacterStat stat;
    Visual visual;
    private void Awake()
    {
        stat = GetComponentInChildren<CharacterStat>();
    }


    public void Init()
    {
        //각 초기화
    }
}
