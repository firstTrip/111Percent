using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Space]
    [Header("Stage Data")]

    [SerializeField]
    SOStage[] stageDatas;

    [SerializeField]
    SOStage nowStage;

    [Space]
    [Header("Monster Data")]
    [SerializeField]
    SOMonster[] MonsterDatas;

    [SerializeField]
    SOMonster nowMonster;

    [Space]
    [Header("Spawn")]
    [SerializeField]
    Spawn[] spawns;


    [Space]
    [Header("ETC Data")]

    [SerializeField]
    int SummonsCnt = 20;

    [SerializeField]
    float GameSpeed =1f;

    [SerializeField]
    int WaveLevel = 1;

    [SerializeField]
    int CoinCnt = 0;



    private void Awake()
    {
        PoolingManager.Instance.Initailize(10);

        StartCoroutine(CO_Timer());
    }

    IEnumerator CO_Timer()
    {
        yield return new WaitForSeconds(1f);


        while ((WaveLevel-1) < stageDatas.Length)
        {
            nowStage = stageDatas[WaveLevel - 1];
            nowMonster = MonsterDatas[WaveLevel - 1];

            float _time = nowStage.time;
            StartCoroutine(CO_MonsterSpawn());
            while(_time>0)
            {
                UIManager.Instance.GetMagicPanel().GetTimer().text = string.Format("{0:D2}:{1:D2}", (_time / 60).ToString("00"), _time.ToString("00"));
                _time -= Time.deltaTime;

                yield return null;
            }

            IncreaseWaveLevel();
            UIManager.Instance.GetMagicPanel().GetTimerTitle().text = "¿þÀÌºê " + WaveLevel.ToString();

        }
    }

    IEnumerator CO_MonsterSpawn()
    {

        int MonsterCnt = 0;
        while(MonsterCnt < nowStage.MonsterCnt)
        {

            for(int i=0;i<spawns.Length;++i)
            {
                if (spawns[i] !=null)
                {
                    spawns[i].CreateMonster(nowMonster);
                }
            }
            MonsterCnt++;
            yield return new WaitForSeconds(1f);

        }
    }

    public void SetCoin(int _cnt)
    {
        CoinCnt += _cnt;
    }

    public int GetCoin()
    {
        return CoinCnt;
    }

    public int GetSummons()
    {
        return SummonsCnt;
    }
    public void IncreaseSummonsCnt()
    {
        SummonsCnt += 1;
    }

    public void IncreaseWaveLevel()
    {
        WaveLevel += 1;
    }

    public float GetGameSpeed()
    {
        return GameSpeed;
    }

    public void SetGameSpeed()
    {

        if (GameSpeed >= 2.5f)
        {
            GameSpeed = 1f;
            Time.timeScale = GameSpeed;
        }
        else
        {
            GameSpeed += 0.5f;
            Time.timeScale = GameSpeed;

        }
    }
}
