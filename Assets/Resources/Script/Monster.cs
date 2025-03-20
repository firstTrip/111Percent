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
    float OriginMonsterHP;
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    GameObject hpBar;


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
            GameManager.Instance.SetCoin(3);
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
        OriginMonsterHP = MonsterHP;
        hpBar.transform.localScale = new Vector3(0.92f, hpBar.transform.localScale.y, 0);
    }

    public void GetDamage(float dmg)
    {
        MonsterHP -= dmg;

        hpBar.transform.localScale = new Vector3( (MonsterHP / OriginMonsterHP) * 0.92f, hpBar.transform.localScale.y,0);
        var obj = PoolingManager.GetObj("DamageText");
        obj.transform.position = this.gameObject.transform.position;
        obj.gameObject.GetComponent<DamageText>().SetDamage(dmg);
    }

}
