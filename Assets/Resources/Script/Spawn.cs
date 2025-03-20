using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public void CreateMonster(SOMonster monsterdata)
    {
        var obj = PoolingManager.GetObj(monsterdata.MonsterName);
        obj.GetComponent<Monster>().SetMonsterData(monsterdata);
        obj.transform.position = transform.position;
    }

    public void CreatePrize(SOPrize prizeData)
    {
        var obj = PoolingManager.GetObj(prizeData.PrizeName);
        obj.GetComponent<Monster>().SetPrizeData(prizeData);
        obj.transform.position = transform.position;
    }
}
