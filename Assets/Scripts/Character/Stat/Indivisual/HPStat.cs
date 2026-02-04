using UnityEngine;

public class HPStat : BaseStat
{
    private float CurHP;

    public override void SetStat(float Stat)
    {
        base.SetStat(Stat);
        CurHP = Stat;
    }

    public void HealHP(float Stat)
    {
        CurHP = Mathf.Min(CurHP + Stat, GetStat());
    }

    public float GetCurHP()
    {
        return CurHP;
    }

    public void UseHP(float Stat)
    {
        if (Stat > CurHP)
        {
            return;
        }
        else
        {
            CurHP -= Stat;
        }
    }
    /// <summary>
    /// - Calculate
    /// </summary>
    /// <param name="Stat"></param>
    public void Damaged(float Stat)
    {
        CurHP = Mathf.Max(CurHP - Stat, 0);
    }

}
