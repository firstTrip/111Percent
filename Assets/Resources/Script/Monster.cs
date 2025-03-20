using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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


    float coolTime = 2f;
    private void FixedUpdate()
    {
        rb.velocity = Vector2.up * MonsterSpeed;

        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, Vector2.up, 1f,LayerMask.GetMask("Player"));

        if(hit)
        {
            coolTime -= Time.deltaTime;
            if (coolTime <=0)
            {
                hit.collider.gameObject.GetComponent<Player>().GetDamage(MonsterAttack);
                coolTime = 2f;
            }
        }
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

}
