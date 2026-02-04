using UnityEngine;

public class MPStat : BaseStat
{
    private float CurMP;

    public override void SetStat(float Stat)
    {
        base.SetStat(Stat);
        CurMP = Stat;
    }

    public void HealMP(float Stat)
    {
        CurMP = Mathf.Min(CurMP + Stat, GetStat());
    }

    public float GetCurMP() 
    {
        return CurMP;
    }
    public void UseMP(float Stat)
    {
        if(Stat > CurMP)
        {
            return;
        }else
        {
            CurMP -= Stat;
        }
    }
}
