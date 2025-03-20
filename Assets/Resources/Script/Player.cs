using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    RectTransform attackPos;

    [SerializeField]
    float HP =200f;
    [SerializeField]
    GameObject hpBar;

    [SerializeField]
    float OriginMonsterHP;

    private void Awake()
    {
        OriginMonsterHP = HP;
    }
    private void Update()
    {
        if(HP < (HP/4))
        {
            var obj = PoolingManager.GetObj("Indicator");
            obj.transform.SetParent(UIManager.Instance.GetUICanvas().transform);
            obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            obj.GetComponent<Indicator>().SetDesc("체력이 많이 낮습니다.", 1.5f);
        }

        if(HP<0)
        {
            //game end

            var obj = PoolingManager.GetObj("Indicator");
            obj.transform.SetParent(UIManager.Instance.GetUICanvas().transform);
            obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            obj.GetComponent<Indicator>().SetDesc("플레이어가 죽었습니다.", 1.5f);

            Time.timeScale = 0f;
        }
    }

    public void Attack(SOMagic magic)
    {
        var obj = PoolingManager.GetObj("Magic");
        obj.GetComponent<Magic>().Init(magic);
        obj.transform.position = attackPos.position;
    }

    public void GetDamage(float _dmg)
    {
        HP -= _dmg;
        hpBar.transform.localScale = new Vector3((HP / OriginMonsterHP) * 0.92f, hpBar.transform.localScale.y, 0);
    }
}
