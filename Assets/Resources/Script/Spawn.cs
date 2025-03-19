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
}
