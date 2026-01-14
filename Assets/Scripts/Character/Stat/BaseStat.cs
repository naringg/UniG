using System.Collections;
using UnityEngine;

public class BaseStat : MonoBehaviour
{

    private int BasicStat = 1;
   private float BasicMulti = 1.0f;


    public void SetStat(int Stat)
    {
        BasicStat = Stat;
    }
    public void SetMulti(float  Multi)
    {
        BasicMulti = Multi;
    }

    public float GetStat()
    {
        return BasicStat * BasicMulti;
    }

    public void AddSubStat(int Stat)
    {
        if(Stat < 0)
        {
            Mathf.Max(BasicStat + Stat, 0);
        }
        else
        {
            Mathf.Min(BasicStat + Stat, 100);//최대값 정해야 함
        }
    }

    public void AddSubMulti(float  Multi)
    {
        if (Multi < 0)
        {
            Mathf.Max(BasicStat + Multi, 0f);
        }
        else
        {
            Mathf.Min(BasicStat + Multi, 100f);//최대값 정해야 함
        }
    }
    public void BuffNerfStat(int Stat, float Time)
    {
        StartCoroutine(CoroutineStat(Stat,Time));
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
