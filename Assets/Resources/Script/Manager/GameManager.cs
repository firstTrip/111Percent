using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [Header("Upgrade Data")]
    [SerializeField]
    SOSummons[] SummonsDatas;

    public GradeType[] summonsGrade = { GradeType.NOMAL, GradeType.SPECIAL, GradeType.RARE, GradeType.UNIQUE, GradeType.LEGEND };

    [Space]
    [Header("Spawn")]
    [SerializeField]
    Spawn[] spawns;

    public enum MagicType
    { 
        NONE,
        GROUND,
        FIRE,
        WATER
    
    }

    public enum GradeType
    {
        NONE,
        NOMAL,
        SPECIAL,
        RARE,
        UNIQUE,
        LEGEND

    }


    [Space]
    [Header("Magic Data")]
    [SerializeField]
    Dictionary<MagicType, Dictionary<GradeType, int>> dictMagic = new Dictionary<MagicType, Dictionary<GradeType, int>>();


    [Space]
    [Header("ETC Data")]
    [SerializeField]
    public Player player;

    [SerializeField]
    int SummonsCnt = 20;

    [SerializeField]
    float GameSpeed =1f;

    [SerializeField]
    int WaveLevel = 1;

    [SerializeField]
    int CoinCnt = 0;

    [SerializeField]
    int SpritCnt = 0;

    [SerializeField]
    int MaxMagicCnt = 28;

    [SerializeField]
    int UpgradeCnt = 0;

    [SerializeField]
    int SummonepgradeCnt = 0;


    [SerializeField]
    public int GroundGrade = 0;

    [SerializeField]
    public int FireGrade = 0;

    [SerializeField]
    public int WaterGrade = 0;

    private void Awake()
    {
        PoolingManager.Instance.Initailize(10);

        StartCoroutine(CO_Timer());


        dictMagic.Add(MagicType.GROUND, new Dictionary<GradeType, int>());
        dictMagic.Add(MagicType.FIRE, new Dictionary<GradeType, int>());
        dictMagic.Add(MagicType.WATER, new Dictionary<GradeType, int>());
    }

    IEnumerator CO_Timer()
    {
        yield return new WaitForSeconds(1f);

        Text timerText = UIManager.Instance.GetMagicPanel().GetTimer();

        while ((WaveLevel-1) < stageDatas.Length)
        {
            nowStage = stageDatas[WaveLevel - 1];
            nowMonster = MonsterDatas[WaveLevel - 1];
            timerText.color = Color.white;
            float _time = nowStage.time;
            StartCoroutine(CO_MonsterSpawn());
            while(_time>0)
            {
                timerText.text = string.Format("{0:D2}:{1:D2}", (_time / 60).ToString("00"), _time.ToString("00"));
                _time -= Time.deltaTime;

                if(_time<3f)
                {
                    timerText.color = Color.red;
                }

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

    public Spawn GetPlayerSpawn()
    {
       return spawns[0];
    }

    public void SetCoin(int _cnt)
    {
        CoinCnt += _cnt;
        UIManager.Instance.GetMagicPanel().SetCoinText();
    }

    public void SetSprit(int _cnt)
    {
        SpritCnt += _cnt;
        UIManager.Instance.GetMagicPanel().SetSpritText();
    }

    public int GetCoin()
    {
        return CoinCnt;
    }

    public int GetSprit()
    {
        return SpritCnt;
    }

    public int GetMagicCnt()
    {
        int cnt = 0;

        foreach(var data in dictMagic)
        {
            foreach(var item in data.Value)
            {
                cnt += item.Value;
            }
        }


        return cnt;
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

    public void AddMagic(MagicType type, GradeType grade)
    {
        if(dictMagic[type].ContainsKey(grade))
        {
            dictMagic[type][grade] += 1;
        }
        else
        {
            dictMagic[type].Add(grade, 1);
        }
    }

    public void RemoveMagic(MagicType type, GradeType grade)
    {
        if (dictMagic[type].ContainsKey(grade))
        {
            dictMagic[type][grade] -= 3;
        }
    }


    public Dictionary<MagicType, Dictionary<GradeType, int>> GetMagicDict()
    {
        return dictMagic;
    }
    public int GetMaxMagicCnt()
    {
        return MaxMagicCnt;
    }

    public void SummonsUpgrade()
    {
        SummonepgradeCnt += 1;
    }

    public int GetSummonsUpgrade()
    {
        return SummonepgradeCnt + 1;
    }

    public void GroundUpgrade()
    {
        GroundGrade += 1;
    }

    public void FireUpgrade()
    {
        FireGrade += 1;
    }

    public void WaterUpgrade()
    {
        WaterGrade += 1;
    }

    public int GetMagicUpgrade(MagicType magicType)
    {
        switch (magicType)
        { 
            case MagicType.GROUND:
                return GroundGrade;

            case MagicType.FIRE:
                return FireGrade;

            case MagicType.WATER:
                return WaterGrade;
        }
        return 0;
    }

    public SOSummons GetSummonsData()
    {
        return SummonsDatas[SummonepgradeCnt];
    }
}
