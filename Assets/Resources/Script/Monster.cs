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

    private void Update()
    {
        if(MonsterHP<0)
        {
            PoolingManager.ReturnObj(MonsterName, this.gameObject);
        }
    }

    public void SetMonsterData(SOMonster monsterdata)
    {
        Debug.Log("몬스터 생성");

        MonsterName = monsterdata.MonsterName;
        MonsterSpeed = monsterdata.MonsterSpeed;
        MonsterAttack = monsterdata.MonsterAttack;
        MonsterHP = monsterdata.MonsterHP;
    }

    public void GetDamage(float dmg)
    {
        MonsterHP -= dmg;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bar"))
        {
            Debug.Log("Bar Get DMG");

        }
    }
}
