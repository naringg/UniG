
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{

    public Dictionary<StatType, BaseStat> DicStat = new Dictionary<StatType, BaseStat>();
    public HPStat HPStat;
    public MPStat MPStat;

    private void Awake()
    {
        
    }





    public void SetStat()
    {
        //스크립터블 오브젝트 받아오기
        foreach (BaseStat stat in DicStat.Values)
        {
            stat.SetStat(0);
        }
        HPStat.SetStat(0);
        MPStat.SetStat(0);
    }

    public void SetDic()
    {
        DicStat.Clear();
        DicStat[StatType.HP] = GetComponentInChildren<HPStat>();
        HPStat = GetComponentInChildren<HPStat>();
        DicStat[StatType.MP] = GetComponentInChildren<MPStat>();
        MPStat = GetComponentInChildren<MPStat>();
        DicStat[StatType.Attack] = GetComponentInChildren<AttackStat>();
        DicStat[StatType.Defense] = GetComponentInChildren<DefenseStat>();
        DicStat[StatType.CriticalPer] =GetComponentInChildren<CriticalPerStat>();
        DicStat[StatType.CriticalAttack] = GetComponentInChildren<CriticalAttackStat>();
    }

    public float Attack()
    {
        float CriOK = Random.Range(0, 100);
        if(DicStat[StatType.CriticalPer].GetStat() > CriOK)
        {
            return DicStat[StatType.Attack].GetStat();
        }
        else
        {
            return DicStat[StatType.Attack].GetStat() * DicStat[StatType.CriticalAttack].GetStat();
        }
    }

    public void Defense(float AttackedDamage)
    {
        if(AttackedDamage < DicStat[StatType.Defense].GetStat())
        {
            HPStat.Damaged(1);
        }
        else
        {
            HPStat.Damaged(AttackedDamage - DicStat[StatType.Defense].GetStat());
        }
    }

    public void Heal(float Amount, bool IsHP)
    {
        if(IsHP)
        {
            HPStat.HealHP(Amount);
        }
        else
        {
            MPStat.HealMP(Amount);
        }
    }


    public float GetStat(StatType type, bool FindCur)
    {
        if(FindCur == true)
        {
            if (type == StatType.HP)
            {
                return HPStat.GetCurHP();
            }
            else if (type == StatType.MP)
            {
                return MPStat.GetCurMP();
            }
            else 
            {
                Debug.LogError("너 이상한 접근 중");
                return 0;
            }
        }
        else
        {
            return DicStat[type].GetStat();
        }
    }
}
