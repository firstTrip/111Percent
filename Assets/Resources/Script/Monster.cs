using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [Space]
    [Header("Monster Data")]
    [SerializeField]
    string MonsterName;
    [SerializeField]
    float MonsterSpeed;
    [SerializeField]
    float MonsterAttack;
    [SerializeField]
    float MonsterHP;

    [SerializeField]
    Rigidbody2D rb;

    private void FixedUpdate()
    {
        rb.velocity = Vector2.up * MonsterSpeed;
    }

    public void SetMonsterData(SOMonster monsterdata)
    {
        Debug.Log("몬스터 생성");

        MonsterName = monsterdata.name;
        MonsterSpeed = monsterdata.MonsterSpeed;
        MonsterAttack = monsterdata.MonsterAttack;
        MonsterHP = monsterdata.MonsterHP;
    }


}
