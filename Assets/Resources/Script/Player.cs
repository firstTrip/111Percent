using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    RectTransform attackPos;

    [SerializeField]
    float HP =200f;

    private void Update()
    {
        if(HP<0)
        {
            //game end
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
    }
}
