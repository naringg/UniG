using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class BaseStat : MonoBehaviour
{

    protected float BasicStat = 1; //변수들
    protected float BasicMulti = 1.0f;

    public Action<float> OnStatChanged;


    public void Notice()
    {
        OnStatChanged?.Invoke(GetStat());
    }
    public virtual void SetStat(float Stat) //함수
    {
        BasicStat = Stat;
        Notice();
    }

    public void SetMulti(float Multi)
    {
        BasicMulti = Multi;
        Notice();
    }

    public float GetStat()
    {
        return BasicStat * BasicMulti;
    }

    public void AddSubStat(float Stat)
    {
        if (Stat < 0)
        {
            BasicStat = Mathf.Max(BasicStat + Stat, 0);
            
        }
        else
        {
            BasicStat = Mathf.Min(BasicStat + Stat, 100);//최대값 정해야 함
        }
        Notice();
    }

    public void AddSubMulti(float Multi)
    {
        if (Multi < 0)
        {
            BasicMulti = Mathf.Max(BasicMulti + Multi, 0f);
        }
        else
        {
            BasicMulti = Mathf.Min(BasicMulti + Multi, 100f);//최대값 정해야 함
        }
        Notice();
    }
    public void BuffNerfStat(int Stat, float Time)
    {
        StartCoroutine(CoroutineStat(Stat, Time));
    }

    public IEnumerator CoroutineStat(int Stat, float Time)
    {

        AddSubStat(Stat);
        yield return new WaitForSeconds(Time);
        AddSubStat(-Stat);

    }

    public void BuffNerfMulti(float Stat, float Time)
    {
        StartCoroutine(CoroutineStat(Stat, Time));
    }

    public IEnumerator CoroutineStat(float Stat, float Time)
    {

        AddSubMulti(Stat);
        yield return new WaitForSeconds(Time);
        AddSubMulti(-Stat);

        

    }

}